
namespace BasicFacebookFeatures
{
    partial class ScrollableLeftRightPictureBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.pictureBoxCurrentUrl = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentUrl)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLeft
            // 
            this.buttonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLeft.BackColor = System.Drawing.Color.Transparent;
            this.buttonLeft.BackgroundImage = global::BasicFacebookFeatures.Properties.Resources.leftArrow;
            this.buttonLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLeft.Location = new System.Drawing.Point(0, 151);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(50, 50);
            this.buttonLeft.TabIndex = 3;
            this.buttonLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLeft.UseVisualStyleBackColor = false;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.BackColor = System.Drawing.Color.Transparent;
            this.buttonRight.BackgroundImage = global::BasicFacebookFeatures.Properties.Resources.rightArrow;
            this.buttonRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRight.Location = new System.Drawing.Point(250, 151);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = this.buttonLeft.Size;
            this.buttonRight.TabIndex = 2;
            this.buttonRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRight.UseVisualStyleBackColor = false;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // pictureBoxCurrentUrl
            // 
            this.pictureBoxCurrentUrl.Image = global::BasicFacebookFeatures.Properties.Resources.No_image_available_svg;
            this.pictureBoxCurrentUrl.Location = new System.Drawing.Point(50, 0);
            this.pictureBoxCurrentUrl.Name = "pictureBoxCurrentUrl";
            this.pictureBoxCurrentUrl.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxCurrentUrl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCurrentUrl.TabIndex = 1;
            this.pictureBoxCurrentUrl.TabStop = false;
            // 
            // ScrollableLeftRightPictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.pictureBoxCurrentUrl);
            this.Name = "ScrollableLeftRightPictureBox";
            this.Size = new System.Drawing.Size(300, 201);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCurrentUrl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxCurrentUrl;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
    }
}
