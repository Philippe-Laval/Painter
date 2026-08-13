using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    public class Painter : IPainter
    {
        private int _width;
        private int _height;

        private SKBitmap _original;
        private SKBitmap _working;
        
        /// <summary>
        /// List of drawing oepration
        /// </summary>
        private List<DrawOperation> _drawOperations;
        private Tool? _currentTool;

        /// <summary>
        /// Event handler
        /// </summary>
        public event EventHandler<EventArgs>? Invalidate;
        
        public Painter(int width, int height)
        {
            _width = width;
            _height = height;

            // SKImageInfo.PlatformColorType
            // SKColorType.Rgba8888
            // SKColorType.Bgra8888

            _original = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul); 
            _working = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Unpremul);

            _drawOperations = new List<DrawOperation>();
            _currentTool = new LineTool();

            ClearOriginal();
        }

        /// <summary>
        /// Calls the Invalidate hadler if not null
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnInvalidate(EventArgs e) => Invalidate?.Invoke(this, e);

        public void AddOperation(DrawOperation drawOperation)
        {
            _drawOperations.Add(drawOperation);
        }

        public void RemoveOperation(DrawOperation drawOperation)
        {
            _drawOperations.Remove(drawOperation);
        }

        public void InvalidatePainter()
        {
            OnInvalidate(new EventArgs());
        }

        /// <summary>
        /// Handles the pointer pressed event by forwarding the info to the _currentTool.
        /// </summary>
        /// <param name="point"></param>
        public void Pressed(Point point)
        {
            _currentTool?.Pressed(this, point);
        }

        /// <summary>
        /// Handles the pointer released event by forwarding the info to the _currentTool.
        /// </summary>
        /// <param name="point"></param>
        public void Released(Point point)
        {
            _currentTool?.Released(this, point);
        }

        /// <summary>
        /// Handles the pointer moved event by forwarding the info to the _currentTool.
        /// </summary>
        /// <param name="point"></param>
        public void Moved(Point point)
        {
            _currentTool?.Moved(this, point);
        }

        /// <summary>
        /// Clears the _original SKBitmap
        /// </summary>
        private void ClearOriginal()
        {
            using var canvas = new SKCanvas(_original);

            canvas.Clear(SKColors.White);
        }

        /// <summary>
        /// Renders all the drawOperations on _working SKBitmap
        /// </summary>
        private void BlitWorking()
        {
            using var canvas = new SKCanvas(_working);

            canvas.Clear(SKColors.Transparent);

            canvas.DrawBitmap(_original, SKPoint.Empty);

            foreach (var operation in _drawOperations)
            {
                operation.Render(canvas);
            }
        }

        /// <summary>
        /// Render the _working SKBitmap on the AvaloniaUI DrawingContext
        /// </summary>
        /// <param name="context"></param>
        public void Render(DrawingContext context)
        {
            BlitWorking();

            var bounds = new Rect(0, 0, _width, _height);
            var custom = new SkiaBitmapCustomDrawOperation(_working, bounds);

            // Draws on the AvaloniaUI DrawingContext
            context.Custom(custom);
        }
    }
}
