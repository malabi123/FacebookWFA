using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    internal static class Utility
    {
        internal static void setImageInPictureBoxFromObject(PictureBox i_PictureBox, object i_Picture)
        {
            string pictureUrl = i_Picture as string;

            if (pictureUrl != null)
            {
                i_PictureBox.ImageLocation = pictureUrl;
            }
            else
            {
                Image image = i_Picture as Image;

                if (image != null)
                {
                    i_PictureBox.Image = image;
                }
                else
                {
                    throw new Exception("Object Must Be string or Image");
                }
            }
        }
    }
}
