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
    public class PWBrowserScrollEventArgs : PWBrowserEventArgs
    {
        public PWBrowserScrollEventArgs(XDocument xMsg)
            : base(xMsg)
        {
            ScrollTop = double.Parse(xMsg.Descendants().Elements("scrollTop").First().Value.ToString());
        }

        /// <summary>
        /// スクロール値
        /// </summary>
        public double ScrollTop { get; set; }
    }
}
