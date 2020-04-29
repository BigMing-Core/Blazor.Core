var LuanNiaoBlazor = {
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
    GetElementValue: function (elementID) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo == undefined) {
            return "";
        }
        return elementInfo.value;
    },
    BlockClickEvent: function (e) {
        var e = window.event || arguments.callee.caller.arguments[0];
        e.preventDefault();
    },
    BindElementEvent: function (eventName, elementID, methodName, dNetInstance, isPreventDefault, async) {
        var elementInfo = document.getElementById(elementID);
        if (elementInfo != undefined) {
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
    GetWindowSize: function () {
        return {

            InnerHeight: window.innerHeight,
            InnerWidth: window.innerWidth,
            OuterWidth: window.outerWidth,
            OuterHeight: window.outerHeight
        };
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
    }
};