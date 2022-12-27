using Newtonsoft.Json;

namespace aaPanelSharp.ResponseModels;

public class _PhpVersion
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}