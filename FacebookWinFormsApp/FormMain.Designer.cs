namespace BasicFacebookFeatures
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonLogout = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxNewPost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.changingPictureBox1 = new BasicFacebookFeatures.ChangingPictureBox();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.listBoxUserPosts = new System.Windows.Forms.ListBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelHomeTown = new System.Windows.Forms.Label();
            this.buttonEditProfile = new System.Windows.Forms.Button();
            this.labelStudyPlace = new System.Windows.Forms.Label();
            this.labelGender = new System.Windows.Forms.Label();
            this.labelWorkPlace = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonPostStatus = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(364, 167);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(325, 32);
            this.buttonLogin.TabIndex = 36;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonLogout
            // 
            this.buttonLogout.Enabled = false;
            this.buttonLogout.Location = new System.Drawing.Point(9, 113);
            this.buttonLogout.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLogout.Name = "buttonLogout";
            this.buttonLogout.Size = new System.Drawing.Size(187, 34);
            this.buttonLogout.TabIndex = 52;
            this.buttonLogout.Text = "Logout";
            this.buttonLogout.UseVisualStyleBackColor = true;
            this.buttonLogout.Visible = false;
            this.buttonLogout.Click += new System.EventHandler(this.buttonLogout_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 723);
            this.tabControl1.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.buttonLogin);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1081, 688);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.buttonPostStatus);
            this.panel3.Controls.Add(this.textBoxNewPost);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.changingPictureBox1);
            this.panel3.Controls.Add(this.pictureBoxProfile);
            this.panel3.Controls.Add(this.listBoxUserPosts);
            this.panel3.Controls.Add(this.labelUserName);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Location = new System.Drawing.Point(3, 217);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1075, 471);
            this.panel3.TabIndex = 65;
            // 
            // textBoxNewPost
            // 
            this.textBoxNewPost.Location = new System.Drawing.Point(122, 369);
            this.textBoxNewPost.Multiline = true;
            this.textBoxNewPost.Name = "textBoxNewPost";
            this.textBoxNewPost.Size = new System.Drawing.Size(381, 69);
            this.textBoxNewPost.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 372);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 71;
            this.label1.Text = "Post Status:";
            // 
            // changingPictureBox1
            // 
            this.changingPictureBox1.Location = new System.Drawing.Point(647, 22);
            this.changingPictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.changingPictureBox1.Name = "changingPictureBox1";
            this.changingPictureBox1.Size = new System.Drawing.Size(375, 210);
            this.changingPictureBox1.TabIndex = 70;
            // 
            // pictureBoxProfile
            // 
            this.pictureBoxProfile.Location = new System.Drawing.Point(5, 44);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(350, 299);
            this.pictureBoxProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProfile.TabIndex = 55;
            this.pictureBoxProfile.TabStop = false;
            // 
            // listBoxUserPosts
            // 
            this.listBoxUserPosts.FormattingEnabled = true;
            this.listBoxUserPosts.ItemHeight = 22;
            this.listBoxUserPosts.Location = new System.Drawing.Point(647, 239);
            this.listBoxUserPosts.Name = "listBoxUserPosts";
            this.listBoxUserPosts.Size = new System.Drawing.Size(375, 202);
            this.listBoxUserPosts.TabIndex = 69;
            this.listBoxUserPosts.SelectedIndexChanged += new System.EventHandler(this.listBoxUserPosts_SelectedIndexChanged);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("Arial Narrow", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUserName.Location = new System.Drawing.Point(7, 1);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(212, 40);
            this.labelUserName.TabIndex = 56;
            this.labelUserName.Text = "Username, age";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelHomeTown);
            this.panel2.Controls.Add(this.buttonEditProfile);
            this.panel2.Controls.Add(this.labelStudyPlace);
            this.panel2.Controls.Add(this.labelGender);
            this.panel2.Controls.Add(this.labelWorkPlace);
            this.panel2.Controls.Add(this.labelBirthday);
            this.panel2.Location = new System.Drawing.Point(361, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(207, 264);
            this.panel2.TabIndex = 67;
            // 
            // labelHomeTown
            // 
            this.labelHomeTown.AutoSize = true;
            this.labelHomeTown.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHomeTown.Location = new System.Drawing.Point(3, 0);
            this.labelHomeTown.Name = "labelHomeTown";
            this.labelHomeTown.Size = new System.Drawing.Size(124, 26);
            this.labelHomeTown.TabIndex = 58;
            this.labelHomeTown.Text = "HomeTown";
            // 
            // buttonEditProfile
            // 
            this.buttonEditProfile.Location = new System.Drawing.Point(0, 195);
            this.buttonEditProfile.Name = "buttonEditProfile";
            this.buttonEditProfile.Size = new System.Drawing.Size(142, 34);
            this.buttonEditProfile.TabIndex = 64;
            this.buttonEditProfile.Text = "Edit Profile";
            this.buttonEditProfile.UseVisualStyleBackColor = true;
            // 
            // labelStudyPlace
            // 
            this.labelStudyPlace.AutoSize = true;
            this.labelStudyPlace.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStudyPlace.Location = new System.Drawing.Point(3, 108);
            this.labelStudyPlace.Name = "labelStudyPlace";
            this.labelStudyPlace.Size = new System.Drawing.Size(112, 26);
            this.labelStudyPlace.TabIndex = 66;
            this.labelStudyPlace.Text = "Studies at";
            // 
            // labelGender
            // 
            this.labelGender.AutoSize = true;
            this.labelGender.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGender.Location = new System.Drawing.Point(3, 27);
            this.labelGender.Name = "labelGender";
            this.labelGender.Size = new System.Drawing.Size(88, 26);
            this.labelGender.TabIndex = 60;
            this.labelGender.Text = "Gender";
            // 
            // labelWorkPlace
            // 
            this.labelWorkPlace.AutoSize = true;
            this.labelWorkPlace.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWorkPlace.Location = new System.Drawing.Point(3, 81);
            this.labelWorkPlace.Name = "labelWorkPlace";
            this.labelWorkPlace.Size = new System.Drawing.Size(121, 26);
            this.labelWorkPlace.TabIndex = 65;
            this.labelWorkPlace.Text = "Workplace";
            // 
            // labelBirthday
            // 
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBirthday.Location = new System.Drawing.Point(3, 54);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(94, 26);
            this.labelBirthday.TabIndex = 59;
            this.labelBirthday.Text = "Birthday";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(84)))), ((int)(((byte)(157)))));
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.buttonLogout);
            this.panel1.Location = new System.Drawing.Point(0, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 160);
            this.panel1.TabIndex = 63;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BasicFacebookFeatures.Properties.Resources.facebookLogo;
            this.pictureBox1.Location = new System.Drawing.Point(314, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(424, 154);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 64;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1081, 671);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonPostStatus
            // 
            this.buttonPostStatus.Location = new System.Drawing.Point(509, 369);
            this.buttonPostStatus.Name = "buttonPostStatus";
            this.buttonPostStatus.Size = new System.Drawing.Size(90, 69);
            this.buttonPostStatus.TabIndex = 73;
            this.buttonPostStatus.Text = "Post";
            this.buttonPostStatus.UseVisualStyleBackColor = true;
            this.buttonPostStatus.Click += new System.EventHandler(this.buttonPostStatus_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 723);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

		#endregion
		private System.Windows.Forms.Button buttonLogin;
		private System.Windows.Forms.Button buttonLogout;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelHomeTown;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.Label labelGender;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonEditProfile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelStudyPlace;
        private System.Windows.Forms.Label labelWorkPlace;
        private System.Windows.Forms.ListBox listBoxUserPosts;
        private System.Windows.Forms.Panel panel3;
        private ChangingPictureBox changingPictureBox1;
        private System.Windows.Forms.TextBox textBoxNewPost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonPostStatus;
    }
}

