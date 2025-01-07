using Sets;

public class Program
{
    private static void Main(string[] args)
    {
        
        List<int> sequence = Enumerable.Range(1, 50).ToList();
        Set<int> set = new Set<int>(sequence);//new Set<int>(new List<int> { 1, 2, 3, 4 });
        Func<int, int, bool> relationCondition = (a, b) => a + b == a * b;

        (Set<(int, int)> relationSet, Dictionary<string, bool> properties) = set.GenerateAndValidateRelations(relationCondition);

        Console.WriteLine("Relation Set:");
        Console.WriteLine(relationSet.ToString());

        Console.WriteLine("\nProperties:");
        foreach (KeyValuePair<string, bool> property in properties)
        {
            Console.WriteLine($"{property.Key}: {property.Value}");
        }
    }
}