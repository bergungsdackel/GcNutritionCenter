using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GcNutritionCenter
{
    internal class JsonFile
    {
        public static void SaveToFile(object value, string filename, string? path = null)
        {
            // TODO: With field names?
            //var tupleToSerialize = (Timestamp: DateTime.UtcNow, Values: value);
            //string jsonString = JsonSerializer.Serialize(new { tupleToSerialize.Timestamp, tupleToSerialize.Values }, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            string jsonString = JsonSerializer.Serialize((DateTime.UtcNow, value), new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            string saveToDir = Directory.GetCurrentDirectory();

            if (path != null)
            {
                Directory.CreateDirectory(path);
                saveToDir = path;
            }

            string _filename = filename;
            if(!filename.Contains(".json"))
            {
                _filename = _filename + ".json";
            }
            string fullPath = Path.Combine(saveToDir, _filename);

            File.WriteAllText(fullPath, jsonString);

            // TODO: Store jsonString as binary(?) in SQL
        }

        public static T? ReadFromFile<T>(string filename, string? path = null)
        {
            try
            {
                string readFromDir = Directory.GetCurrentDirectory();

                if (path != null)
                {
                    if (Directory.Exists(path))
                    {
                        readFromDir = path;
                    }
                }

                string _filename = filename;
                if (!filename.Contains(".json"))
                {
                    _filename = _filename + ".json";
                }

                string fullPath = Path.Combine(readFromDir, _filename);

                if(File.Exists(fullPath))
                {
                    string jsonString = File.ReadAllText(fullPath);
                    Tuple<DateTime, T> _obj = JsonSerializer.Deserialize<Tuple<DateTime, T>>(jsonString, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true })!;
                    DateTime timestampUtc = _obj.Item1.ToLocalTime();

                    // TODO: Read binary(?) from sql then to string and JsonSerializer.Deserialize

                    return _obj.Item2;
                }
                else
                {
                    return default(T);
                }

            }
            catch(System.IO.FileNotFoundException)
            {
                return default;
            }
        }

    }
}
