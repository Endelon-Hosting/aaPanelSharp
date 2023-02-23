namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _ms__Domains
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("msg")]
        public _ms__DomainsMsg Msg { get; set; }
    }

    internal partial class _ms__DomainsMsg
    {
        [JsonProperty("data")]
        public _ms__DomainsDatum[] Data { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
    }

    internal partial class _ms__DomainsDatum
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("a_record")]
        public string ARecord { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("active")]
        public long Active { get; set; }

        [JsonProperty("mx_status")]
        public long MxStatus { get; set; }

        [JsonProperty("spf_status")]
        public long SpfStatus { get; set; }

        [JsonProperty("dkim_status")]
        public long DkimStatus { get; set; }

        [JsonProperty("dmarc_status")]
        public long DmarcStatus { get; set; }

        [JsonProperty("a_status")]
        public long AStatus { get; set; }

        [JsonProperty("dkim_value")]
        public string DkimValue { get; set; }

        [JsonProperty("dmarc_value")]
        public string DmarcValue { get; set; }

        [JsonProperty("mx_record")]
        public string MxRecord { get; set; }

        [JsonProperty("ssl_status")]
        public bool SslStatus { get; set; }

        [JsonProperty("catch_all")]
        public bool CatchAll { get; set; }

        [JsonProperty("ssl_info")]
        public _ms__DomainsSslInfo SslInfo { get; set; }
    }

    internal partial class _ms__DomainsSslInfo
    {
        [JsonProperty("dns")]
        public string[] Dns { get; set; }
    }
}
