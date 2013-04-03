using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PassioneBrowserKit
{
    /// <summary>
    /// Clickイベント情報
    /// </summary>
    public class PWBrowserMouseUpEventArgs : PWBrowserMouseDownEventArgs
    {
        public PWBrowserMouseUpEventArgs(XDocument xMsg)
            : base(xMsg)
        {

        }
    }
}
