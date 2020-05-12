namespace crtcpl
{
    partial class AdvancedPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedPage));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.comPortLabel = new System.Windows.Forms.Label();
            this.comPortComboBox = new System.Windows.Forms.ComboBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.rateLabel = new System.Windows.Forms.Label();
            this.rateComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(this.tableLayoutPanel, "tableLayoutPanel");
            this.tableLayoutPanel.Controls.Add(this.comPortLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.comPortComboBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.connectButton, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.disconnectButton, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.rateLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.rateComboBox, 1, 1);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            // 
            // comPortLabel
            // 
            resources.ApplyResources(this.comPortLabel, "comPortLabel");
            this.comPortLabel.Name = "comPortLabel";
            // 
            // comPortComboBox
            // 
            resources.ApplyResources(this.comPortComboBox, "comPortComboBox");
            this.tableLayoutPanel.SetColumnSpan(this.comPortComboBox, 2);
            this.comPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comPortComboBox.FormattingEnabled = true;
            this.comPortComboBox.Name = "comPortComboBox";
            this.comPortComboBox.SelectedIndexChanged += new System.EventHandler(this.comPortComboBox_SelectedIndexChanged);
            // 
            // connectButton
            // 
            resources.ApplyResources(this.connectButton, "connectButton");
            this.connectButton.Name = "connectButton";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            resources.ApplyResources(this.disconnectButton, "disconnectButton");
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // rateLabel
            // 
            resources.ApplyResources(this.rateLabel, "rateLabel");
            this.rateLabel.Name = "rateLabel";
            // 
            // rateComboBox
            // 
            resources.ApplyResources(this.rateComboBox, "rateComboBox");
            this.tableLayoutPanel.SetColumnSpan(this.rateComboBox, 2);
            this.rateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rateComboBox.FormattingEnabled = true;
            this.rateComboBox.Name = "rateComboBox";
            // 
            // AdvancedPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(303, 31);
            this.Name = "AdvancedPage";
            this.Load += new System.EventHandler(this.AdvancedPage_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label comPortLabel;
        private System.Windows.Forms.ComboBox comPortComboBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label rateLabel;
        private System.Windows.Forms.ComboBox rateComboBox;
    }
}
