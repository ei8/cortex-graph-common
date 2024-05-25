using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class DepthIdsPair
    {
        public int Depth { get; set; }
        public IEnumerable<Guid> Ids { get; set; }
    }
}
