using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class Terminal
    {
        public string Id { get; set; }
        public string PresynapticNeuronId { get; set; }
        public string PostsynapticNeuronId { get; set; }
        public string Effect { get; set; }
        public string Strength { get; set; }
        public int Version { get; set; }
        public AuthorEventInfo Creation { get; set; }
        public AuthorEventInfo LastModification { get; set; }
        public ExternalReferenceInfo ExternalReference { get; set; }
        public bool Active { get; set; }
    }
}
