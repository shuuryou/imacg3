namespace crtcpl
{
    partial class AppletForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppletForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.ADVANCED = new crtcpl.AdvancedPage();
            this.COLORS = new crtcpl.ColorsPage();
            this.GEOMETRY = new crtcpl.GeometryPage();
            this.SCREEN = new crtcpl.ScreenPage();
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.defaultsButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.screenRadioButton = new System.Windows.Forms.RadioButton();
            this.geometryRadioButton = new System.Windows.Forms.RadioButton();
            this.colorsRadioButton = new System.Windows.Forms.RadioButton();
            this.advancedRadioButton = new System.Windows.Forms.RadioButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.pagePanel.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.pagePanel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonTableLayoutPanel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.screenRadioButton, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.geometryRadioButton, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.colorsRadioButton, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.advancedRadioButton, 3, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // pagePanel
            // 
            resources.ApplyResources(this.pagePanel, "pagePanel");
            this.tableLayoutPanel.SetColumnSpan(this.pagePanel, 4);
            this.pagePanel.Controls.Add(this.ADVANCED);
            this.pagePanel.Controls.Add(this.COLORS);
            this.pagePanel.Controls.Add(this.GEOMETRY);
            this.pagePanel.Controls.Add(this.SCREEN);
            this.pagePanel.Name = "pagePanel";
            // 
            // ADVANCED
            // 
            resources.ApplyResources(this.ADVANCED, "ADVANCED");
            this.ADVANCED.Name = "ADVANCED";
            // 
            // COLORS
            // 
            resources.ApplyResources(this.COLORS, "COLORS");
            this.COLORS.Name = "COLORS";
            this.COLORS.ColorChanged += new System.EventHandler<crtcpl.ColorsPageEventArgs>(this.COLORS_ColorChanged);
            // 
            // GEOMETRY
            // 
            resources.ApplyResources(this.GEOMETRY, "GEOMETRY");
            this.GEOMETRY.Name = "GEOMETRY";
            this.GEOMETRY.GeometryChanged += new System.EventHandler<crtcpl.GeometryPageEventArgs>(this.GEOMETRY_GeometryChanged);
            // 
            // SCREEN
            // 
            resources.ApplyResources(this.SCREEN, "SCREEN");
            this.SCREEN.Name = "SCREEN";
            this.SCREEN.BrightnessChanged += new System.EventHandler<crtcpl.ScreenPageEventArgs>(this.SCREEN_BrightnessChanged);
            this.SCREEN.ContrastChanged += new System.EventHandler<crtcpl.ScreenPageEventArgs>(this.SCREEN_ContrastChanged);
            // 
            // buttonTableLayoutPanel
            // 
            resources.ApplyResources(this.buttonTableLayoutPanel, "buttonTableLayoutPanel");
            this.tableLayoutPanel.SetColumnSpan(this.buttonTableLayoutPanel, 4);
            this.buttonTableLayoutPanel.Controls.Add(this.defaultsButton, 0, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.okButton, 1, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.cancelButton, 2, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.applyButton, 3, 0);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            // 
            // defaultsButton
            // 
            resources.ApplyResources(this.defaultsButton, "defaultsButton");
            this.defaultsButton.Name = "defaultsButton";
            this.defaultsButton.UseVisualStyleBackColor = true;
            this.defaultsButton.Click += new System.EventHandler(this.defaultsButton_Click);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            resources.ApplyResources(this.applyButton, "applyButton");
            this.applyButton.Name = "applyButton";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // screenRadioButton
            // 
            resources.ApplyResources(this.screenRadioButton, "screenRadioButton");
            this.screenRadioButton.Image = global::crtcpl.ImageRes.ImageRes.TAB1;
            this.screenRadioButton.Name = "screenRadioButton";
            this.screenRadioButton.TabStop = true;
            this.screenRadioButton.UseVisualStyleBackColor = true;
            this.screenRadioButton.CheckedChanged += new System.EventHandler(this.screenRadioButton_CheckedChanged);
            // 
            // geometryRadioButton
            // 
            resources.ApplyResources(this.geometryRadioButton, "geometryRadioButton");
            this.geometryRadioButton.Image = global::crtcpl.ImageRes.ImageRes.TAB2;
            this.geometryRadioButton.Name = "geometryRadioButton";
            this.geometryRadioButton.TabStop = true;
            this.geometryRadioButton.UseVisualStyleBackColor = true;
            this.geometryRadioButton.CheckedChanged += new System.EventHandler(this.geometryRadioButton_CheckedChanged);
            // 
            // colorsRadioButton
            // 
            resources.ApplyResources(this.colorsRadioButton, "colorsRadioButton");
            this.colorsRadioButton.Image = global::crtcpl.ImageRes.ImageRes.TAB3;
            this.colorsRadioButton.Name = "colorsRadioButton";
            this.colorsRadioButton.TabStop = true;
            this.colorsRadioButton.UseVisualStyleBackColor = true;
            this.colorsRadioButton.CheckedChanged += new System.EventHandler(this.colorsRadioButton_CheckedChanged);
            // 
            // advancedRadioButton
            // 
            resources.ApplyResources(this.advancedRadioButton, "advancedRadioButton");
            this.advancedRadioButton.Image = global::crtcpl.ImageRes.ImageRes.TAB4;
            this.advancedRadioButton.Name = "advancedRadioButton";
            this.advancedRadioButton.TabStop = true;
            this.advancedRadioButton.UseVisualStyleBackColor = true;
            this.advancedRadioButton.CheckedChanged += new System.EventHandler(this.advancedRadioButton_CheckedChanged);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "TAB1");
            this.imageList.Images.SetKeyName(1, "TAB2");
            this.imageList.Images.SetKeyName(2, "TAB3");
            this.imageList.Images.SetKeyName(3, "TAB4");
            // 
            // AppletForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppletForm";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Applet_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.pagePanel.ResumeLayout(false);
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton advancedRadioButton;
        private System.Windows.Forms.RadioButton colorsRadioButton;
        private System.Windows.Forms.RadioButton geometryRadioButton;
        private System.Windows.Forms.RadioButton screenRadioButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel pagePanel;
        private AdvancedPage ADVANCED;
        private ColorsPage COLORS;
        private GeometryPage GEOMETRY;
        private ScreenPage SCREEN;
        private System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;
        private System.Windows.Forms.Button defaultsButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
    }
}

