using Newtonsoft.Json;

namespace SerializerReflections.Classes
{
    public class JsonCSVSerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);

        }

        public T Deserialiser<T>(string data) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
