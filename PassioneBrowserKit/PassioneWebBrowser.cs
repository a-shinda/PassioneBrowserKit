using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebKit;

/*
    All source code provided in this release of WebKit.NET is subject to the
    following licensing terms:

    Copyright (c) 2009, Peter Nelson (charn.opcode@gmail.com)
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice,
      this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice,
      this list of conditions and the following disclaimer in the documentation
      and/or other materials provided with the distribution.
       
    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
    ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
    LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
    CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
    SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
    INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
    CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
    POSSIBILITY OF SUCH DAMAGE.
 */

/*
    All source code provided in this release of PassioneBrowserKit is subject to the
    following licensing terms:

    Copyright (c) 2013, Akinobu Shinada (bnc.shinada@gmail.com)
    All rights reserved.

    Redistribution and use in source and binary forms, with or without
    modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice,
      this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice,
      this list of conditions and the following disclaimer in the documentation
      and/or other materials provided with the distribution.
       
    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
    AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
    ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
    LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
    CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
    SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
    INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
    CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
    POSSIBILITY OF SUCH DAMAGE.
 */

namespace PassioneBrowserKit
{
    public partial class PassioneWebBrowser : WebKit.WebKitBrowser
    {

        private bool _isSysEvtBinded = false;

        public PassioneWebBrowser()
        {
            InitializeComponent();
        }

        public PassioneWebBrowser(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// 初期化(htmlファイルをロードし、ロード完了後に引数で渡されたコールバックを実行する)
        /// </summary>
        /// <param name="htmlPath">htmlファイルパス</param>
        /// <param name="loadedCallBack">ロード完了後のコールバック</param>
        public void LoadHtml(string htmlPath, Action<PassioneWebBrowser> loadedCallBack)
        {
            this.Navigate(htmlPath);

            if (!_isSysEvtBinded)
            {
                _isSysEvtBinded = true;
                this.DocumentCompleted += (sender, e) =>
                {
                    // ロード完了後、コールバックを実行
                    loadedCallBack(this);
                };

                // htmlファイルと通信するために、DocumentTitleChangedに処理を追加
                // html ⇒ jquery.Link2Webkit.jsを経由して実行される
                this.DocumentTitleChanged += (sender, e) =>
                {
                    try
                    {
                        if(this.DocumentTitle.ToString().IndexOf("<") != -1)
                        {
                            XDocument xMsg = XDocument.Parse(this.DocumentTitle);
                            this.CallEvent(this, xMsg);
                        }
                    }
                    catch (Exception ex)
                    { System.Windows.Forms.MessageBox.Show(ex.ToString()); }
                };
            }
        }

        private Dictionary<string, Action<PassioneWebBrowser, PWBrowserEventArgs>> _dicEvent = new Dictionary<string, Action<PassioneWebBrowser, PWBrowserEventArgs>>();
        /// <summary>
        /// イベント登録
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="e"></param>
        public void AddEventListener(string ui,string _event, Action<PassioneWebBrowser, PWBrowserEventArgs> e)
        {
            if (!_dicEvent.ContainsKey(string.Format("{0}.{1}.{2}", this.Name, ui, _event)))
            {
                _dicEvent.Add(string.Format("{0}.{1}.{2}", this.Name, ui, _event), e);
            }
        }

        /// <summary>
        /// イベント呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CallEvent(PassioneWebBrowser sender, XDocument xMsg)
        {
            // イベント情報を生成
            PWBrowserEventArgs arg = PWBEventArgsFactory.CreateEventArgs(xMsg);
            string _id = string.Format("{0}.{1}.{2}", sender.Name, arg.ElementID, arg.EvtName);
            string _tmpClass = string.Empty;


            if (_dicEvent.ContainsKey(_id))
            {
                // ID指定
                _dicEvent[_id](sender, arg);
            }
            // クラスは複数の可能性がある
            foreach (string _class in arg.ElementClassName)
            {
                _tmpClass = string.Format("{0}.{1}.{2}", sender.Name, _class, arg.EvtName);
                if (_dicEvent.ContainsKey(_tmpClass))
                {
                    arg.EventTriggerClassName = _class;
                    _dicEvent[_tmpClass](sender, arg);
                }
            }

        }

        private const string GET_HTML_FORMAT = @"GetHtml('{0}');";
        /// <summary>
        /// Htmlを取得する
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public string GetHtml(string selector)
        {
            return (string)this.StringByEvaluatingJavaScriptFromString(string.Format(GET_HTML_FORMAT, selector));
        }

        private const string SET_HTML_FORMAT = @"SetHtml('{0}', '{1}');";
        /// <summary>
        /// htmlを設定する
        /// </summary>
        public void SetHtml(string selector, string html)
        {
            string htmlTmp = html.Replace(System.Environment.NewLine, "").Replace("\r", "").Replace("\n", "");
            this.StringByEvaluatingJavaScriptFromString(string.Format(SET_HTML_FORMAT, selector, htmlTmp));
        }

        private const string GET_TEXT_FORMAT = @"GetText('{0}');";
        /// <summary>
        /// Textを取得する
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public string GetText(string selector)
        {
            return (string)this.StringByEvaluatingJavaScriptFromString(string.Format(GET_TEXT_FORMAT, selector));
        }

        private const string GET_VAL_FORMAT = @"GetVal('{0}');";
        /// <summary>
        /// valを取得する
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public string GetVal(string selector)
        {
            return (string)this.StringByEvaluatingJavaScriptFromString(string.Format(GET_VAL_FORMAT, selector));
        }

        private const string SET_TEXT_FORMAT = @"SetText('{0}', '{1}');";
        /// <summary>
        /// textを設定する
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public void SetText(string selector, string str)
        {
            this.StringByEvaluatingJavaScriptFromString(string.Format(SET_TEXT_FORMAT, selector, str));
        }

        private const string SET_VAL_FORMAT = @"SetVal('{0}', '{1}');";
        /// <summary>
        /// Valを設定する
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public void SetVal(string selector, string str)
        {
            this.StringByEvaluatingJavaScriptFromString(string.Format(SET_VAL_FORMAT, selector, str));
        }

        private const string GET_CSS_VAL_FORMAT = @"GetCssVal('{0}', '{1}');";
        /// <summary>
        /// 指定要素の指定CSSの値を取得
        /// </summary>
        public string GetCssVal(string selector, string cssKey)
        {
            return this.StringByEvaluatingJavaScriptFromString(string.Format(GET_CSS_VAL_FORMAT, selector, cssKey));
        }

        private const string SET_CSS_VAL_FORMAT = @"SetCssVal('{0}', '{1}', '{2}');";
        /// <summary>
        /// 指定要素の指定CSSの値を設定
        /// </summary>
        public string SetCssVal(string selector, string cssKey, string val)
        {
            return this.StringByEvaluatingJavaScriptFromString(string.Format(SET_CSS_VAL_FORMAT, selector, cssKey, val));
        }

        /// <summary>
        /// 指定ファンクションを実行
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public string CallFunction(string function)
        {
            return this.StringByEvaluatingJavaScriptFromString(function);
        }

    }
}
