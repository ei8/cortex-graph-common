using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class TraversalResult
    {
        public IEnumerable<NeuronResult> Neurons { get; set; }
        public IEnumerable<Terminal> Terminals { get; set; }
    }
}
