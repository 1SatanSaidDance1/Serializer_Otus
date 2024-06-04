using System.Text;
using System.Diagnostics;

namespace SerializerReflections.Classes
{
    internal class ExecutionTimeJsonSerializer
    {
        private readonly int numberOfRepetitions;

        private readonly JsonCSVSerializer myJsonSerializer;

        private readonly string csvFile = @"C:\Users\Home\Source\Repos\Serializer_Otus\Data.csv";

  

        public ExecutionTimeJsonSerializer(int numberOfRepetitions, JsonCSVSerializer myJsonSerializer)
        {
            this.numberOfRepetitions = numberOfRepetitions;
            this.myJsonSerializer = myJsonSerializer;
        }

        public string Timer<T>(T obj) where T : new()
        {
            var file = csvFile;
            string prop = string.Empty;
            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch sw2 = Stopwatch.StartNew();
            Stopwatch sw3 = Stopwatch.StartNew();

            string info = $"Среда разработки {Environment.Version}\r\n";
            info += "JsonSerializer\r\n";
            info += $"Количество итераций: {numberOfRepetitions}\r\n\r\n";

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = myJsonSerializer.Serialize(obj);
                File.WriteAllText(file, prop, Encoding.UTF8);
            }

            sw.Start();

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = myJsonSerializer.Serialize(obj);
            }

            sw.Stop();

            sw2.Start();
            info += $"Реузльтат сериализации:\r\n{prop} \r\n\r\n";
            info += $"Реультат выполнения: {sw.ElapsedMilliseconds}мс \r\n";
            sw2.Stop();

            sw.Restart();

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = File.ReadAllText(file);
                T objUot = myJsonSerializer.Deserialiser<T>(prop);
            }

            sw.Stop();

            sw3.Start();
            info += $"Время десериализации: {sw.ElapsedMilliseconds}мс \r\n\r\n";
            sw3.Stop();

            info += $"Время написания сообщения {sw2.Elapsed + sw3.Elapsed}мс \r\n\r\n";

            return info;
        }
    }
}
