using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beijing_Inn_Order_System.Printing
{
    public static class TextBitmap
    {
        public static Bitmap Convert(string text, int fontsize, int width, int height) 
        {
            Font font = new Font("MS Gothic", fontsize);
            SizeF hT = TextRenderer.MeasureText(text, font);
            Bitmap bmp = new Bitmap((int)hT.Width, (int)hT.Height);
            //FromImage method creates a new Graphics from the specified Image.
            Graphics graphics = Graphics.FromImage(bmp);
            // Create the Font object for the image text drawing.
            // Instantiating object of Bitmap image again with the correct size for the text and font.
            SizeF stringSize = graphics.MeasureString(text, font);
            bmp = new Bitmap(bmp, (int)stringSize.Width, (int)stringSize.Height);
            graphics = Graphics.FromImage(bmp);

            /* It can also be a way
            bmp = new Bitmap(bmp, new Size((int)graphics.MeasureString(txt, font).Width, (int)graphics.MeasureString(txt, font).Height));*/

            //Draw Specified text with specified format
            graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
            graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            graphics.DrawString(text, font, Brushes.Black, 0, 0);
            font.Dispose();
            graphics.Flush();
            graphics.Dispose();
            bmp.Save("file.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            int g = bmp.Width;
            return bmp;     //return Bitmap Image 
        }
    }
}
