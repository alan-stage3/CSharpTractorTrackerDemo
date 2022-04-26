using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TractorTracker.Core
{
    public class Result
    {
        public bool Success { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }
}
