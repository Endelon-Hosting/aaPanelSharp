namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _Site
    {
        [JsonProperty("where")]
        public string Where { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("data")] public _SDatum[] Data { get; set; } = { };
    }

    internal partial class _SDatum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ps")]
        public string Ps { get; set; }

        [JsonProperty("addtime")]
        public DateTimeOffset Addtime { get; set; }

        [JsonProperty("edate")]
        public string Edate { get; set; }

        [JsonProperty("backup_count")]
        public long BackupCount { get; set; }

        [JsonProperty("quota")]
        public _SQuota Quota { get; set; }

        [JsonProperty("domain")]
        public long Domain { get; set; }

        [JsonProperty("ssl")]
        public long Ssl { get; set; }

        [JsonProperty("php_version")]
        public string PhpVersion { get; set; }

        [JsonProperty("attack")]
        public long Attack { get; set; }

        [JsonProperty("project_type")]
        public string ProjectType { get; set; }
    }

    internal partial class _SQuota
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("used")]
        public long Used { get; set; }

        [JsonProperty("free")]
        public long Free { get; set; }
    }
}