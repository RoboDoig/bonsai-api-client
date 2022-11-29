using bonsai_api_client.Models;
using System;
using System.Collections;

namespace bonsai_api_client.Views;

public partial class InteractiveObjectCanvas : ContentView, IDrawable
{
    public static readonly BindableProperty ObjectsProperty =
        BindableProperty.Create("Objects", typeof(IEnumerable<ICanvasObject>), typeof(InteractiveObjectCanvas), null);

    public IEnumerable<ICanvasObject> Objects
    {
        get => (IEnumerable<ICanvasObject>)GetValue(ObjectsProperty);
        set => SetValue(ObjectsProperty, value);
    }

    public InteractiveObjectCanvas()
	{
		InitializeComponent();
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
           
	}
}