using Godot;
using System;
using ViewModels;

namespace Views
{
	public abstract partial class View<TViewModel> : Node where TViewModel : ViewModel
	{
		public TViewModel ViewModel { get; protected set; }
		
		protected IDisposable disposable;

		public override void _ExitTree()
		{
			base._ExitTree();
			disposable?.Dispose();
		}
	}
}
