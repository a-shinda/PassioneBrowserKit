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
    public class PWBrowserClickEventArgs : PWBrowserEventArgs
    {
        public PWBrowserClickEventArgs(XDocument xMsg):base(xMsg)
        {
            MouseButton = (from e in (PWBClickEventArgsMouseButton[])Enum.GetValues(typeof(PWBClickEventArgsMouseButton))
                           where e.ToString() == xMsg.Descendants().Elements("mouse_button").First().Value.ToString()
                           select e).First();

            ICtrlKey = bool.Parse(xMsg.Descendants().Elements("ctrlKey").First().Value.ToString());
            IsShiftKey = bool.Parse(xMsg.Descendants().Elements("shiftKey").First().Value.ToString());
            IsAltKey = bool.Parse(xMsg.Descendants().Elements("altKey").First().Value.ToString());
        }

        /// <summary>
        /// マウスのボタン種類
        /// </summary>
        public enum PWBClickEventArgsMouseButton
        {
            Left,
            Middle,
            Right
        }

        /// <summary>
        /// どのマウスボタンが押されたのかを示します
        /// </summary>
        public PWBClickEventArgsMouseButton MouseButton { get; set; }

        /// <summary>
        /// クリック時にCtrlキーが押されている : true
        /// </summary>
        public bool ICtrlKey { get; set; }

        /// <summary>
        /// クリック時にShiftキーが押されている : true
        /// </summary>
        public bool IsShiftKey { get; set; }

        /// <summary>
        /// クリック時にAltキーが押されている : true
        /// </summary>
        public bool IsAltKey { get; set; }
    }
}
