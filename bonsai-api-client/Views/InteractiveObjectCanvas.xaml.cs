namespace bonsai_api_client.Views;

public partial class InteractiveObjectCanvas : ContentView, IDrawable
{
    public static readonly BindableProperty ObjectsProperty =
        BindableProperty.Create("Objects", typeof(ArrayExtension), typeof(InteractiveObjectCanvas), null);

    public ArrayExtension Objects
    {
        get => (ArrayExtension)GetValue(ObjectsProperty);
        set => SetValue(ObjectsProperty, value);
    }

    public InteractiveObjectCanvas()
	{
		InitializeComponent();
	}

	public void Draw(ICanvas canvas, RectF dirtyRect)
	{
		// If can be casted as canvas object use its properties to draw

        // Otherwise some default draw
	}
}