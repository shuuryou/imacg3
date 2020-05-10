using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace crtcpl
{
    public partial class SettingsAnalyzerForm : Form
    {
        public SettingsAnalyzerForm()
        {
            InitializeComponent();

            listView.Items.Add("CONFIG_OFFSET_CONTRAST");
            listView.Items.Add("CONFIG_OFFSET_HORIZONTAL_POS");
            listView.Items.Add("CONFIG_OFFSET_HEIGHT");
            listView.Items.Add("CONFIG_OFFSET_VERTICAL_POS");
            listView.Items.Add("CONFIG_OFFSET_KEYSTONE");
            listView.Items.Add("CONFIG_OFFSET_PINCUSHION");
            listView.Items.Add("CONFIG_OFFSET_WIDTH");
            listView.Items.Add("CONFIG_OFFSET_PARALLELOGRAM");
            listView.Items.Add("CONFIG_OFFSET_BRIGHTNESS");
            listView.Items.Add("CONFIG_OFFSET_ROTATION");
            listView.Items.Add("CONFIG_OFFSET_RED");
            listView.Items.Add("CONFIG_OFFSET_GREEN");
            listView.Items.Add("CONFIG_OFFSET_BLUE");
            listView.Items.Add("CONFIG_OFFSET_RESERVED1");
            listView.Items.Add("CONFIG_OFFSET_RESERVED2");
            listView.Items.Add("CONFIG_OFFSET_RESERVED3");
            listView.Items.Add("CONFIG_OFFSET_RESERVED4");
            listView.Items.Add("CONFIG_OFFSET_RESERVED5");
            listView.Items.Add("CONFIG_OFFSET_RESERVED6");
            listView.Items.Add("CONFIG_OFFSET_RESERVED6");
            listView.Items.Add("CONFIG_OFFSET_CHECKSUM");

            foreach (ListViewItem item in listView.Items)
            {
                item.SubItems.Add("?");
                item.SubItems.Add("?");
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Trace.Assert(UCCom.IsOpen, "UCCom has no connection at the moment!");

            refreshButton.Enabled = false;
            this.UseWaitCursor = true;
            Application.DoEvents();
            
            byte[] sram;

            try
            {
                sram = UCCom.SendCommand(2, 0, 0);
            }
            catch (UCComException ex)
            {
                MessageBox.Show(this, string.Format(CultureInfo.CurrentCulture, StringRes.StringRes.CantUpdatePages,
                    ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

                for (int i = 0; i < listView.Items.Count; i++)
                {
                    listView.Items[i].SubItems[0].Text = "?";
                    listView.Items[i].SubItems[1].Text = "?";
                }

                goto end;
            }

            listView.BeginUpdate();

            for (int i = 0; i < sram.Length; i++)
            {
                listView.Items[i].SubItems[0].Text = string.Format(CultureInfo.InvariantCulture, "0x{0:X2}", sram[i]);
                listView.Items[i].SubItems[1].Text = string.Format(CultureInfo.InvariantCulture, "{0}", sram[i]);
            }
            listView.EndUpdate();

        end:
            refreshButton.Enabled = true;
            this.UseWaitCursor = false;
        }
    }
}
