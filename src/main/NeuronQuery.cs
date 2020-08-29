using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class NeuronQuery
    {
        public IEnumerable<string> Postsynaptic { get; set; }

        public IEnumerable<string> PostsynapticNot { get; set; }

        public IEnumerable<string> Presynaptic { get; set; }

        public IEnumerable<string> PresynapticNot { get; set; }

        public IEnumerable<string> TagContains { get; set; }

        public IEnumerable<string> TagContainsNot { get; set; }

        public IEnumerable<string> Id { get; set; }

        public IEnumerable<string> IdNot { get; set; }

        public RelativeValues? RelativeValues { get; set; }

        public ActiveValues? NeuronActiveValues { get; set; }

        public ActiveValues? TerminalActiveValues { get; set; }

        public int? Limit { get; set; }
    }
}
