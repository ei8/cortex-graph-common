using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    public class NeuronResult
    {
        public string Id { get; set; }

        public string Tag { get; set; }

        public Terminal Terminal { get; set; }

        [JsonIgnore]
        public RelativeType Type => this.Terminal != null && this.Terminal.PresynapticNeuronId != null ?
            this.Terminal.PresynapticNeuronId.EndsWith(this.Id) ?
                RelativeType.Presynaptic :
                RelativeType.Postsynaptic :
            RelativeType.NotSet;

        public int Version { get; set; }

        public NeuronInfo Region { get; set; }

        public AuthorEventInfo Creation { get; set; }

        public AuthorEventInfo LastModification { get; set; }

        public AuthorEventInfo UnifiedLastModification { get; set; }

        public ExternalReferenceInfo ExternalReference { get; set; }

        public bool Active { get; set; }
    }
}
