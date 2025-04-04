using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public partial class ChangingPictureBox : UserControl
    {
        private List<String> m_UrlsList = null;
        private int m_CurrentUrlIndex = 0;

        public ChangingPictureBox()
        {
            InitializeComponent();
            this.AutoSize = false;
        }

        public void SetUrlsList(List<String> i_UrlsList)
        {
            m_UrlsList = i_UrlsList;

            if (m_UrlsList.Count >= 2)
            {
                buttonLeft.Visible = true;
                buttonRight.Visible = true;
            }
            else
            {
                buttonLeft.Visible = false;
                buttonRight.Visible = false;
            }

            m_CurrentUrlIndex = 0;
            pictureBox1.ImageLocation = m_UrlsList[0];
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            m_CurrentUrlIndex = (m_CurrentUrlIndex + 1) % m_UrlsList.Count;
            updatePictureURL(m_UrlsList[m_CurrentUrlIndex]);
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            m_CurrentUrlIndex = (m_CurrentUrlIndex - 1) % m_UrlsList.Count;
            updatePictureURL(m_UrlsList[m_CurrentUrlIndex]);
        }

        private void updatePictureURL(string i_pictureUrl)
        {
            pictureBox1.ImageLocation = i_pictureUrl;

            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = Properties.Resources.No_image_available_svg;
            }
        }

        public void SetOnePictureURL(string i_pictureUrl)
        {
            m_UrlsList = null;
            updatePictureURL(i_pictureUrl);
            buttonLeft.Visible = false;
            buttonRight.Visible = false;
        }

        public void SetOnePictureImage(Image i_Image)
        {
            m_UrlsList = null;
            updatePictureImage(i_Image);
            buttonLeft.Visible = false;
            buttonRight.Visible = false;
        }

        private void updatePictureImage(Image i_Image)
        {
            pictureBox1.Image = i_Image;

            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = Properties.Resources.No_image_available_svg;
            }
        }

        public void Clear()
        {
            m_UrlsList = null;
            m_CurrentUrlIndex = 0;
            pictureBox1.Image = Properties.Resources.No_image_available_svg;
        }

        /*protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.DesignMode)
            {
                int width = this.Width - 12;
                int height = this.Height;
                buttonLeft.Width = width / 4;
                buttonLeft.Height = height / 3;
                buttonRight.Width = width / 4;
                buttonRight.Height = height / 3;
                pictureBox1.Width = width / 2;
                pictureBox1.Height = height;
                pictureBox1.Left = buttonLeft.Right + 6;
                buttonRight.Left = pictureBox1.Right + 6;

            }
        }*/
    }
}