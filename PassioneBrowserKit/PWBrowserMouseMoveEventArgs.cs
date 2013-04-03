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
    public class PWBrowserMouseMoveEventArgs : PWBrowserEventArgs
    {
        public PWBrowserMouseMoveEventArgs(XDocument xMsg)
            : base(xMsg)
        {
            PagePos = new Point(int.Parse(xMsg.Descendants().Elements("pageX").First().Value.ToString()),
                                int.Parse(xMsg.Descendants().Elements("pageY").First().Value.ToString()));

            ClientPos = new Point(int.Parse(xMsg.Descendants().Elements("clientX").First().Value.ToString()),
                                  int.Parse(xMsg.Descendants().Elements("clientY").First().Value.ToString()));

            ScreentPos = new Point(int.Parse(xMsg.Descendants().Elements("screenX").First().Value.ToString()),
                                   int.Parse(xMsg.Descendants().Elements("screenY").First().Value.ToString()));

            OffsetPos = new Point(int.Parse(xMsg.Descendants().Elements("offsetX").First().Value.ToString()),
                                  int.Parse(xMsg.Descendants().Elements("offsetY").First().Value.ToString()));
        }

        /// <summary>
        /// ページ上の座標
        /// </summary>
        public  Point PagePos { get; set; }

        /// <summary>
        /// クライアント領域（ウィンドウ）上の座標
        /// </summary>
        public Point ClientPos { get; set; }

        /// <summary>
        /// スクリーン上の座標
        /// </summary>
        public Point ScreentPos { get; set; }

        /// <summary>
        /// クリックした要素上の座標
        /// </summary>
        public Point OffsetPos { get; set; }
    }
}
