using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GcNutritionCenter
{
    internal class JsonFile
    {
        public static void SaveToFile(object value, string filename, string? path = null)
        {
            string jsonString = JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = true });
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

                string jsonString = File.ReadAllText(fullPath);
                T _obj = JsonSerializer.Deserialize<T>(jsonString)!;

                return _obj;
            }
            catch(System.IO.FileNotFoundException)
            {
                return default;
            }
        }

    }
}
