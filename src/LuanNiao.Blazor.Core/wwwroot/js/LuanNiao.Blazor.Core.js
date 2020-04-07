var WaveBlazor = {
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
        if (elementRects.length==0) {
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
            Height: rectInfo.height
        }
    }
};