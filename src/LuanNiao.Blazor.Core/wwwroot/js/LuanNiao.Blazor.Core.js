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
    }
};