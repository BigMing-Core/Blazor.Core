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
        var elementRects = document.getElementById(elementID).getClientRects()[0];
        return {
            X: elementRects.x,
            Y: elementRects.y,
            Top: elementRects.top,
            Bottom: elementRects.bottom,
            Left: elementRects.left,
            Right: elementRects.right,
            Width: elementRects.width,
            Height: elementRects.height
        }
    }
};