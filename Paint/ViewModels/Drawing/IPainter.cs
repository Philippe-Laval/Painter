namespace Paint.ViewModels.Drawing
{
    /// <summary>
    /// Interface for Painter
    /// </summary>
    public interface IPainter
    {
        /// <summary>
        /// Add a draw operation
        /// </summary>
        /// <param name="drawOperation"></param>
        void AddOperation(DrawOperation drawOperation);

        /// <summary>
        /// Remove a draw operation
        /// </summary>
        /// <param name="drawOperation"></param>
        void RemoveOperation(DrawOperation drawOperation);

        /// <summary>
        /// Invalidate the painter forcing a redraw of the control
        /// </summary>
        void InvalidatePainter();
    }
}
