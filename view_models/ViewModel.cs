using System;

namespace ViewModels
{
    public abstract class ViewModel : IDisposable
    {
        protected IDisposable disposable;

        public virtual void Dispose()
        {
            disposable?.Dispose();
        }
    }
}
