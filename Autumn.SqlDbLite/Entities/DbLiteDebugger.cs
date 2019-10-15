using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDbLite
{
    /// <summary>
    /// Used for debugging errors or BUG,Used for debugging, which has an impact on Performance
    /// </summary>
    public class DbLiteDebugger
    {
        /// <summary>
        /// If you use the same Db object across threads, you will be prompted
        /// </summary>
        public bool EnableThreadSecurityValidation { get; set; }
    }
}
