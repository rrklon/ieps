namespace IPSv1
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NewMentor = new System.Windows.Forms.PictureBox();
            this.UpdateInfo = new System.Windows.Forms.PictureBox();
            this.ViewDatabase = new System.Windows.Forms.PictureBox();
            this.AssignMentor = new System.Windows.Forms.PictureBox();
            this.ViewReport = new System.Windows.Forms.PictureBox();
            this.ExitProgram = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewMentor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignMentor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitProgram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // NewMentor
            // 
            resources.ApplyResources(this.NewMentor, "NewMentor");
            this.NewMentor.Name = "NewMentor";
            this.NewMentor.TabStop = false;
            this.NewMentor.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // UpdateInfo
            // 
            resources.ApplyResources(this.UpdateInfo, "UpdateInfo");
            this.UpdateInfo.Name = "UpdateInfo";
            this.UpdateInfo.TabStop = false;
            this.UpdateInfo.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // ViewDatabase
            // 
            resources.ApplyResources(this.ViewDatabase, "ViewDatabase");
            this.ViewDatabase.Name = "ViewDatabase";
            this.ViewDatabase.TabStop = false;
            // 
            // AssignMentor
            // 
            resources.ApplyResources(this.AssignMentor, "AssignMentor");
            this.AssignMentor.Name = "AssignMentor";
            this.AssignMentor.TabStop = false;
            this.AssignMentor.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // ViewReport
            // 
            resources.ApplyResources(this.ViewReport, "ViewReport");
            this.ViewReport.Name = "ViewReport";
            this.ViewReport.TabStop = false;
            this.ViewReport.Click += new System.EventHandler(this.ViewReport_Click);
            // 
            // ExitProgram
            // 
            resources.ApplyResources(this.ExitProgram, "ExitProgram");
            this.ExitProgram.Name = "ExitProgram";
            this.ExitProgram.TabStop = false;
            this.ExitProgram.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.pictureBox8, "pictureBox8");
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.TabStop = false;
            // 
            // MainMenu
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.ExitProgram);
            this.Controls.Add(this.ViewReport);
            this.Controls.Add(this.AssignMentor);
            this.Controls.Add(this.ViewDatabase);
            this.Controls.Add(this.UpdateInfo);
            this.Controls.Add(this.NewMentor);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainMenu";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewMentor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AssignMentor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitProgram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox NewMentor;
        private System.Windows.Forms.PictureBox UpdateInfo;
        private System.Windows.Forms.PictureBox ViewDatabase;
        private System.Windows.Forms.PictureBox AssignMentor;
        private System.Windows.Forms.PictureBox ViewReport;
        private System.Windows.Forms.PictureBox ExitProgram;
        private System.Windows.Forms.PictureBox pictureBox8;
    }
}

