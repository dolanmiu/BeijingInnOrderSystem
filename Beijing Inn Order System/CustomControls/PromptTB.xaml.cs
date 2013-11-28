using System.Windows;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System.CustomControls
{
    /// <summary>
    /// Interaction logic for PromptTB.xaml
    /// </summary>
    public partial class PromptTB : TextBox
    {
        private string promptText;
        public static readonly DependencyProperty ContentTextProperty = DependencyProperty.Register("ContentText", typeof(string), typeof(PromptTB), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public PromptTB()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = promptText;
                return;
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == promptText)
            {
                ((TextBox)sender).Text = "";
                return;
            }

            if (string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                ((TextBox)sender).Text = promptText;
                return;
            }
        }

        public new void Clear()
        {
            Text = promptText;
        }

        public string PromptText
        {
            get
            {
                return promptText;
            }

            set
            {
                promptText = value;
                Text = value;
            }
        }

        public string ContentText
        {     
            get
            {
                if (Text == promptText)
                {
                    return "";
                }
                else
                {
                    return (string)GetValue(ContentTextProperty);
                    //return Text;
                }
            }

            set
            {
                SetValue(ContentTextProperty, value);
                //Text = value;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Text == promptText)
            {
                ContentText = "";
            }
            else
            {
                ContentText = Text;
            }
        }
    }
}
