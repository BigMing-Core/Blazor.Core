var ElementOp = {
    ScrollTo: function (elementID, x, y) {
        var element = document.getElementById(elementID);
        if (element != undefined) {
            element.scroll(x, y);
        }
    },
    GetElementClientRects: function (elementID) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo == undefined) {
            return {
                X: 0,
                Y: 0,
                Top: 0,
                Bottom: 0,
                Left: 0,
                Right: 0,
                Width: 0,
                Height: 0
            }
        }
        var elementRects = elementInfo.getClientRects();
        if (elementRects.length == 0) {
            return {
                X: 0,
                Y: 0,
                Top: 0,
                Bottom: 0,
                Left: 0,
                Right: 0,
                Width: 0,
                Height: 0
            }
        }
        var rectInfo = elementRects[0];
        return {
            X: rectInfo.x,
            Y: rectInfo.y,
            Top: rectInfo.top,
            Bottom: rectInfo.bottom,
            Left: rectInfo.left,
            Right: rectInfo.right,
            Width: rectInfo.width,
            Height: rectInfo.height,
            OffsetTop: elementInfo.offsetTop,
            OffsetLeft: elementInfo.offsetLeft,
            OffsetHeight: elementInfo.offsetHeight,
            OffsetWidth: elementInfo.offsetWidth
        }
    },
    GetElementScrollInfo: function (elementID) {
        var element = document.getElementById(elementID);
        if (element != undefined) {
            return {
                ScrollTop: e.target.scrollTop,
                ScrollHeight: e.target.scrollHeight,
                ScrollLeft: e.target.scrollLeft,
                ScrollWidth: e.target.scrollWidth
            };
        }
        return {
            ScrollTop: 0,
            ScrollHeight: 0,
            ScrollLeft: 0,
            ScrollWidth: 0
        }
    },
    GetElementValue: function (elementID) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo == undefined) {
            return "";
        }
        return elementInfo.value;
    },
    SetElementValue: function (elementID, value) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo == undefined) {
            return "";
        }
        return elementInfo.value = value;
    },
    GetElementInnerText: function (elementID) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo == undefined) {
            return "";
        }
        return elementInfo.innerText;
    }
};



var LuanNiaoBlazor = {
    ElementOp: ElementOp,
    MousePosition: function (event, mouseEvent) {
        if (event.pageX || event.pageY) {
            mouseEvent.X = event.pageX;
            mouseEvent.Y = event.pageY;

        }
        else {
            mouseEvent.X = event.clientX + document.body.scrollLeft - document.body.clientLeft;
            mouseEvent.Y = event.clientY + document.body.scrollTop - document.body.clientTop;
        }
    },
    Copy: function (text) {
        try {
            navigator.clipboard.writeText(text);
        } catch   {

        }
    },   
    BlockClickEvent: function (e) {
        var e = window.event || arguments.callee.caller.arguments[0];
        e.preventDefault();
        e.stopPropagation();
    },
    BindElementEvent: function (eventName, elementID, methodName, dNetInstance, isPreventDefault, isStopPropagation) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo != undefined) {
            elementInfo.addEventListener(eventName, (e) => {
                if (isPreventDefault) {
                    e.preventDefault();
                }
                if (isStopPropagation) {
                    e.stopPropagation();
                }
                var eventInfo = {
                    EventType: 0
                };
                if (e.constructor == MouseEvent) {
                    eventInfo.EventType = eventInfo.EventType | 1;
                    eventInfo.MouseEvent = {
                        Alt: e.altKey,
                        Button: e.button,
                        Buttons: e.buttons,
                        ClientX: e.clientX,
                        ClientY: e.clientY,
                        Control: e.ctrlKey,
                        Meta: e.metaKey,
                        Shift: e.shiftKey
                    };
                    eventInfo.CurrentWindowInfo = {

                        InnerHeight: window.innerHeight,
                        InnerWidth: window.innerWidth,
                        OuterWidth: window.outerWidth,
                        OuterHeight: window.outerHeight
                    }
                    LuanNiaoBlazor.MousePosition(e, eventInfo.MouseEvent);

                }

                dNetInstance.invokeMethodAsync(methodName, eventInfo);

            });
        }
    },
    BindElementKeyBoardEvent: function (eventName, elementID, methodName, dNetInstance, allowKeyList) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo != undefined) {
            elementInfo.addEventListener(eventName, async (e) => {

                if (e.constructor == KeyboardEvent && allowKeyList.includes(e.keyCode)) {
                    var eventInfo = {
                    };
                    eventInfo = {
                        Key: e.key,
                        KeyCode: e.keyCode,
                        Code: e.code,
                        CharCode: e.charCode,
                        Location: e.location,
                        ShiftKey: e.shiftKey,
                        CtrlKey: e.ctrlKey,
                        AltKey: e.altKey
                    };
                    dNetInstance.invokeMethodAsync(methodName, eventInfo);
                }
            });
        }
    },
    BindBodyEvent: function (eventName, methodName, dNetInstance, isPreventDefault, async) {
        var elementInfo = document.getElementsByTagName("body")[0];
        elementInfo.addEventListener(eventName, (e) => {
            if (isPreventDefault) {
                e.preventDefault();
            }
            var eventInfo = {
                EventType: 0
            };
            if (e.constructor == MouseEvent) {
                eventInfo.EventType = eventInfo.EventType | 1;
                eventInfo.MouseEvent = {
                    Alt: e.altKey,
                    Button: e.button,
                    Buttons: e.buttons,
                    ClientX: e.clientX,
                    ClientY: e.clientY,
                    Control: e.ctrlKey,
                    Meta: e.metaKey,
                    Shift: e.shiftKey
                };
                eventInfo.CurrentWindowInfo = {

                    InnerHeight: window.innerHeight,
                    InnerWidth: window.innerWidth,
                    OuterWidth: window.outerWidth,
                    OuterHeight: window.outerHeight
                }
                LuanNiaoBlazor.MousePosition(e, eventInfo.MouseEvent);

            }
            if (async) {

                dNetInstance.invokeMethodAsync(methodName, eventInfo);
            }
            else {

                dNetInstance.invokeMethod(methodName, eventInfo);
            }
        });

    },
    WindowReSize: function (callBack) {
        window.addEventListener("resize", (args) => {
            callBack.invokeMethodAsync("Resize",
                {
                    InnerHeight: window.innerHeight,
                    InnerWidth: window.innerWidth,
                    OuterWidth: window.outerWidth,
                    OuterHeight: window.outerHeight
                })
        })
    },
    WindowScroll: function (callBack) {
        window.addEventListener("scroll", (e) => {
            callBack.invokeMethodAsync("Scroll", {
                PageXOffset: window.pageXOffset,
                PageYOffset: window.pageYOffset
            });

        });
    },
    GetWindowSize: function () {
        return {

            InnerHeight: window.innerHeight,
            InnerWidth: window.innerWidth,
            OuterWidth: window.outerWidth,
            OuterHeight: window.outerHeight
        };
    },
    RegistElementEventHub: function (elementID, dNetInstance) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo != undefined) {
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "click", dNetInstance, "OnClick");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mouseover", dNetInstance, "OnMouseOver");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mouseenter", dNetInstance, "OnMouseEnter");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mousedown", dNetInstance, "OnMouseDown");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mouseup", dNetInstance, "OnMouseUp");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mousemove", dNetInstance, "OnMouseMove");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "mouseout", dNetInstance, "OnMouseOut");
            LuanNiaoBlazor.RegistElementEvent(elementInfo, "contextmenu", dNetInstance, "OnContextMenu");


            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "focus", dNetInstance, "OnFocus");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "focusin", dNetInstance, "OnFocusIn");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "focusout", dNetInstance, "OnFocusOut");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "input", dNetInstance, "OnInput");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "blur", dNetInstance, "OnBlur");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "change", dNetInstance, "OnChange");
            LuanNiaoBlazor.RegistElementCallBackEvent(elementInfo, "DOMNodeRemoved", dNetInstance, "OnElementRemoved");


            LuanNiaoBlazor.RegistElementKeyboardEvent(elementInfo, "keydown", dNetInstance, "OnKeyDown");
            LuanNiaoBlazor.RegistElementKeyboardEvent(elementInfo, "keypress", dNetInstance, "OnKeypress");
            LuanNiaoBlazor.RegistElementKeyboardEvent(elementInfo, "keyup", dNetInstance, "OnKeyup");

            LuanNiaoBlazor.RegistElementScroll(elementInfo, dNetInstance, "OnScroll");
        }
    },
    RegistElementEvent: function (element, eventName, dNetInstance, methodName) {
        element.addEventListener(eventName, (e) => {
            var eventInfo = {
                Alt: e.altKey,
                Button: e.button,
                Buttons: e.buttons,
                ClientX: e.clientX,
                ClientY: e.clientY,
                Control: e.ctrlKey,
                Meta: e.metaKey,
                Shift: e.shiftKey
            };
            LuanNiaoBlazor.MousePosition(e, eventInfo); 
            dNetInstance.invokeMethodAsync(methodName, eventInfo);
        });
    },
    RegistElementCallBackEvent: function (element, eventName, dNetInstance, methodName) {
        element.addEventListener(eventName, (e) => {
            dNetInstance.invokeMethodAsync(methodName);
        });
    },
    RegistElementKeyboardEvent: function (element, eventName, dNetInstance, methodName) {
        element.addEventListener(eventName, async (e) => {

            if (e.constructor == KeyboardEvent) {
                var eventInfo = {
                };
                eventInfo = {
                    Key: e.key,
                    KeyCode: e.keyCode,
                    Code: e.code,
                    CharCode: e.charCode,
                    Location: e.location,
                    ShiftKey: e.shiftKey,
                    CtrlKey: e.ctrlKey,
                    AltKey: e.altKey
                };
                dNetInstance.invokeMethodAsync(methodName, eventInfo);
            }
        });
    },
    RegistElementScroll: function (element, dNetInstance, methodName) {
        element.addEventListener("scroll", (e) => {
            dNetInstance.invokeMethodAsync(methodName, {
                ScrollTop: e.target.scrollTop,
                ScrollHeight: e.target.scrollHeight,
                ScrollLeft: e.target.scrollLeft,
                ScrollWidth: e.target.scrollWidth
            });

        });
    }

};



