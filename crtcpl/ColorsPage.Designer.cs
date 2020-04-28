namespace crtcpl
{
    partial class ColorsPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorsPage));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.colorGradient = new crtcpl.ColorGradient();
            this.blueGroupBox = new System.Windows.Forms.GroupBox();
            this.blueTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.blueTrackBar = new System.Windows.Forms.TrackBar();
            this.greenGroupBox = new System.Windows.Forms.GroupBox();
            this.greenTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.greenTrackBar = new System.Windows.Forms.TrackBar();
            this.redGroupBox = new System.Windows.Forms.GroupBox();
            this.redTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.redTrackBar = new System.Windows.Forms.TrackBar();
            this.tableLayoutPanel.SuspendLayout();
            this.blueGroupBox.SuspendLayout();
            this.blueTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).BeginInit();
            this.greenGroupBox.SuspendLayout();
            this.greenTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).BeginInit();
            this.redGroupBox.SuspendLayout();
            this.redTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.colorGradient, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.blueGroupBox, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.greenGroupBox, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.redGroupBox, 0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // colorGradient
            // 
            resources.ApplyResources(this.colorGradient, "colorGradient");
            this.colorGradient.Name = "colorGradient";
            // 
            // blueGroupBox
            // 
            this.blueGroupBox.Controls.Add(this.blueTableLayoutPanel);
            resources.ApplyResources(this.blueGroupBox, "blueGroupBox");
            this.blueGroupBox.Name = "blueGroupBox";
            this.blueGroupBox.TabStop = false;
            // 
            // blueTableLayoutPanel
            // 
            resources.ApplyResources(this.blueTableLayoutPanel, "blueTableLayoutPanel");
            this.blueTableLayoutPanel.Controls.Add(this.blueTrackBar, 0, 0);
            this.blueTableLayoutPanel.Name = "blueTableLayoutPanel";
            // 
            // blueTrackBar
            // 
            resources.ApplyResources(this.blueTrackBar, "blueTrackBar");
            this.blueTrackBar.Maximum = 50;
            this.blueTrackBar.Name = "blueTrackBar";
            this.blueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.blueTrackBar.Scroll += new System.EventHandler(this.blueTrackBar_Scroll);
            // 
            // greenGroupBox
            // 
            this.greenGroupBox.Controls.Add(this.greenTableLayoutPanel);
            resources.ApplyResources(this.greenGroupBox, "greenGroupBox");
            this.greenGroupBox.Name = "greenGroupBox";
            this.greenGroupBox.TabStop = false;
            // 
            // greenTableLayoutPanel
            // 
            resources.ApplyResources(this.greenTableLayoutPanel, "greenTableLayoutPanel");
            this.greenTableLayoutPanel.Controls.Add(this.greenTrackBar, 0, 0);
            this.greenTableLayoutPanel.Name = "greenTableLayoutPanel";
            // 
            // greenTrackBar
            // 
            resources.ApplyResources(this.greenTrackBar, "greenTrackBar");
            this.greenTrackBar.Maximum = 50;
            this.greenTrackBar.Name = "greenTrackBar";
            this.greenTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.greenTrackBar.Scroll += new System.EventHandler(this.greenTrackBar_Scroll);
            // 
            // redGroupBox
            // 
            resources.ApplyResources(this.redGroupBox, "redGroupBox");
            this.redGroupBox.Controls.Add(this.redTableLayoutPanel);
            this.redGroupBox.Name = "redGroupBox";
            this.redGroupBox.TabStop = false;
            // 
            // redTableLayoutPanel
            // 
            resources.ApplyResources(this.redTableLayoutPanel, "redTableLayoutPanel");
            this.redTableLayoutPanel.Controls.Add(this.redTrackBar, 0, 0);
            this.redTableLayoutPanel.Name = "redTableLayoutPanel";
            // 
            // redTrackBar
            // 
            resources.ApplyResources(this.redTrackBar, "redTrackBar");
            this.redTrackBar.Maximum = 50;
            this.redTrackBar.Name = "redTrackBar";
            this.redTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.redTrackBar.Scroll += new System.EventHandler(this.redTrackBar_Scroll);
            // 
            // ColorsPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(355, 235);
            this.Name = "ColorsPage";
            this.tableLayoutPanel.ResumeLayout(false);
            this.blueGroupBox.ResumeLayout(false);
            this.blueGroupBox.PerformLayout();
            this.blueTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blueTrackBar)).EndInit();
            this.greenGroupBox.ResumeLayout(false);
            this.greenGroupBox.PerformLayout();
            this.greenTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.greenTrackBar)).EndInit();
            this.redGroupBox.ResumeLayout(false);
            this.redGroupBox.PerformLayout();
            this.redTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.redTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox greenGroupBox;
        private System.Windows.Forms.GroupBox redGroupBox;
        private System.Windows.Forms.GroupBox blueGroupBox;
        private System.Windows.Forms.TableLayoutPanel blueTableLayoutPanel;
        private System.Windows.Forms.TrackBar blueTrackBar;
        private System.Windows.Forms.TableLayoutPanel greenTableLayoutPanel;
        private System.Windows.Forms.TrackBar greenTrackBar;
        private System.Windows.Forms.TableLayoutPanel redTableLayoutPanel;
        private System.Windows.Forms.TrackBar redTrackBar;
        private ColorGradient colorGradient;
    }
}
