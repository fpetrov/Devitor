namespace Devitor.Models.Window
{
    public class WindowSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public WindowSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}