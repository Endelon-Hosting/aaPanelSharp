namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class _FTP
    {
        [JsonProperty("where")]
        public string Where { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("data")]
        public _FTPDatum[] Data { get; set; }
    }

    public partial class _FTPDatum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("pid")]
        public long Pid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ps")]
        public string Comment { get; set; }

        [JsonProperty("addtime")]
        public DateTimeOffset Addtime { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("quota")]
        public _FTPQuota Quota { get; set; }
    }

    public partial class _FTPQuota
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("used")]
        public long Used { get; set; }

        [JsonProperty("free")]
        public long Free { get; set; }
    }
}