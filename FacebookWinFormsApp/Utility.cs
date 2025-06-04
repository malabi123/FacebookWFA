using System;
using System.Drawing;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    internal static class Utility
    {
        internal static void SetImageInPictureBoxFromObject(PictureBox i_PictureBox, object i_Picture)
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

        internal static int CalculateAge(DateTime i_BirthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - i_BirthDate.Year;

            if (i_BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}