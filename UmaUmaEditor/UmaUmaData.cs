using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UmaUmaEditor
{
    enum Kinds
    {
        KIND_CHARACTOR = 0,
        KIND_SUPPORT = 1,
    }

    class OptionEffect
    {
        [JsonPropertyName("Option")]
        public string option { get; set; }

        [JsonPropertyName("Effect")]
        public string effect { get; set; }
    }

    class CardEvent
    {
        [JsonPropertyName("Event")]
        public List<Dictionary<string, List<OptionEffect>>> events { get; set; }
    }

    internal class UmaUmaData
    {
        [JsonPropertyName("Charactor")]
        public Dictionary<string, Dictionary<string, CardEvent>> charas { get; set; }

        [JsonPropertyName("Support")]
        public Dictionary<string, Dictionary<string, CardEvent>> supports { get; set; }
    }
}
