using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

namespace PassioneBrowserKit
{
    /// <summary>
    /// Clickイベント情報
    /// </summary>
    public class PWBrowserMouseDownEventArgs : PWBrowserEventArgs
    {
        public PWBrowserMouseDownEventArgs(XDocument xMsg)
            : base(xMsg)
        {
            PagePos = new Point(int.Parse(xMsg.Descendants().Elements("pageX").First().Value.ToString()),
                                int.Parse(xMsg.Descendants().Elements("pageY").First().Value.ToString()));

            ClientPos = new Point(int.Parse(xMsg.Descendants().Elements("clientX").First().Value.ToString()),
                                  int.Parse(xMsg.Descendants().Elements("clientY").First().Value.ToString()));

            MouseButton = (from e in (PWBMouseDownEventArgsMouseButton[])Enum.GetValues(typeof(PWBMouseDownEventArgsMouseButton))
                           where e.ToString() == xMsg.Descendants().Elements("mouse_button").First().Value.ToString()
                           select e).First();       
        }

        /// <summary>
        /// マウスのボタン種類
        /// </summary>
        public enum PWBMouseDownEventArgsMouseButton
        {
            Left,
            Middle,
            Right
        }

        /// <summary>
        /// どのマウスボタンが押されたのかを示します
        /// </summary>
        public PWBMouseDownEventArgsMouseButton MouseButton { get; set; }

        /// <summary>
        /// ページ上の座標
        /// </summary>
        public Point PagePos { get; set; }

        /// <summary>
        /// クライアント領域（ウィンドウ）上の座標
        /// </summary>
        public Point ClientPos { get; set; }
    }
}
