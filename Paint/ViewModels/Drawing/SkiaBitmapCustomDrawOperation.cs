using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class SkiaBitmapCustomDrawOperation : ICustomDrawOperation
    {
        private readonly SKBitmap _bitmap;

        public SkiaBitmapCustomDrawOperation(SKBitmap bitmap, Rect bounds)
        {
            _bitmap = bitmap;
            Bounds = bounds;
        }

        public void Dispose()
        {
        }

        public bool HitTest(Point p) => false;

        public void Render(ImmediateDrawingContext context)
        {
            var leaseFeature = context.TryGetFeature<ISkiaSharpApiLeaseFeature>();
            if (leaseFeature == null)
                return;

            using var lease = leaseFeature.Lease();
            var canvas = lease.SkCanvas;

            // Draws the SKBitmap on the SkCanvas 
            canvas?.DrawBitmap(_bitmap, SKPoint.Empty);
        }

        public Rect Bounds { get; }

        public bool Equals(ICustomDrawOperation? other) => false;
    }
}
