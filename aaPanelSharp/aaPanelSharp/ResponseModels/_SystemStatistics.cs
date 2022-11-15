namespace aaPanelSharp.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal partial class _SystemStatistics
    {
        [JsonProperty("network")]
        public Dictionary<string, _Network> Network { get; set; }

        [JsonProperty("upTotal")]
        public long UpTotal { get; set; }

        [JsonProperty("downTotal")]
        public long DownTotal { get; set; }

        [JsonProperty("up")]
        public double Up { get; set; }

        [JsonProperty("down")]
        public double Down { get; set; }

        [JsonProperty("downPackets")]
        public long DownPackets { get; set; }

        [JsonProperty("upPackets")]
        public long UpPackets { get; set; }

        [JsonProperty("cpu")]
        public _Cpu[] Cpu { get; set; }

        [JsonProperty("cpu_times")]
        public object CpuTimes { get; set; }

        [JsonProperty("load")]
        public _Load Load { get; set; }

        [JsonProperty("mem")]
        public _Mem Mem { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("disk")]
        public _Disk[] Disk { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("site_total")]
        public long SiteTotal { get; set; }

        [JsonProperty("ftp_total")]
        public long FtpTotal { get; set; }

        [JsonProperty("database_total")]
        public long DatabaseTotal { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("installed")]
        public bool Installed { get; set; }

        [JsonProperty("user_info")]
        public _UserInfo UserInfo { get; set; }

        [JsonProperty("iostat")]
        public _Iostat Iostat { get; set; }
    }

    internal partial class _Disk
    {
        [JsonProperty("filesystem")]
        public string Filesystem { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("size")]
        public string[] Size { get; set; }

        [JsonProperty("inodes")]
        public string[] Inodes { get; set; }
    }

    internal partial class _Iostat : Dictionary<string, _All>
    {
    }

    internal partial class _All
    {
        [JsonProperty("read_count")]
        public long ReadCount { get; set; }

        [JsonProperty("write_count")]
        public long WriteCount { get; set; }

        [JsonProperty("read_bytes")]
        public long ReadBytes { get; set; }

        [JsonProperty("write_bytes")]
        public long WriteBytes { get; set; }

        [JsonProperty("read_time")]
        public long ReadTime { get; set; }

        [JsonProperty("write_time")]
        public long WriteTime { get; set; }

        [JsonProperty("read_merged_count")]
        public long ReadMergedCount { get; set; }

        [JsonProperty("write_merged_count")]
        public long WriteMergedCount { get; set; }
    }

    internal partial class _Load
    {
        [JsonProperty("one")]
        public double One { get; set; }

        [JsonProperty("five")]
        public double Five { get; set; }

        [JsonProperty("fifteen")]
        public double Fifteen { get; set; }

        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("limit")]
        public long Limit { get; set; }

        [JsonProperty("safe")]
        public long Safe { get; set; }
    }

    internal partial class _Mem
    {
        [JsonProperty("memTotal")]
        public long MemTotal { get; set; }

        [JsonProperty("memFree")]
        public long MemFree { get; set; }

        [JsonProperty("memBuffers")]
        public long MemBuffers { get; set; }

        [JsonProperty("memCached")]
        public long MemCached { get; set; }

        [JsonProperty("memRealUsed")]
        public long MemRealUsed { get; set; }
    }

    internal partial class _Network
    {
        [JsonProperty("upTotal")]
        public long UpTotal { get; set; }

        [JsonProperty("downTotal")]
        public long DownTotal { get; set; }

        [JsonProperty("up")]
        public double Up { get; set; }

        [JsonProperty("down")]
        public double Down { get; set; }

        [JsonProperty("downPackets")]
        public long DownPackets { get; set; }

        [JsonProperty("upPackets")]
        public long UpPackets { get; set; }
    }

    internal partial class _UserInfo
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public _Data Data { get; set; }
    }

    internal partial class _Data
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }

    internal partial struct _Cpu
    {
        public double? Double;
        public double[] DoubleArray;
        public string String;

        public static implicit operator _Cpu(double Double) => new _Cpu { Double = Double };
        public static implicit operator _Cpu(double[] DoubleArray) => new _Cpu { DoubleArray = DoubleArray };
        public static implicit operator _Cpu(string String) => new _Cpu { String = String };
    }

    internal static class _Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                _CpuConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class _CpuConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(_Cpu) || t == typeof(_Cpu?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                case JsonToken.Float:
                    var doubleValue = serializer.Deserialize<double>(reader);
                    return new _Cpu { Double = doubleValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new _Cpu { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<double[]>(reader);
                    return new _Cpu { DoubleArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type Cpu");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (_Cpu)untypedValue;
            if (value.Double != null)
            {
                serializer.Serialize(writer, value.Double.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.DoubleArray != null)
            {
                serializer.Serialize(writer, value.DoubleArray);
                return;
            }
            throw new Exception("Cannot marshal type Cpu");
        }

        public static readonly _CpuConverter Singleton = new _CpuConverter();
    }
}