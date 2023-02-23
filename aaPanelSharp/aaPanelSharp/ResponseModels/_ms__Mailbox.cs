namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _ms__Mailbox
    {
        [JsonProperty("data")]
        public _ms__MailboxDatum[] Data { get; set; }

        [JsonProperty("page")]
        public string Page { get; set; }
    }

    internal partial class _ms__MailboxDatum
    {
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("quota")]
        public long Quota { get; set; }

        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty("modified")]
        public DateTimeOffset Modified { get; set; }

        [JsonProperty("active")]
        public long Active { get; set; }

        [JsonProperty("is_admin")]
        public long IsAdmin { get; set; }
    }
}