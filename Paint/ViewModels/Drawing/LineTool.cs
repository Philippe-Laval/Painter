using Avalonia;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    /// <summary>
    /// Tool to draw a line
    /// </summary>
    public class LineTool : Tool
    {
        private LineDrawOperation? _drawOperation;
        private bool _pressed;

        public override void Pressed(IPainter painter, Point point)
        {
            // Start point P0 and end point P1 are the same for now
            var p0 = new SKPoint((float)point.X, (float)point.Y);
            var p1 = new SKPoint((float)point.X, (float)point.Y);

            //var so = SKSamplingOptions.Default;
            //var soHight = new SKSamplingOptions(SKCubicResampler.Mitchell);

            var paint = new SKPaint()
            {
                IsAntialias = false,                
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill,
                StrokeWidth = 10
            };

            _drawOperation = new LineDrawOperation(p0, p1, paint);

            // Add the LineDrawOperation
            painter.AddOperation(_drawOperation);
            painter.InvalidatePainter();

            _pressed = true;
        }

        public override void Released(IPainter painter, Point point)
        {
            if (_pressed && _drawOperation is { })
            {
                painter.InvalidatePainter();
                // Update the P1 point (end point)
                _drawOperation.P1 = new SKPoint((float) point.X, (float) point.Y);
                _drawOperation = null;
                painter.InvalidatePainter();
            }

            _pressed = false;
        }

        public override void Moved(IPainter painter, Point point)
        {
            if (_pressed && _drawOperation is { })
            {
                // Update the P1 point (end point)
                _drawOperation.P1 = new SKPoint((float) point.X, (float) point.Y);
                painter.InvalidatePainter();
            }
        }
    }
}
