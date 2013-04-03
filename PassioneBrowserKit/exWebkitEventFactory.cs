using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PassioneBrowserKit
{
    public static class PWBEventArgsFactory
    {
        public static PWBrowserEventArgs CreateEventArgs(XDocument xMsg)
        {
            string jqEventName = xMsg.Descendants().Elements("event").First().Value.ToString();
            if (jqEventName == "click")
            {
                return new PWBrowserClickEventArgs(xMsg);
            }
            else if (jqEventName == "dblclick")
            {
                return new PWBrowserClickEventArgs(xMsg);
            }
            else if (jqEventName == "mousedown")
            {
                return new PWBrowserMouseDownEventArgs(xMsg);
            }
            else if (jqEventName == "mouseup")
            {
                return new PWBrowserMouseUpEventArgs(xMsg);
            }
            else if (jqEventName == "mousemove")
            {
                return new PWBrowserMouseMoveEventArgs(xMsg);
            }
            else if (jqEventName == "keydown")
            {
                return new PWBrowserKeyDownEventArgs(xMsg);
            }
            else if (jqEventName == "keyup")
            {
                return new PWBrowserKeyUpEventArgs(xMsg);
            }
            else if (jqEventName == "scroll")
            {
                return new PWBrowserScrollEventArgs(xMsg);
            }
            else
            {
                return new PWBrowserEventArgs(xMsg);
            }
        }
    }
}
