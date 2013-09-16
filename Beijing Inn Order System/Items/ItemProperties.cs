using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijing_Inn_Order_System.Items
{
    [Serializable]
    public class ItemProperties
    {
        /*private bool noPeas;
        private bool noOnions;
        private bool noChilli;
        private bool noPork;
        private bool noShrimps;
        private bool spicy;
        private bool noVegetables;
        private bool noBeansprouts;
        private bool extraHot;
        private bool littleOil;
        private bool noWaterChestnuts;
        private bool noPeanuts;
        private bool sauceSeperate;
        private bool noMSG;
        private bool littleSalt;
        private bool notTooSpicy;*/
        SpecialComponent special;


        public ItemProperties()
        {

        }

        #region Properties
        public SpecialComponent Special
        {
            get
            {
                return special;
            }
            set
            {
                special = value;
            }
        }
        /*
        public bool NoPeas
        {
            get
            {
                return noPeas;
            }
            set
            {
                noPeas = value;
            }
        }

        public bool NoOnions
        {
            get
            {
                return noOnions;
            }
            set
            {
                noOnions = value;
            }
        }

        public bool NoChilli
        {
            get
            {
                return noChilli;
            }
            set
            {
                noChilli = value;
            }
        }

        public bool NoPork
        {
            get
            {
                return noPork;
            }
            set
            {
                noPork = value;
            }
        }

        public bool NoShrimps
        {
            get
            {
                return noShrimps;
            }
            set
            {
                noShrimps = value;
            }
        }

        public bool Spicy
        {
            get
            {
                return spicy;
            }
            set
            {
                spicy = value;
            }
        }

        public bool NoVegetables
        {
            get
            {
                return noVegetables;
            }
            set
            {
                noVegetables = value;
            }
        }

        public bool NoBeansprouts
        {
            get
            {
                return noBeansprouts;
            }
            set
            {
                noBeansprouts = value;
            }
        }

        public bool ExtraHot
        {
            get
            {
                return extraHot;
            }
            set
            {
                extraHot = value;
            }
        }

        public bool LittleOil
        {
            get
            {
                return littleOil;
            }
            set
            {
                littleOil = value;
            }
        }

        public bool NoWaterChestnuts
        {
            get
            {
                return noWaterChestnuts;
            }
            set
            {
                noWaterChestnuts = value;
            }
        }

        public bool NoPeanuts
        {
            get
            {
                return noPeanuts;
            }
            set
            {
                noPeanuts = value;
            }
        }

        public bool SauceSeperate
        {
            get
            {
                return sauceSeperate;
            }
            set
            {
                sauceSeperate = value;
            }
        }

        public bool NoMSG
        {
            get
            {
                return noMSG;
            }
            set
            {
                noMSG = value;
            }
        }

        public bool LittleSalt
        {
            get
            {
                return littleSalt;
            }
            set
            {
                littleSalt = value;
            }
        }

        public bool NotTooSpicy
        {
            get
            {
                return notTooSpicy;
            }
            set
            {
                notTooSpicy = value;
            }
        }*/
        #endregion
         
    }
}
