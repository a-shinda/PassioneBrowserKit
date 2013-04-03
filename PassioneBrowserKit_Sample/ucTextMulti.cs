using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassioneBrowserKit_Sample
{
    public partial class ucTextMulti : UserControl
    {
        public ucTextMulti()
        {
            InitializeComponent();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                ((frmEdit)this.Parent.FindForm()).strTargetValue = textBox1.Text;
                this.Parent.FindForm().DialogResult = DialogResult.OK;
                
            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }
        }
    }
}
