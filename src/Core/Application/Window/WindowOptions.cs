using Devitor.Models.Window;

namespace Devitor.Core.Application.Window
{
    public class WindowOptions
    {
        public string Title { get; set; }
        public WindowSize Size { get; set; }
        public byte Transparency { get; set; } = 255;
        public string StartLocation { get; set; }
    }
}