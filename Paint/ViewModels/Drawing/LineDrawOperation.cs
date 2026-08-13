using SkiaSharp;

namespace Paint.ViewModels.Drawing
{
    /// <summary>
    /// LineDrawOperation to memorize the information to draw a line
    /// </summary>
    public class LineDrawOperation : DrawOperation
    {
        /// <summary>
        /// Start point
        /// </summary>
        public SKPoint P0 { get; set; }
        /// <summary>
        /// End point
        /// </summary>
        public SKPoint P1 { get; set; }
        /// <summary>
        /// The paint use to draw the line
        /// </summary>
        public SKPaint Paint { get; set; }

        public LineDrawOperation(SKPoint p0, SKPoint p1, SKPaint paint)
        {
            P0 = p0;
            P1 = p1;
            Paint = paint;
        }

        public override void Render(SKCanvas canvas)
        {
            canvas.DrawLine(P0, P1, Paint);
        }
    }
}
