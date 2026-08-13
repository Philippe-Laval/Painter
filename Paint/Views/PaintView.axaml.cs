using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Paint.ViewModels.Drawing;

namespace Paint.Views
{
    /// <summary>
    /// UserControl 
    /// </summary>
    public partial class PaintView : UserControl
    {
        private readonly Painter _painter;

        public PaintView()
        {
            InitializeComponent();

            _painter = new Painter(400, 400);
            _painter.Invalidate += (_, _) => this.InvalidateVisual();

            // Forward the events to the _painter object
            AddHandler(PointerPressedEvent, PointerPressedHandler, RoutingStrategies.Tunnel);
            AddHandler(PointerReleasedEvent, PointerReleaseHandler, RoutingStrategies.Tunnel);
            AddHandler(PointerMovedEvent, PointerMovedHandler, RoutingStrategies.Tunnel);
        }

        private void PointerPressedHandler(object? sender, PointerPressedEventArgs e)
        {
            _painter.Pressed(e.GetPosition(this));
        }

        private void PointerReleaseHandler(object? sender, PointerReleasedEventArgs e)
        {
            _painter.Released(e.GetPosition(this));
        }

        private void PointerMovedHandler(object? sender, PointerEventArgs e)
        {
            _painter.Moved(e.GetPosition(this));
        }

        /// <summary>
        /// Render the UserControl by asking _painter to render itself
        /// </summary>
        /// <param name="context"></param>
        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // Usual draw operation without Skia
            //
            //var rect = new Rect(0, 0, Bounds.Width, Bounds.Height);
            //string colorString = "#FF5733"; // Hexadecimal color code
            //SolidColorBrush brush = new SolidColorBrush(Color.Parse(colorString));
            //
            //context.DrawRectangle(brush, new Pen(Brushes.Black, 4), rect);

            _painter.Render(context);
        }
    }
}
