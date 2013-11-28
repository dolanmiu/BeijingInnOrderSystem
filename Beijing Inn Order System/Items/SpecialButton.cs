using Beijing_Inn_Order_System.Items.SpecialDecoration;
using System;
using System.Windows.Controls;

namespace Beijing_Inn_Order_System.Items
{
    public class SpecialButton : Button
    {
        SpecialReqs reqs;
        public enum SpecialType { NoType, ExtraHot, LittleOil, LittleSalt, NoBeanSprouts, NoChilli, NoMSG, NoOnions, NoPeanuts, NoPeas, NoPork, NoShrimps, LittleChilli, NoVegetables, NoWaterChestnuts, SauceSeperate, Spicy, ExtraPancakes, NoSalt }
        private SpecialType type;

        public SpecialButton(string text, SpecialReqs reqs, SpecialType type)
        {
            this.Content = text;
            this.Click += click;
            this.reqs = reqs;
            this.type = type;

            this.Margin = new System.Windows.Thickness(10, 10, 0, 0);
            this.Padding = new System.Windows.Thickness(5, 5, 5, 5);
        }

        private void click(object sender, EventArgs e)
        {
            reqs.ToggleButton(this);
        }

        public bool IsEqualTo(SpecialType type)
        {
            if (this.type == type)
            {
                return true;
            }
            return false;
            /*if (name.ToLower() == Content.ToString().ToLower())
            {
                return true;
            }
            return false;*/

        }

        public SpecialComponent Decorate(SpecialComponent special)
        {
            SpecialComponent s;
            switch (type)
            {
                case SpecialType.ExtraHot:
                    s = new SpecialExtraHot(special);
                    return s;
                case SpecialType.LittleOil:
                    s = new SpecialLittleOil(special);
                    return s;
                case SpecialType.LittleSalt:
                    s = new SpecialLittleSalt(special);
                    return s;
                case SpecialType.NoBeanSprouts:
                    s = new SpecialNoBeansprouts(special);
                    return s;
                case SpecialType.NoChilli:
                    s = new SpecialNoChilli(special);
                    return s;
                case SpecialType.NoMSG:
                    s = new SpecialNoMSG(special);
                    return s;
                case SpecialType.NoOnions:
                    s = new SpecialNoOnions(special);
                    return s;
                case SpecialType.NoPeanuts:
                    s = new SpecialNoPeanuts(special);
                    return s;
                case SpecialType.NoPeas:
                    s = new SpecialNoPeas(special);
                    return s;
                case SpecialType.NoPork:
                    s = new SpecialNoPork(special);
                    return s;
                case SpecialType.NoShrimps:
                    s = new SpecialNoShrimps(special);
                    return s;
                case SpecialType.LittleChilli:
                    s = new SpecialLittleChilli(special);
                    return s;
                case SpecialType.NoVegetables:
                    s = new SpecialNoVegetables(special);
                    return s;
                case SpecialType.NoWaterChestnuts:
                    s = new SpecialNoWaterChestnuts(special);
                    return s;
                case SpecialType.SauceSeperate:
                    s = new SpecialSauceSeperate(special);
                    return s;
                case SpecialType.Spicy:
                    s = new SpecialSpicy(special);
                    return s;
                case SpecialType.ExtraPancakes:
                    s = new SpecialExtraPancakes(special);
                    return s;
                case SpecialType.NoSalt:
                    s = new SpecialNoSalt(special);
                    return s;
                default:
                    return special;
            }
        }

    }
}
