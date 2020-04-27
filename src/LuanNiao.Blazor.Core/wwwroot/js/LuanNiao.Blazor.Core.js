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
    BlockClickEvent: function (e) {
        var e = window.event || arguments.callee.caller.arguments[0];
        e.preventDefault();
    },
    BindElementEvent: function (eventName, elementID, methodName, dNetInstance, isPreventDefault, async) {
        var elementInfo = document.getElementById(elementID);
        if (elementID == "body") {
            elementInfo = document.getElementsByTagName("body")[0];
        }
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
                        InnerSize:
                        {
                            Height: window.innerHeight,
                            Width: window.innerWidth
                        }
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
        }
    },
    WindowReSize: function (callBack) {
        window.addEventListener("resize", (args) => {
            callBack.invokeMethodAsync("Resize",
                {
                    InnerSize:
                    {
                        Height: window.innerHeight,
                        Width: window.innerWidth
                    }
                })
        })
    },
    GetWindowSize: function () {
        return {
            InnerSize:
            {
                Height: window.innerHeight,
                Width: window.innerWidth
            }
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
            OffetTop: elementInfo.offsetTop,
            OffsetLeft: elementInfo.offsetLeft,
            OffsetHeight: elementInfo.offsetHeight,
            OffsetWidth: elementInfo.offsetWidth
        }
    }
};