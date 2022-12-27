using Newtonsoft.Json;

namespace aaPanelSharp.RequestModels;

internal class _Webname
{
    [JsonProperty("domain")]
    public string Domain { get; set; }

    [JsonProperty("domainlist")]
    public string[] Domainlist { get; set; }

    [JsonProperty("count")]
    public long Count { get; set; }
}