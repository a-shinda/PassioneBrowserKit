﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassioneBrowserKit_Sample
{
    public partial class ucWH : UserControl
    {
        public ucWH()
        {
            InitializeComponent();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            ((frmEdit)this.Parent.FindForm()).strTargetValue = numericUpDown1.Text + "," + numericUpDown2.Text;
            this.Parent.FindForm().DialogResult = DialogResult.OK;
        }
    }
}
