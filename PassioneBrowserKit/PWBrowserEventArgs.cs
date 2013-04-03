using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PassioneBrowserKit
{
    /// <summary>
    /// イベント情報
    /// </summary>
    public class PWBrowserEventArgs
    {
        public PWBrowserEventArgs(XDocument xMsg)
        {
            EvtName = xMsg.Descendants().Elements("event").First().Value.ToString();
            ElementID = xMsg.Descendants().Elements("id").First().Value.ToString();

            ElementClassName = new List<string>();
            ElementClassName.AddRange(from v in xMsg.Descendants().Elements("class").First().Value.ToString().Split(' ')
                                      where !string.IsNullOrEmpty(v.ToString())
                                      select "." + v);
            
            ElementText = xMsg.Descendants().Elements("text").First().Value.ToString();
            ElementValue = xMsg.Descendants().Elements("val").First().Value.ToString();
        }

        /// <summary>
        /// イベント名を示します
        /// </summary>
        public string EvtName { get; set; }
        /// <summary>
        /// イベントの発生もとのエレメントのIDを示します
        /// </summary>
        public string ElementID { get; set; }
        /// <summary>
        /// イベントの発生もとのエレメントのクラスを示します
        /// </summary>
        public List<string> ElementClassName { get; set; }
        /// <summary>
        /// このイベントが発生するトリガとなったクラスを示します
        /// </summary>
        public string EventTriggerClassName { get; set; }
        /// <summary>
        /// イベントの発生もとのエレメントのTextを示します
        /// </summary>
        public string ElementText { get; set; }
        /// <summary>
        /// イベントの発生もとのエレメントのValをしめします
        /// </summary>
        public string ElementValue { get; set; }
    }
}
