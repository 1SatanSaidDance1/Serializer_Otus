using SerializerReflections.Classes;

public static class Program
{
    static void Main()
    {
        F getFclass = F.Get();
        int numberOfRepetitions = 9999;

        ExecutionTimeMySerializer executionTimeMySerializer = new ExecutionTimeMySerializer(
            numberOfRepetitions,
            new MyCSVSerializer(",", Environment.NewLine));

        Console.WriteLine(executionTimeMySerializer.Timer(getFclass));


        ExecutionTimeJsonSerializer executionTimeJsonSerializer = new ExecutionTimeJsonSerializer(numberOfRepetitions, new JsonCSVSerializer());

        Console.WriteLine(executionTimeJsonSerializer.Timer(getFclass));

        Console.WriteLine();

    }
}
