using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;
using Beijing_Inn_Order_System.Items;
using System.Collections;

namespace Beijing_Inn_Order_System
{
	public partial class CustJournal
	{
        private Point mouseDragStartPoint;
        private DateTime mouseDownTime;
        private Point scrollStartOffset;
        private const double DECELERATION = 980;
        private const double SPEED_RATIO = .5;
        private const double MAX_VELOCITY = 2500;
        private const double MIN_DISTANCE = 0;
        private const double TIME_THRESHOLD = .4;

        public static readonly DependencyProperty ScrollOffsetProperty = DependencyProperty.Register("ScrollOffset", typeof(double), typeof(CustJournal), new UIPropertyMetadata(CustJournal.ScrollOffsetValueChanged));
        public double ScrollOffset
        {
            get { return (double)GetValue(ScrollOffsetProperty); }

            set { SetValue(ScrollOffsetProperty, value); }
        }

        private static void ScrollOffsetValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CustJournal myClass = (CustJournal)d;
            myClass.myScrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }
        
        public CustJournal()
		{
			this.InitializeComponent();

		}

        public void UpdateItemList(List<IItem> items)
        {
            this.listItems.ItemsSource = null;
            this.listItems.ItemsSource = items;     
        }

        private void Scroll(double startY, double endY, DateTime startTime, DateTime endTime)
        {
            double timeScrolled = endTime.Subtract(startTime).TotalSeconds;

            //if scrolling slowly, don't scroll with force
            if (timeScrolled < TIME_THRESHOLD)
            {
                double distanceScrolled = Math.Max(Math.Abs(endY - startY), MIN_DISTANCE);

                double velocity = distanceScrolled / timeScrolled;
                velocity = Math.Min(MAX_VELOCITY, velocity);
                int direction = 1;

                if (endY > startY)
                {
                    direction = -1;
                }

                double timeToScroll = (velocity / DECELERATION) * SPEED_RATIO;

                double distanceToScroll = ((velocity * velocity) / (2 * DECELERATION)) * SPEED_RATIO;

                DoubleAnimation scrollAnimation = new DoubleAnimation();
                scrollAnimation.From = myScrollViewer.VerticalOffset;
                scrollAnimation.To = myScrollViewer.VerticalOffset + distanceToScroll * direction;
                scrollAnimation.DecelerationRatio = .9;
                scrollAnimation.SpeedRatio = SPEED_RATIO;
                scrollAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, Convert.ToInt32(timeToScroll), 0));
                this.BeginAnimation(CustJournal.ScrollOffsetProperty, scrollAnimation);
            }
        }
      

        #region Mouse Overrides
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            mouseDragStartPoint = e.GetPosition(this);
            mouseDownTime = DateTime.Now;
            scrollStartOffset.X = myScrollViewer.HorizontalOffset;
            scrollStartOffset.Y = myScrollViewer.VerticalOffset;

            // Update the cursor if scrolling is possible 
            this.Cursor = (myScrollViewer.ExtentWidth > myScrollViewer.ViewportWidth) ||
                (myScrollViewer.ExtentHeight > myScrollViewer.ViewportHeight) ?
                Cursors.ScrollAll : Cursors.Arrow;

            this.CaptureMouse();
            base.OnPreviewMouseDown(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                // Get the new mouse position. 
                Point mouseDragCurrentPoint = e.GetPosition(this);

                // Determine the new amount to scroll. 
                Point delta = new Point(
                    (mouseDragCurrentPoint.X > this.mouseDragStartPoint.X) ?
                    -(mouseDragCurrentPoint.X - this.mouseDragStartPoint.X) :
                    (this.mouseDragStartPoint.X - mouseDragCurrentPoint.X),
                    (mouseDragCurrentPoint.Y > this.mouseDragStartPoint.Y) ?
                    -(mouseDragCurrentPoint.Y - this.mouseDragStartPoint.Y) :
                    (this.mouseDragStartPoint.Y - mouseDragCurrentPoint.Y));

                // Scroll to the new position. 
                myScrollViewer.ScrollToHorizontalOffset(this.scrollStartOffset.X + delta.X);
                myScrollViewer.ScrollToVerticalOffset(this.scrollStartOffset.Y + delta.Y);
            }
            base.OnPreviewMouseMove(e);
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.Cursor = Cursors.Arrow;
                this.ReleaseMouseCapture();
            }

            Scroll(mouseDragStartPoint.Y, e.GetPosition(this).Y, mouseDownTime, DateTime.Now);

            base.OnPreviewMouseUp(e);
        }

        #endregion

        #region Properties
        public IEnumerable ItemsSource
        {
            get
            {
                return this.listItems.ItemsSource;
            }

            set
            {
                this.listItems.ItemsSource = value;
            }
        }
        #endregion


    }
}