using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGridPCL.IO {
    public static class Path {
        public static string GetFileName(string path) {
            if (string.IsNullOrWhiteSpace(path)) {
                throw new ArgumentNullException("path cannot be empty");
            }
            return path.Split(new[] { '/', '\\' }).Last();
        }
    }
}
