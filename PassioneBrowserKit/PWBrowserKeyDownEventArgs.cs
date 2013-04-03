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
    public class PWBrowserKeyDownEventArgs : PWBrowserEventArgs
    {
        public PWBrowserKeyDownEventArgs(XDocument xMsg)
            : base(xMsg)
        {
            KeysConverter kc = new KeysConverter();
            KeyCode = (Keys)kc.ConvertFrom(xMsg.Descendants().Elements("keyCode").First().Value.ToString());
            Char = kc.ConvertToString(KeyCode);

            ICtrlKey = bool.Parse(xMsg.Descendants().Elements("ctrlKey").First().Value.ToString());
            IsShiftKey = bool.Parse(xMsg.Descendants().Elements("shiftKey").First().Value.ToString());
        }

        /// <summary>
        /// キーｺｰﾄﾞ
        /// </summary>
        public Keys KeyCode { get; set; }

        /// <summary>
        /// 入力値
        /// </summary>
        public string Char { get; set; }

        /// <summary>
        /// Ctrlキーが押されている : true
        /// </summary>
        public bool ICtrlKey { get; set; }

        /// <summary>
        /// Shiftキーが押されている : true
        /// </summary>
        public bool IsShiftKey { get; set; }
    }
}
