using System;
using System.Collections.Generic;
using System.Text;

namespace Devitor.Models.Enums
{
    public enum ApplicationHint
    {
        /// <summary>
        /// Width and height are default size.
        /// </summary>
        None = 0,
        /// <summary>
        /// Width and height are minimum bounds.
        /// </summary>
        Min = 1,
        /// <summary>
        ///  Width and height are maximum bounds.
        /// </summary>
        Max = 2,
        /// <summary>
        /// Window size can not be changed by a user.
        /// </summary>
        Fixed = 3,
    }
}
