using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PassioneBrowserKit;

namespace PassioneBrowserKit_Sample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Navigate
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            path = System.IO.Path.Combine(path, "htdocs");
            path = System.IO.Path.Combine(path, "htmlfile.htm");
            passioneWebBrowser1.LoadHtml(@"file:///" + path.Replace(@"\", "/"), loadCallBack());
        }

        /// <summary>
        /// webkitブラウザにhtmlをロードした後に実行されるデリゲード
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        private Action<PassioneWebBrowser> loadCallBack()
        {
            return (_wb) =>
            {
                try
                {
                    // イベントをバインドする
                    _wb.AddEventListener("#div1", "click", div1_Click());
                    _wb.AddEventListener("#div2", "click", div2_Click());
                    _wb.AddEventListener("#div3", "dblclick", div3_dblClick());
                    _wb.AddEventListener("#div4", "mousedown", div4_mousedown());
                    _wb.AddEventListener("#div4", "mouseup", div4_mouseup());
                    _wb.AddEventListener("#div5", "mousemove", div5_mousemove());
                    _wb.AddEventListener("#input1", "blur", input1_blur());
                    _wb.AddEventListener("#input2", "blur", input1_blur());
                    _wb.AddEventListener("#input3", "focus", input3_focus());
                    _wb.AddEventListener("#input4", "focus", input3_focus());
                    _wb.AddEventListener("#select1", "change", select1_change());
                    _wb.AddEventListener("#select2", "change", select1_change());
                    _wb.AddEventListener("#input5", "keydown", input5_keydown());
                    _wb.AddEventListener("#input6", "keyup", input6_keyup());
                    _wb.AddEventListener("#div6", "mouseover", div6_mouseover());
                    _wb.AddEventListener("#div6", "mouseout", div6_mouseout());
                    _wb.AddEventListener("#div_outer", "scroll", div_outer_scroll());


                    _wb.AddEventListener("#divHoge", "click", (sender, e) => { MessageBox.Show("div1 click!!"); });
                    _wb.AddEventListener(".hoge", "click", (sender, e) => { MessageBox.Show("hoge click!!"); });
                    _wb.AddEventListener(".fuga", "click", (sender, e) => { MessageBox.Show("fuga click!!"); });

                    _wb.AddEventListener(".showEditfrm", "click", showEditFrm());

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            };
        }

        /// <summary>
        /// htmlの#div1クリック時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div1_Click()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserClickEventArgs _e = (PWBrowserClickEventArgs)e;
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("button: {0}", _e.MouseButton).AppendLine("");
                    str.AppendFormat("ctrlKey: {0}", _e.ICtrlKey.ToString()).AppendLine("");
                    str.AppendFormat("shiftKey: {0}", _e.IsShiftKey.ToString()).AppendLine("");
                    str.AppendFormat("altKey: {0}", _e.IsAltKey.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            };

        }

        /// <summary>
        /// htmlの#div2クリック時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div2_Click()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserClickEventArgs _e = (PWBrowserClickEventArgs)e;
                    if (_e.MouseButton != PWBrowserClickEventArgs.PWBClickEventArgsMouseButton.Left)
                    {
                        StringBuilder strtmp = new StringBuilder();
                        strtmp.AppendLine("右クリックは受け付けません");
                        txtInfo.Text = strtmp.ToString() + Environment.NewLine + txtInfo.Text;
                        return;
                    }

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("button: {0}", _e.MouseButton).AppendLine("");
                    str.AppendFormat("ctrlKey: {0}", _e.ICtrlKey.ToString()).AppendLine("");
                    str.AppendFormat("shiftKey: {0}", _e.IsShiftKey.ToString()).AppendLine("");
                    str.AppendFormat("altKey: {0}", _e.IsAltKey.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            };

        }

        /// <summary>
        /// htmlの#div3クリック時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div3_dblClick()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserClickEventArgs _e = (PWBrowserClickEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
		            {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
		            }
                    str.AppendLine("");
                    
                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("button: {0}", _e.MouseButton).AppendLine("");
                    str.AppendFormat("ctrlKey: {0}", _e.ICtrlKey.ToString()).AppendLine("");
                    str.AppendFormat("shiftKey: {0}", _e.IsShiftKey.ToString()).AppendLine("");
                    str.AppendFormat("altKey: {0}", _e.IsAltKey.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            };

        }

        /// <summary>
        /// htmlの#div4マウスダウン時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div4_mousedown()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserMouseDownEventArgs _e = (PWBrowserMouseDownEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("button: {0}", _e.MouseButton).AppendLine("");
                    str.AppendFormat("PagePosX: {0}", _e.PagePos.X).AppendLine("");
                    str.AppendFormat("PagePosY: {0}", _e.PagePos.Y).AppendLine("");
                    str.AppendFormat("ClientPosX: {0}", _e.ClientPos.X).AppendLine("");
                    str.AppendFormat("ClientPosY: {0}", _e.ClientPos.Y).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlの#div4マウスアップ時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div4_mouseup()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserMouseUpEventArgs _e = (PWBrowserMouseUpEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("button: {0}", _e.MouseButton).AppendLine("");
                    str.AppendFormat("PagePosX: {0}", _e.PagePos.X).AppendLine("");
                    str.AppendFormat("PagePosY: {0}", _e.PagePos.Y).AppendLine("");
                    str.AppendFormat("ClientPosX: {0}", _e.ClientPos.X).AppendLine("");
                    str.AppendFormat("ClientPosY: {0}", _e.ClientPos.Y).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlの#div5マウス移動時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div5_mousemove()
        {

            return (sender, e) =>
            {
                try
                {
                    PWBrowserMouseMoveEventArgs _e = (PWBrowserMouseMoveEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(_e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("PagePosX: {0}", _e.PagePos.X).AppendLine("");
                    str.AppendFormat("PagePosY: {0}", _e.PagePos.Y).AppendLine("");
                    str.AppendFormat("ClientPosX: {0}", _e.ClientPos.X).AppendLine("");
                    str.AppendFormat("ClientPosY: {0}", _e.ClientPos.Y).AppendLine("");
                    str.AppendFormat("ScreentPosX: {0}", _e.ScreentPos.X).AppendLine("");
                    str.AppendFormat("ScreentPosY: {0}", _e.ScreentPos.Y).AppendLine("");
                    str.AppendFormat("OffsetPosX: {0}", _e.OffsetPos.X).AppendLine("");
                    str.AppendFormat("OffsetPosY: {0}", _e.OffsetPos.Y).AppendLine("");

                    txtMoveInfo.Text = str.ToString();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlのinput1/2のフォーカス消失時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> input1_blur()
        {

            return (sender, e) =>
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlのinput3/4のフォーカス取得時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> input3_focus()
        {

            return (sender, e) =>
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlのselect1/2の選択地変更時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> select1_change()
        {

            return (sender, e) =>
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlのinput5のキーダウンイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> input5_keydown()
        {

            return (sender, e) =>
            {
                try
                {
                    PWBrowserKeyDownEventArgs _e = (PWBrowserKeyDownEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");
                    
                    str.AppendFormat("keyCode  : {0}", _e.KeyCode.ToString()).AppendLine("");
                    str.AppendFormat("Input  : {0}", _e.Char).AppendLine("");
                    str.AppendFormat("ctrl  : {0}", _e.ICtrlKey.ToString()).AppendLine("");
                    str.AppendFormat("shift  : {0}", _e.IsShiftKey.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlのinput6のキーアップイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> input6_keyup()
        {

            return (sender, e) =>
            {
                try
                {
                    PWBrowserKeyUpEventArgs _e = (PWBrowserKeyUpEventArgs)e;

                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", _e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", _e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < _e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != _e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", _e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", _e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", _e.EventTriggerClassName).AppendLine("");

                    str.AppendFormat("keyCode  : {0}", _e.KeyCode.ToString()).AppendLine("");
                    str.AppendFormat("Input  : {0}", _e.Char).AppendLine("");
                    str.AppendFormat("ctrl  : {0}", _e.ICtrlKey.ToString()).AppendLine("");
                    str.AppendFormat("shift  : {0}", _e.IsShiftKey.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlの#div6のマウスオーバー時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div6_mouseover()
        {

            return (sender, e) =>
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlの#div6のマウスアウト時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div6_mouseout()
        {

            return (sender, e) =>
            {
                try
                {
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }

        /// <summary>
        /// htmlの#div_outerのスクロール時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> div_outer_scroll()
        {

            return (sender, e) =>
            {
                try
                {
                    PWBrowserScrollEventArgs _e = (PWBrowserScrollEventArgs)e;
                    StringBuilder str = new StringBuilder();
                    str.AppendFormat("evt   : {0}", e.EvtName).AppendLine("");
                    str.AppendFormat("id    : {0}", e.ElementID).AppendLine("");

                    str.Append("class : ");
                    for (int i = 0; i < e.ElementClassName.Count; i++)
                    {
                        str.Append(e.ElementClassName[i]);
                        if (i != e.ElementClassName.Count - 1)
                            str.Append(", ");
                    }
                    str.AppendLine("");

                    str.AppendFormat("text  : {0}", e.ElementText).AppendLine("");
                    str.AppendFormat("val   : {0}", e.ElementValue).AppendLine("");
                    str.AppendFormat("trig  : {0}", e.EventTriggerClassName).AppendLine("");
                    str.AppendFormat("scrollTop  : {0}", _e.ScrollTop.ToString()).AppendLine("");

                    txtInfo.Text = str.ToString() + Environment.NewLine + txtInfo.Text;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.ToString()); }

            };

        }


        /// <summary>
        /// htmlの#div2クリック時のイベント
        /// </summary>
        /// <returns></returns>
        private Action<PassioneWebBrowser, PWBrowserEventArgs> showEditFrm()
        {

            return (sender, e) =>
            {
                try
                {
                    // マウスボタンの右/左を判定したい場合はキャストすれば参照可能
                    PWBrowserClickEventArgs _e = (PWBrowserClickEventArgs)e;
                    if (_e.MouseButton != PWBrowserClickEventArgs.PWBClickEventArgsMouseButton.Left)
                    {
                        return;
                    }

                    using (frmEdit frm = new frmEdit())
                    {
                        frm.StartPosition = FormStartPosition.CenterParent;
                        frm.strTargetName = e.ElementID;

                        if (e.ElementID == "#edForGetText")
                        {
                            frm.strTargetValue = sender.GetText("#divGetSet");
                            frm.ShowDialog(this);

                            if (frm.DialogResult == DialogResult.OK)
                            {
                                sender.SetText("#divGetSet", frm.strTargetValue);
                            }
                        }
                        else if (e.ElementID == "#edForGetVal")
                        {
                            frm.strTargetValue = sender.GetVal("#selectGetSet");
                            frm.ShowDialog(this);

                            if (frm.DialogResult == DialogResult.OK)
                            {
                                sender.SetVal("#selectGetSet", frm.strTargetValue);
                            }
                        }
                        else if (e.ElementID == "#edForGetCss")
                        {
                            frm.strTargetValue = sender.GetCssVal("#divGetCss", "width") + "," + sender.GetCssVal("#divGetCss", "height");
                            frm.ShowDialog(this);

                            if (frm.DialogResult == DialogResult.OK)
                            {
                                sender.SetCssVal("#divGetCss", "width", frm.strTargetValue.Split(',')[0].Trim() + "px");
                                sender.SetCssVal("#divGetCss", "height", frm.strTargetValue.Split(',')[1].Trim() + "px");
                            }
                        }
                        else if (e.ElementID == "#edForGetHtml")
                        {
                            frm.strTargetValue = sender.GetHtml("#divGetHtml");
                            frm.ShowDialog(this);

                            if (frm.DialogResult == DialogResult.OK)
                            {
                                sender.SetHtml("#divGetHtml", frm.strTargetValue);
                            }
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            };

        }

        private void btnBrowserReload_Click(object sender, EventArgs e)
        {
            passioneWebBrowser1.Reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtInfo.Text = string.Empty;
        }

    }
}
