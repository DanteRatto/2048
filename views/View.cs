using Godot;
using System;
using System.Threading.Tasks;
using ViewModels;

namespace Views;

public abstract partial class View<TViewModel> : Control where TViewModel : ViewModel
{
    public TViewModel ViewModel
    {
        get
        {
            if (viewModel == null) Initialize();
            return viewModel;
        }

        protected set => viewModel = value;
    }
    private TViewModel viewModel;

    protected IDisposable disposable;

    protected abstract void Initialize();

    public override void _Ready()
    {
        base._Ready();
        if (viewModel == null) Initialize();
        DelayAFrame();
    }

    private async void DelayAFrame()
    {
        try
        {
            await Task.Yield();
            _LateReady();
        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }
    }

    protected virtual void _LateReady() { }

    public override void _ExitTree()
    {
        base._ExitTree();
        disposable?.Dispose();
    }
}
