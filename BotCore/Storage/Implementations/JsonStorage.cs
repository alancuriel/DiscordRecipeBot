using System.IO;
using Newtonsoft.Json;
using static System.IO.Directory;

namespace BotCore.Storage.Implementations
{
    public class JsonStorage : IDataStorage
    {
        public T RestoreObject<T>(string key)
        {
            var json = File.ReadAllText($"{key}.json");
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void StoreObject(object obj, string key)
        {
            string file = $"{key}.json";
            CreateDirectory(Path.GetDirectoryName(file));
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(file, json);
        }
    }
}
