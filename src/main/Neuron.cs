using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace works.ei8.Cortex.Graph.Common
{
    public class Neuron
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

        public string AuthorId { get; set; }

        public string AuthorTag { get; set; }

        public string LayerId { get; set; }

        public string LayerTag { get; set; }

        public string Timestamp { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
