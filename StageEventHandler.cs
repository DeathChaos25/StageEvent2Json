using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace StageEvent2Json
{
    public static class StageEventHandler
    {
        public static TblFile ReadBinary(string filePath)
        {
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                return new TblFile(reader);
            }
        }

        public static void WriteBinary(string filePath, TblFile tblFile)
        {
            using (var writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                tblFile.Write(writer);
            }
        }

        public static string ToJson(TblFile tblFile)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };
            return JsonConvert.SerializeObject(tblFile, settings);
        }

        public static TblFile FromJson(string json)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };
            return JsonConvert.DeserializeObject<TblFile>(json, settings);
        }
    }

}
