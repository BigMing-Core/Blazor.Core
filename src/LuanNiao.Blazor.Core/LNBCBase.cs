using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuanNiao.Blazor.Core
{
    /// <summary>
    /// LuanNiao's blazor component's base class
    /// </summary>
    public abstract class LNBCBase : ComponentBase, IDisposable
    {

        private readonly int _createSequence = 0;
        private bool _disposed = false;
        protected bool _hasFirstRender = false;
        protected OriginalStyleHelper _styleHelper = new OriginalStyleHelper();
        protected ClassNameHelper _classHelper = new ClassNameHelper();


        #region disposable pattern
        public LNBCBase()
        {
            _createSequence = LNBCState.Instance.GetID();
        }
        ~LNBCBase()
        {
            this.Dispose(false);
        }
        protected virtual void Dispose(bool flag)
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            Disposing?.Invoke();
            if (flag)
            {
                _styleHelper = null;
                _classHelper = null;
                ChildContent = null;
                WindowEventHub = null;
                ElementInfo = null;
                WindowInfo = null; 
                Navigator = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public event Action Disposing;
        public string IdentityKey = Guid.NewGuid().ToString("N", Thread.CurrentThread.CurrentCulture);


        public OriginalStyleHelper StyleHelper { get => _styleHelper; }
        public ClassNameHelper ClassHelper { get => _classHelper; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        protected Navigator Navigator { get; set; }

        [Inject]
        protected WindowEventHub WindowEventHub { get; set; }

        [Inject]
        protected ElementInfo ElementInfo { get; set; }
        [Inject]
        protected BodyEventHub BodyEventHub { get; set; }

        [Inject]
        protected WindowInfo WindowInfo { get; set; }

        [Parameter]
        public virtual string CStyle
        {
            get
            {
                return _styleHelper.Build();
            }
            set
            {
                _styleHelper.AddCustomStyleStr(value);
            }
        }

        [Parameter]
        public virtual string ClassName
        {
            get
            {

                return _classHelper.Build();
            }
            set
            {
                _classHelper.AddCustomClass(value);
            }
        }




        protected override void OnAfterRender(bool firstRender)
        {
            _hasFirstRender = true;
            base.OnAfterRender(firstRender);
        }

        public override bool Equals(object obj)
        {
            if (obj is LNBCBase item)
            {
                return item.IdentityKey.Equals(this.IdentityKey);
            }
            return false;
        }


        public override int GetHashCode()
        {
            return this._createSequence;
        }


        public void Flush()
        {
            InvokeAsync(() =>
            {
                if (!_disposed)
                {
                    StateHasChanged();
                }
            });
        }

        public async Task FlushAsync()
        {
           await InvokeAsync(() =>
            {
                if (!_disposed)
                {
                    StateHasChanged();
                }
            });
        }


    }
}
