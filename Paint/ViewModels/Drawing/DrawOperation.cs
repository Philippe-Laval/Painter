using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    /// <summary>
    /// Abstract class for drawing operation
    /// </summary>
    public abstract class DrawOperation
    {
        /// <summary>
        /// Renders the draw operation on a SKCanvas canvas
        /// </summary>
        /// <param name="canvas"></param>
        public abstract void Render(SKCanvas canvas);
    }
}
