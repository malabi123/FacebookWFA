using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public partial class ScrollableLeftRightPictureBox: UserControl
    {
        private List<String> m_UrlsList = null;
        private int m_CurrentUrlIndex = 0;

        public ScrollableLeftRightPictureBox()
        {
            InitializeComponent();
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
            pictureBoxCurrentUrl.ImageLocation = m_UrlsList[0];
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            m_CurrentUrlIndex = (m_CurrentUrlIndex + 1) % m_UrlsList.Count;
            updatePicture();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            m_CurrentUrlIndex = (m_CurrentUrlIndex - 1) % m_UrlsList.Count;
            updatePicture();

        }

        private void updatePicture()
        {
            try
            {
                pictureBoxCurrentUrl.ImageLocation = m_UrlsList[m_CurrentUrlIndex];
            }
            catch (Exception ex)
            {
                pictureBoxCurrentUrl.Image = Properties.Resources.No_image_available_svg;
            }
        }

        public void Clear()
        {
            m_UrlsList = null;
            m_CurrentUrlIndex = 0;
            pictureBoxCurrentUrl.Image = Properties.Resources.No_image_available_svg;
        }

        /*public void ChangeSize(int i_Size)
        {
            this.buttonLeft.Size = new Size(i_Size, i_Size);
            this.buttonRight.Size = new Size(i_Size, i_Size);
            this.pictureBoxCurrentUrl.Size = new Size(5 * i_Size, 5 * i_Size);

            this.pictureBoxCurrentUrl.Left = this.buttonLeft.Right + 10;
            this.buttonRight.Left = this.pictureBoxCurrentUrl.Right + 10;

        }*/
    }
}
