using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

namespace PassioneBrowserKit
{
    /// <summary>
    /// Clickイベント情報
    /// </summary>
    public class PWBrowserKeyUpEventArgs : PWBrowserKeyDownEventArgs
    {
        public PWBrowserKeyUpEventArgs(XDocument xMsg)
            : base(xMsg)
        {

        }
    }
}
