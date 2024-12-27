using Sets;

public class Program
{
    private static void Main(string[] args)
    {
        Set<int> set = new Set<int>(new List<int> { 1, 2, 3 });
        Func<int, int, bool> relationCondition = (a, b) => a > b;

        var (relationSet, properties) = set.GenerateAndValidateRelations(relationCondition);

        Console.WriteLine("Relation Set:");
        Console.WriteLine(relationSet.ToString());

        Console.WriteLine("\nProperties:");
        foreach (KeyValuePair<string, bool> property in properties)
        {
            Console.WriteLine($"{property.Key}: {property.Value}");
        }
    }
}