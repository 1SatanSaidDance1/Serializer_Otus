using System.Text;
using System.Diagnostics;

namespace SerializerReflections.Classes
{
    internal class ExecutionTimeMySerializer
    {
        private readonly int numberOfRepetitions;

        private readonly MyCSVSerializer myCSVSerializer;

        private readonly string csvFile = @"C:\Users\Home\Source\Repos\Serializer_Otus\Data.csv";

        public ExecutionTimeMySerializer(int numberOfRepetitions, MyCSVSerializer myCSVSerializer)
        {
            this.numberOfRepetitions = numberOfRepetitions;
            this.myCSVSerializer = myCSVSerializer;
        }

        public string Timer<T>(T obj) where T : new()
        {
            var file = csvFile;
            string prop = string.Empty;
            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch sw2 = Stopwatch.StartNew();
            Stopwatch sw3 = Stopwatch.StartNew();

            string info = $"Среда разработки {Environment.Version}\r\n";
            info += "MySerializer\r\n";
            info += $"Количество итераций: {numberOfRepetitions}\r\n\r\n";

            //Для записи в файл
            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = myCSVSerializer.Serialize(obj);
                File.WriteAllText(file, prop, Encoding.UTF8);
            }

            //Для того, чтобы точно засечь время
            sw.Start();

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = myCSVSerializer.Serialize(obj);
            }

            sw.Stop();

            sw2.Start();
            info += $"Реузльтат сериализации:\r\n{prop} \r\n\r\n";
            info += $"Реультат выполнения: {sw.ElapsedMilliseconds}мс\r\n";
            sw2.Stop() ;


            sw.Restart() ;

            for (int i = 0; i < numberOfRepetitions; i++)
            {
                prop = File.ReadAllText(file);
                T objUot = myCSVSerializer.Deserialiser<T>(prop);
            }

            sw.Stop();

            sw3 .Start();
            info += $"Время десериализации: {sw.ElapsedMilliseconds}мс\r\n\r\n";
            sw3.Stop() ;

            info += $"Время вывода сообщения {sw2.Elapsed + sw3.Elapsed}мс\r\n\r\n";

            return info;
        }
    }
}
