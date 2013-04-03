////////////////////////////////////////////////////////////////////////////
// jquery.Linq2PassioneWebBrowser.js V1.0.0
// (c) 2012 by A.Shinada
// MIT License
//
// Please retain this copyright header in all versions of the software
////////////////////////////////////////////////////////////////////////////

// $(document).ready
$(document).ready(function() {

    ReBindLinq($(".linqPBObj"));

});
// $(document).ready

function CallClick(ui, e, btn) {
    $("title").text(CreateWindowMsg($(ui), "click", e.timeStamp, DecorateMouseButtonMsg(btn, e)));
    if ($(ui)[0].nodeName != "INPUT") {
        ClearSelection();
    }
}

function CallDblClick(ui, e, btn) {
    $("title").text(CreateWindowMsg($(ui), "dblclick", e.timeStamp, DecorateMouseButtonMsg(btn, e)));
    if ($(ui)[0].nodeName != "INPUT") {
        ClearSelection();
    }
}

function CallMouseDown(ui, e) {

    $("title").text(CreateWindowMsg($(ui), "mousedown", e.timeStamp, DecorateMouseUpDownMsg(e)));
}

function CallMouseUp(ui, e) {

    $("title").text(CreateWindowMsg($(ui), "mouseup", e.timeStamp, DecorateMouseUpDownMsg(e)));
}

function CallMove(ui, e) {
    $("title").text(CreateWindowMsg($(ui), "mousemove", e.timeStamp, DecorateMousePosMsg(e.pageX, e.pageY, e.screenX, e.screenY, e.offsetX, e.offsetY, e.clientX, e.clientY)));
}

function CallCommon(ui, e) {
    $("title").text(CreateWindowMsg($(ui), e.type, e.timeStamp, function() { }));
}

function CallKeyMsg(ui, e) {
    $("title").text(CreateWindowMsg($(ui), e.type, e.timeStamp, DecorateKeyCodeMsg(e)));
}

function CallScroll(ui, e) {
    $("title").text(CreateWindowMsg($(ui), e.type, e.timeStamp, DecorateScrollMsg(ui)));
}

// マウスボタン用メッセージ装飾クロージャ
function DecorateMouseButtonMsg(btnType, e) {
    return function() {
        return "<mouse_button>" + btnType + "</mouse_button>" +
               "<ctrlKey>" + e.ctrlKey + "</ctrlKey>" +
               "<shiftKey>" + e.shiftKey + "</shiftKey>" +
               "<altKey>" + e.altKey + "</altKey>";
    };
}

// マウスボタン用メッセージ装飾クロージャ
function DecorateMouseUpDownMsg(e) {
    return function() {

        var btn = "";
        switch (e.which) {
            case 1:
                btn = "Left";
                break;
            case 2:
                btn = "Middle";
                break;
            case 3:
                btn = "Right";
                break;
            default:
        }

        return "<mouse_button>" + btn + "</mouse_button>"  +
               "<pageX>" + e.pageX + "</pageX>" +
               "<pageY>" + e.pageY + "</pageY>" +
               "<clientX>" + e.clientX + "</clientX>" +
               "<clientY>" + e.clientY + "</clientY>";
    };
}

// マウスポジション用メッセージ装飾クロージャ
function DecorateMousePosMsg(pageX, pageY, screenX, screenY, offsetX, offsetY, clientX, clientY) {
    return function() {
        return "<pageX>" + pageX + "</pageX>" +
                   "<pageY>" + pageY + "</pageY>" +
                   "<screenX>" + screenX + "</screenX>" +
                   "<screenY>" + screenY + "</screenY>" +
                   "<offsetX>" + offsetX + "</offsetX>" +
                   "<offsetY>" + offsetY + "</offsetY>" +
                   "<clientX>" + clientX + "</clientX>" +
                   "<clientY>" + clientY + "</clientY>";
    };
}

// キーｺｰﾄﾞ用メッセージ装飾クロージャ
function DecorateKeyCodeMsg(e) {
    return function() {
    return "<keyCode>" + e.keyCode + "</keyCode>" +
           "<shiftKey>" + e.shiftKey + "</shiftKey>" +
           "<ctrlKey>" + e.ctrlKey + "</ctrlKey>";
    };
}

// キーｺｰﾄﾞ用メッセージ装飾クロージャ
function DecorateScrollMsg(ui) {
    return function() {
        return "<scrollTop>" + $(ui).scrollTop() + "</scrollTop>";
    };
}

// ブラウザ連携用メッセージを作成
function CreateWindowMsg(_ui, _event, evtTime, decorateCloser) {

    return "<Msg>" +
                    "<id>#" + $(_ui).attr("id") + "</id>" +
                    "<class>" + $(_ui).attr("class") + "</class>" +
                    "<event>" + _event + "</event>" +
                    "<text>" + $(_ui).text() + "</text>" +
                    "<val>" + $(_ui).val() + "</val>" +
                    "<time>" + evtTime + "</time>" +
                    (decorateCloser)() +
           "</Msg>";
}

// コールバックを通知する場合のクロージャ
function NotifyCallBack(_id, _class, _event, _decorateCloser) {
    return function() {
        $("title").text(CreateWindowMsg($(ui), _event, getCurrentTime(), _decorateCloser));
    };
}

function ReBindLinq(ui) {
    $(ui).on({

        'click': function(e) {
            CallClick($(this), e, "Left");
        },
        'contextmenu': function(e) {
            CallClick($(this), e, "Right");
            return false;
        },
        'dblclick': function(e) {
            CallDblClick($(this), e, "Left");
        },
        'mousedown': function(e) {
            CallMouseDown($(this), e);
        },
        'mouseup': function(e) {
            CallMouseUp($(this), e);
        },
        'mousemove': function(e) {
            CallMove($(this), e);
        },
        'scroll': function(e) {
            CallScroll($(this), e);
        },
        'blur focus change mouseover mouseout': function(e) {
            CallCommon($(this), e);
        },
        'keydown keyup': function(e) {
            CallKeyMsg($(this), e);
        }

    });
}

function ClearSelection() {
    // テキスト反転状態を解除
    if (window.getSelection) {
        var selection = window.getSelection();
        selection.collapse(document.body, 0);
    }
    else {
        var selection = document.selection.createRange();
        selection.setEndPoint("EndToStart", selection);
        selection.select();
    }
}

function GetHtml(ui) {
    return $(ui).html();
}
function SetHtml(ui, str) {
    $(ui).html(str);
    ReBindLinq($(ui).find(".linqPBObj"));
}
function GetText(ui) {
    return $(ui).text();
}
function GetVal(ui) {
    return $(ui).val();
}
function SetText(ui, str) {
    $(ui).text(str);
}
function SetVal(ui, str) {
    $(ui).val(str);
}
function GetCssVal(ui, cssKey) {
    return $(ui).css(cssKey);
}
function SetCssVal(ui, cssKey, val) {
    return $(ui).css(cssKey, val);
}

//現在時刻取得（yyyy/mm/dd hh:mm:ss）
function getCurrentTime() {
    var now = new Date();
    var res = "" + now.getFullYear() + "/" + padZero(now.getMonth() + 1) +
		"/" + padZero(now.getDate()) + " " + padZero(now.getHours()) + ":" +
		padZero(now.getMinutes()) + ":" + padZero(now.getSeconds());
    return res;
}

//先頭ゼロ付加
function padZero(num) {
    var result;
    if (num < 10) {
        result = "0" + num;
    } else {
        result = "" + num;
    }
    return result;
}
