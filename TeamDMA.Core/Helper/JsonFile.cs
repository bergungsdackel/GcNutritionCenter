using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TeamDMA.Core.Helper;
using TeamDMA.Core.Logging;

namespace TeamDMA.Core.Helper
{
    // TODO: Do a bulk save/read test
    public class JsonFile
    {
        private static readonly ILogger Logger = LogManager.GetLogger<JsonFile>();

        private class HelperObjectToSave<T>
        {
            public HelperObjectToSave(T values, DateTime timestamp = default(DateTime))
            {
                this.version = typeof(JsonFile).Assembly.GetName().Version ?? new Version();
                this.values = values;
                this.timestamp = timestamp;
            }
            public System.Version version { get; set; }

            public DateTime timestamp { get; set; }

            public T values { get; set; }
        }

        public static void SaveToFile<T>(T values, string filename, string? customPath = null, string? pathToServer = null)
        {
            HelperObjectToSave<T> helperObject = new HelperObjectToSave<T>(values, DateTime.UtcNow);

            string jsonString = JsonSerializer.Serialize(helperObject, new JsonSerializerOptions { WriteIndented = true });
            //string saveToDir = Directory.GetCurrentDirectory();
            string saveToDir = Configuration.GetCurrentAppDataDir(System.Reflection.Assembly.GetCallingAssembly());
            Directory.CreateDirectory(saveToDir);

            if (customPath != null)
            {
                Directory.CreateDirectory(customPath);
                saveToDir = customPath;
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

        public static T? ReadFromFile<T>(string filename, string? customPath = null, string? pathToServer = null)
        {
            //string readFromDir = Directory.GetCurrentDirectory();
            string readFromDir = Configuration.GetCurrentAppDataDir(System.Reflection.Assembly.GetCallingAssembly());

            if (customPath != null)
            {
                if (Directory.Exists(customPath))
                {
                    readFromDir = customPath;
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
                try
                {
                    string jsonString = File.ReadAllText(fullPath);
                    HelperObjectToSave<T> _obj = JsonSerializer.Deserialize<HelperObjectToSave<T>> (jsonString, new JsonSerializerOptions { WriteIndented = true })!;
                    DateTime timestampUtc = _obj.timestamp.ToLocalTime();

                    // TODO: Read binary(?) from sql then to string and JsonSerializer.Deserialize
                    if(pathToServer != null)
                    {
                        // from ftp?
                    }

                    // TODO: Good programming?
                    try
                    {
                        return _obj.values;
                    }
                    catch(InvalidCastException)
                    {
                        return default;
                    }
                }
                catch(Exception ex)
                {
                    Logger.Error("Error while reading file. Doing backup and returning default.", ex);

                    // backup old file, because it actually exists

                    string backupFolderPath = Path.Combine(readFromDir, "Backups");
                    Directory.CreateDirectory(backupFolderPath);

                    string backupFilePath = Path.Combine(backupFolderPath, $"{_filename}_{DateTime.Now:HH-mm-ss-dd-MM-yyyy}");

                    try
                    {
                        File.Copy(fullPath, backupFilePath); // maybe here try catch and log error
                    }
                    catch(Exception exInner)
                    {
                        Logger.Error("Can't copy backup file.", exInner);
                    }
                    

                    return default; // anyway return default
                }

            }
            else
            {
                Logger.Info($"No file '{_filename}' found. Returning default.");
                return default;
            }
        }

    }
}
