using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassioneBrowserKit_Sample
{
    public partial class frmEdit : Form
    {
        public string strTargetName { get; set; }
        public string strTargetValue { get; set; }

        public frmEdit()
        {
            InitializeComponent();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            try
            {
                if (strTargetName == "#edForGetText")
                {
                    ucTextSingle uc = new ucTextSingle();
                    uc.textBox1.Text = strTargetValue;
                    panel1.Controls.Add(uc);
                }
                else if (strTargetName == "#edForGetVal")
                {
                    ucVal uc = new ucVal();
                    uc.numericUpDown1.Value = decimal.Parse(strTargetValue);
                    panel1.Controls.Add(uc);
                }
                else if (strTargetName == "#edForGetCss")
                {
                    ucWH uc = new ucWH();
                    uc.numericUpDown1.Value = decimal.Parse(strTargetValue.Split(',')[0].Trim().Replace("px", ""));
                    uc.numericUpDown2.Value = decimal.Parse(strTargetValue.Split(',')[1].Trim().Replace("px", ""));
                    panel1.Controls.Add(uc);
                }
                else if (strTargetName == "#edForGetHtml")
                {
                    ucTextMulti uc = new ucTextMulti();
                    uc.textBox1.Text = strTargetValue;
                    panel1.Controls.Add(uc);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
    }
}
