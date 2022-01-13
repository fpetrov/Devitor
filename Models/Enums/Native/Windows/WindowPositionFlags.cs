using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Models.Enums.Native.Windows
{
    public enum WindowPositionFlags
    {
        FrameChanged = 0x0020,
        NoMove = 0x0002,
        NoSize = 0x0001,
        NoZOrder = 0x0004,
        NoOwnerZOrder = 0x0200
    }
}
