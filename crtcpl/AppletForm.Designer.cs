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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showTestPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testPatternSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.screenAdjustToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMPTEColorBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fuBKTestCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bWverticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bWhorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBverticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBhorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graybarsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSettingsanalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel.SuspendLayout();
            this.pagePanel.SuspendLayout();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
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
            this.tableLayoutPanel.SetColumnSpan(this.pagePanel, 4);
            this.pagePanel.Controls.Add(this.ADVANCED);
            this.pagePanel.Controls.Add(this.COLORS);
            this.pagePanel.Controls.Add(this.GEOMETRY);
            this.pagePanel.Controls.Add(this.SCREEN);
            resources.ApplyResources(this.pagePanel, "pagePanel");
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
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
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
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTestPatternToolStripMenuItem,
            this.testPatternSelectionToolStripMenuItem,
            this.showSettingsanalyzerToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // showTestPatternToolStripMenuItem
            // 
            this.showTestPatternToolStripMenuItem.Name = "showTestPatternToolStripMenuItem";
            resources.ApplyResources(this.showTestPatternToolStripMenuItem, "showTestPatternToolStripMenuItem");
            this.showTestPatternToolStripMenuItem.Click += new System.EventHandler(this.showTestPatternToolStripMenuItem_Click);
            // 
            // testPatternSelectionToolStripMenuItem
            // 
            this.testPatternSelectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.screenAdjustToolStripMenuItem,
            this.sMPTEColorBarToolStripMenuItem,
            this.fuBKTestCardToolStripMenuItem,
            this.bWverticalToolStripMenuItem,
            this.bWhorizontalToolStripMenuItem,
            this.rGBverticalToolStripMenuItem,
            this.rGBhorizontalToolStripMenuItem,
            this.redScreenToolStripMenuItem,
            this.greenScreenToolStripMenuItem,
            this.blueScreenToolStripMenuItem,
            this.graybarsToolStripMenuItem});
            this.testPatternSelectionToolStripMenuItem.Name = "testPatternSelectionToolStripMenuItem";
            resources.ApplyResources(this.testPatternSelectionToolStripMenuItem, "testPatternSelectionToolStripMenuItem");
            // 
            // screenAdjustToolStripMenuItem
            // 
            this.screenAdjustToolStripMenuItem.Checked = true;
            this.screenAdjustToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.screenAdjustToolStripMenuItem.Name = "screenAdjustToolStripMenuItem";
            resources.ApplyResources(this.screenAdjustToolStripMenuItem, "screenAdjustToolStripMenuItem");
            this.screenAdjustToolStripMenuItem.Tag = "0";
            this.screenAdjustToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // sMPTEColorBarToolStripMenuItem
            // 
            this.sMPTEColorBarToolStripMenuItem.Name = "sMPTEColorBarToolStripMenuItem";
            resources.ApplyResources(this.sMPTEColorBarToolStripMenuItem, "sMPTEColorBarToolStripMenuItem");
            this.sMPTEColorBarToolStripMenuItem.Tag = "1";
            this.sMPTEColorBarToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // fuBKTestCardToolStripMenuItem
            // 
            this.fuBKTestCardToolStripMenuItem.Name = "fuBKTestCardToolStripMenuItem";
            resources.ApplyResources(this.fuBKTestCardToolStripMenuItem, "fuBKTestCardToolStripMenuItem");
            this.fuBKTestCardToolStripMenuItem.Tag = "2";
            this.fuBKTestCardToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // bWverticalToolStripMenuItem
            // 
            this.bWverticalToolStripMenuItem.Name = "bWverticalToolStripMenuItem";
            resources.ApplyResources(this.bWverticalToolStripMenuItem, "bWverticalToolStripMenuItem");
            this.bWverticalToolStripMenuItem.Tag = "3";
            this.bWverticalToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // bWhorizontalToolStripMenuItem
            // 
            this.bWhorizontalToolStripMenuItem.Name = "bWhorizontalToolStripMenuItem";
            resources.ApplyResources(this.bWhorizontalToolStripMenuItem, "bWhorizontalToolStripMenuItem");
            this.bWhorizontalToolStripMenuItem.Tag = "4";
            this.bWhorizontalToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // rGBverticalToolStripMenuItem
            // 
            this.rGBverticalToolStripMenuItem.Name = "rGBverticalToolStripMenuItem";
            resources.ApplyResources(this.rGBverticalToolStripMenuItem, "rGBverticalToolStripMenuItem");
            this.rGBverticalToolStripMenuItem.Tag = "5";
            this.rGBverticalToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // rGBhorizontalToolStripMenuItem
            // 
            this.rGBhorizontalToolStripMenuItem.Name = "rGBhorizontalToolStripMenuItem";
            resources.ApplyResources(this.rGBhorizontalToolStripMenuItem, "rGBhorizontalToolStripMenuItem");
            this.rGBhorizontalToolStripMenuItem.Tag = "6";
            this.rGBhorizontalToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // redScreenToolStripMenuItem
            // 
            this.redScreenToolStripMenuItem.Name = "redScreenToolStripMenuItem";
            resources.ApplyResources(this.redScreenToolStripMenuItem, "redScreenToolStripMenuItem");
            this.redScreenToolStripMenuItem.Tag = "7";
            this.redScreenToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // greenScreenToolStripMenuItem
            // 
            this.greenScreenToolStripMenuItem.Name = "greenScreenToolStripMenuItem";
            resources.ApplyResources(this.greenScreenToolStripMenuItem, "greenScreenToolStripMenuItem");
            this.greenScreenToolStripMenuItem.Tag = "8";
            this.greenScreenToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // blueScreenToolStripMenuItem
            // 
            this.blueScreenToolStripMenuItem.Name = "blueScreenToolStripMenuItem";
            resources.ApplyResources(this.blueScreenToolStripMenuItem, "blueScreenToolStripMenuItem");
            this.blueScreenToolStripMenuItem.Tag = "9";
            this.blueScreenToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // graybarsToolStripMenuItem
            // 
            this.graybarsToolStripMenuItem.Name = "graybarsToolStripMenuItem";
            resources.ApplyResources(this.graybarsToolStripMenuItem, "graybarsToolStripMenuItem");
            this.graybarsToolStripMenuItem.Tag = "10";
            this.graybarsToolStripMenuItem.Click += new System.EventHandler(this.testPatternSelectionToolStripMenuItem_Click);
            // 
            // showSettingsanalyzerToolStripMenuItem
            // 
            this.showSettingsanalyzerToolStripMenuItem.Name = "showSettingsanalyzerToolStripMenuItem";
            resources.ApplyResources(this.showSettingsanalyzerToolStripMenuItem, "showSettingsanalyzerToolStripMenuItem");
            this.showSettingsanalyzerToolStripMenuItem.Click += new System.EventHandler(this.showSettingsanalyzerToolStripMenuItem_Click);
            // 
            // AppletForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ContextMenuStrip = this.contextMenuStrip;
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
            this.contextMenuStrip.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showTestPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testPatternSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenAdjustToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMPTEColorBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fuBKTestCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bWverticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bWhorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBverticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBhorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSettingsanalyzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graybarsToolStripMenuItem;
    }
}

