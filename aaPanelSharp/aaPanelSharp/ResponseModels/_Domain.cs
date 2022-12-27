using Newtonsoft.Json;

namespace aaPanelSharp.ResponseModels;

public class _Domain
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("pid")]
    public long Pid { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("port")]
    public long Port { get; set; }

    [JsonProperty("addtime")]
    public DateTimeOffset Addtime { get; set; }
}