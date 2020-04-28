namespace crtcpl
{
    partial class ScreenPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenPage));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.contrastGroupBox = new System.Windows.Forms.GroupBox();
            this.constrastTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.brightnessGroupBox = new System.Windows.Forms.GroupBox();
            this.brightnessTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel.SuspendLayout();
            this.contrastGroupBox.SuspendLayout();
            this.constrastTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            this.brightnessGroupBox.SuspendLayout();
            this.brightnessTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.contrastGroupBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.brightnessGroupBox, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // contrastGroupBox
            // 
            resources.ApplyResources(this.contrastGroupBox, "contrastGroupBox");
            this.contrastGroupBox.Controls.Add(this.constrastTableLayoutPanel);
            this.contrastGroupBox.Name = "contrastGroupBox";
            this.contrastGroupBox.TabStop = false;
            // 
            // constrastTableLayoutPanel
            // 
            resources.ApplyResources(this.constrastTableLayoutPanel, "constrastTableLayoutPanel");
            this.constrastTableLayoutPanel.Controls.Add(this.pictureBox3, 0, 0);
            this.constrastTableLayoutPanel.Controls.Add(this.pictureBox4, 2, 0);
            this.constrastTableLayoutPanel.Controls.Add(this.contrastTrackBar, 1, 0);
            this.constrastTableLayoutPanel.Name = "constrastTableLayoutPanel";
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Image = global::crtcpl.ImageRes.ImageRes.RES004;
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.Image = global::crtcpl.ImageRes.ImageRes.RES003;
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // contrastTrackBar
            // 
            resources.ApplyResources(this.contrastTrackBar, "contrastTrackBar");
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.TickFrequency = 2;
            this.contrastTrackBar.Value = 10;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.contrastTrackBar_Scroll);
            // 
            // brightnessGroupBox
            // 
            resources.ApplyResources(this.brightnessGroupBox, "brightnessGroupBox");
            this.brightnessGroupBox.Controls.Add(this.brightnessTableLayoutPanel);
            this.brightnessGroupBox.Name = "brightnessGroupBox";
            this.brightnessGroupBox.TabStop = false;
            // 
            // brightnessTableLayoutPanel
            // 
            resources.ApplyResources(this.brightnessTableLayoutPanel, "brightnessTableLayoutPanel");
            this.brightnessTableLayoutPanel.Controls.Add(this.pictureBox1, 0, 0);
            this.brightnessTableLayoutPanel.Controls.Add(this.pictureBox2, 2, 0);
            this.brightnessTableLayoutPanel.Controls.Add(this.brightnessTrackBar, 1, 0);
            this.brightnessTableLayoutPanel.Name = "brightnessTableLayoutPanel";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::crtcpl.ImageRes.ImageRes.RES002;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::crtcpl.ImageRes.ImageRes.RES001;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // brightnessTrackBar
            // 
            resources.ApplyResources(this.brightnessTrackBar, "brightnessTrackBar");
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.TickFrequency = 2;
            this.brightnessTrackBar.Scroll += new System.EventHandler(this.brightnessTrackBar_Scroll);
            // 
            // ScreenPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ScreenPage";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.contrastGroupBox.ResumeLayout(false);
            this.contrastGroupBox.PerformLayout();
            this.constrastTableLayoutPanel.ResumeLayout(false);
            this.constrastTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            this.brightnessGroupBox.ResumeLayout(false);
            this.brightnessGroupBox.PerformLayout();
            this.brightnessTableLayoutPanel.ResumeLayout(false);
            this.brightnessTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox brightnessGroupBox;
        private System.Windows.Forms.TableLayoutPanel brightnessTableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TrackBar brightnessTrackBar;
        private System.Windows.Forms.GroupBox contrastGroupBox;
        private System.Windows.Forms.TableLayoutPanel constrastTableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TrackBar contrastTrackBar;
    }
}
