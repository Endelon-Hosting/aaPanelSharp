namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _DbCreate
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}