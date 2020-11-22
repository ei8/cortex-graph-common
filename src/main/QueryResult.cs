using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class QueryResult
    {
        public IEnumerable<NeuronResult> Neurons { get; set; }

        public int Count { get; set; }
    }
}
