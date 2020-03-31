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
    /// Wave's blazor component's base class
    /// </summary>
    public abstract class WBCBase : ComponentBase, IDisposable
    {

        private readonly int _createSequence = 0;
        private bool _disposed = false;
        protected OriginalStyleHelper _styleHelper = new OriginalStyleHelper();
        protected ClassNameHelper _classHelper = new ClassNameHelper();


        #region disposable pattern
        public WBCBase()
        {
            _createSequence = WBCState.Instance.GetID();
        }
        ~WBCBase()
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
            Disposing?.Invoke(this);
            if (flag)
            {
                _styleHelper = null;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public event Action<WBCBase> Disposing;
        public string IdentityKey => Guid.NewGuid().ToString("N", Thread.CurrentThread.CurrentCulture);

        [CascadingParameter]
        public ComponentBase Parent { get; set; }

        [Inject]
        public WindowEventHub WindowEventHub { get; set; }

        [Parameter]
        public virtual string CStyle
        {
            get
            {
                return _styleHelper.Build();
            }
            set
            {
                _styleHelper.AddCustomStyle(value);
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





        public HashSet<ComponentBase> Child { get; set; } = new HashSet<ComponentBase>();

        public override bool Equals(object obj)
        {
            if (obj is WBCBase item)
            {
                return item.IdentityKey.Equals(this.IdentityKey);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this._createSequence;
        }


        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (Parent is WBCBase parent)
                {
                    parent.AddChildNode(this);
                } 
            }

            base.OnAfterRender(firstRender);
        }
        protected void Flush()
        {
            InvokeAsync(() =>
            {
                if (!_disposed)
                {
                    StateHasChanged();
                }
            });
        }

        private void AddChildNode(WBCBase component)
        {
            this.Child.Add(component);
            component.Disposing += item =>
            {
                Child.Remove(item);
            };
        }



    }
}
