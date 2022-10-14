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

        private class HelperObjectToSave<T>
        {
            public HelperObjectToSave(T values, DateTime timestamp = default(DateTime))
            {
                this.values = values;
                this.timestamp = timestamp;
            }

            public DateTime timestamp { get; set; }

            public T values { get; set; }
        }

        public static void SaveToFile<T>(T values, string filename, string? path = null, string? pathToServer = null)
        {
            HelperObjectToSave<T> helperObject = new HelperObjectToSave<T>(values, DateTime.UtcNow);

            string jsonString = JsonSerializer.Serialize(helperObject, new JsonSerializerOptions { WriteIndented = true });
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
            if(pathToServer != null)
            {
                // to ftp?
            }
        }

        public static T? ReadFromFile<T>(string filename, string? path = null, string? pathToServer = null)
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
                    HelperObjectToSave<T> _obj = JsonSerializer.Deserialize<HelperObjectToSave<T>> (jsonString, new JsonSerializerOptions { WriteIndented = true })!;
                    DateTime timestampUtc = _obj.timestamp.ToLocalTime();

                    // TODO: Read binary(?) from sql then to string and JsonSerializer.Deserialize
                    if(pathToServer != null)
                    {
                        // from ftp?
                    }

                    try
                    {
                        return _obj.values;
                    }
                    catch(InvalidCastException)
                    {
                        return default;
                    }
                }
                else
                {
                    return default;
                }

            }
            catch(System.IO.FileNotFoundException)
            {
                return default;
            }
        }

    }
}
