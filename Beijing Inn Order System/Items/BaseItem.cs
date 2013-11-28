using Beijing_Inn_Order_System.Helper_Classes;
using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public abstract class BaseItem : INotifyPropertyChanged
    {
        protected String englishName;
        protected String chineseName;
        protected SpecialComponent itemProperties;
        protected string extraDetails = "";
        protected int number;

        private readonly List<Delegate> _serializableDelegates;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public BaseItem(String englishName, String chineseName, int number)
        {
            this.englishName = englishName;
            this.chineseName = chineseName;
            this.number = number;
            _serializableDelegates = new List<Delegate>();
        }

        protected bool IsPropertiesTheSameAs(IItem item)
        {
            List<SpecialComponent> properties = this.GetPropertyList();
            List<SpecialComponent> otherItemProperties = item.GetPropertyList();
            if (IsPropertyListEqual(properties, otherItemProperties) && IsExtraDetailsEqual(extraDetails, item.ExtraDetails))
            {
                return true;
            }
            return false;
        }

        public List<SpecialComponent> GetPropertyList()
        {
            List<SpecialComponent> components = new List<SpecialComponent>();
            SpecialComponent currentComponent = itemProperties;
            if (currentComponent == null) return null;

            components.Add(currentComponent);
            while (currentComponent.BaseComponent() != null)
            {
                currentComponent = currentComponent.BaseComponent();
                components.Add(currentComponent);
            }
            return components;
        }

        private bool IsExtraDetailsEqual(string extraDetails1, string extraDetails2)
        {
            if (extraDetails1.Trim().ToLower() == extraDetails2.Trim().ToLower())
            {
                return true;
            }
            return false;
        }

        private bool IsPropertyListEqual(List<SpecialComponent> _list1, List<SpecialComponent> _list2)
        {
            //List<SpecialComponent> properties1 = Helper.DeepClone<List<SpecialComponent>
            if (_list1 == null && _list2 == null) return true;
            if (_list1 == null || _list2 == null) return false;
            if (_list1.Count != _list2.Count) return false;

            List<SpecialComponent> list1 = Helper.DeepClone<List<SpecialComponent>>(_list1);
            List<SpecialComponent> list2 = Helper.DeepClone<List<SpecialComponent>>(_list2);

            foreach (SpecialComponent sc in list1)
            {
                foreach (SpecialComponent special in list2)
                {
                    if (special.GetSpecialType() == sc.GetSpecialType())
                    {
                        list2.Remove(special);
                        break;
                    }
                }
                /*if (!list2.Contains(sc))
                {
                    return false;
                }*/
            }

            if (list2.Count > 0)
            {
                return false;
            }
            return true;
        }

        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            _serializableDelegates.Clear();
            var handler = PropertyChanged;

            if (handler != null)
            {
                foreach (var invocation in handler.GetInvocationList())
                {
                    if (invocation.Target.GetType().IsSerializable)
                    {
                        _serializableDelegates.Add(invocation);
                    }
                }
            }
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            foreach (var invocation in _serializableDelegates)
            {
                PropertyChanged += (PropertyChangedEventHandler)invocation;
            }
        }

        #region Properties
        public string NumberedEnglishName
        {
            get
            {
                return number + " " + englishName;
            }
        }

        public string NumberedChineseName
        {
            get
            {
                return number + " " + chineseName;
            }
        }

        public String EnglishName
        {
            get
            {
                return englishName;
            }

            set
            {
                englishName = value;
            }
        }

        public String ChineseName
        {
            get
            {
                return chineseName;
            }
        }

        public SpecialComponent Properties
        {
            get
            {
                return itemProperties;
            }
            set
            {
                itemProperties = value;
                NotifyPropertyChanged("ConcatProperties");
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public string ConcatProperties
        {
            get
            {
                if (itemProperties == null) return "";
                return itemProperties.GetEnglishValue() + ExtraDetails;
            }
        }

        public string ExtraDetails
        {
            get
            {
                return extraDetails;
            }

            set
            {
                extraDetails = value;
            }
        }
        #endregion
    }
}
