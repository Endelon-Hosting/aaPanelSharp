namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _DbList
    {
        [JsonProperty("where")]
        public string Where { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }

        [JsonProperty("data")]
        public _Datum[] Data { get; set; }
    }

    internal partial class _Datum
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("sid")]
        public long Sid { get; set; }

        [JsonProperty("pid")]
        public long Pid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("ps")]
        public string Ps { get; set; }

        [JsonProperty("addtime")]
        public DateTimeOffset Addtime { get; set; }

        [JsonProperty("db_type")]
        public long DbType { get; set; }

        [JsonProperty("conn_config")]
        public _ConnConfig ConnConfig { get; set; }

        [JsonProperty("backup_count")]
        public long BackupCount { get; set; }

        [JsonProperty("quota")]
        public _Quota Quota { get; set; }
    }

    internal partial class _ConnConfig
    {
    }

    public partial class _Quota
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("used")]
        public long Used { get; set; }
    }
}
