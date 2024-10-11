using FluentAssertions;
using Sets;

namespace Sets.UnitTests.SetTests;

public class SetTests
{
    [Fact]
    public void Set_Union_ReturnSet()
    {
        Set setA = new Set(new List<int> { 1, 2, 3 });
        Set setB = new Set(new List<int> { 3, 4, 5 });

        Set result = setA.Union(setB);

        result.Should().Be(new Set(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Fact]
    public void Set_Intersection_ReturnSet()
    {
        Set setA = new Set(new List<int> { 1, 2, 3 });
        Set setB = new Set(new List<int> { 3, 4, 5 });

        Set result = setA.Intersection(setB);

        result.Should().Be(new Set(new List<int> { 3 }));
    }

    [Fact]
    public void Set_Difference_ReturnSet()
    {
        Set setA = new Set(new List<int> { 1, 2, 3 });
        Set setB = new Set(new List<int> { 3, 4, 5 });

        Set result = setA.Difference(setB);

        result.Should().Be(new Set(new List<int> { 1, 2 }));
    }

    [Theory]
    [MemberData(nameof(ComplementTestData))]
    public void Set_Complement_ReturnSet(Set setA, Set universalSet, Set expectedSet)
    {
        Set result = setA.Complement(universalSet);

        result.Should().Be(expectedSet);
    }

    [Fact]
    public void Set_EvaluateExpression_ReturnSet()
    {
        Set setA = new Set(new List<int> { 1, 2, 3 });
        Set setB = new Set(new List<int> { 3, 4, 5 });
        Set setC = new Set(new List<int> { 5, 6, 7 });
        Dictionary<string, Set> setsDict = new Dictionary<string, Set>
        {
                { "A", setA },
                { "B", setB },
                { "C", setC }
            };

        string expression = "A intersection B union C";
        Set result = Set.EvaluateExpression(expression, setsDict);

        result.Should().Be(new Set(new List<int> { 3, 5, 6, 7 }));
    }
    public static IEnumerable<object[]> ComplementTestData()
    {
        yield return new object[]
        {
            new Set(new List<int> { 1, 2, 3 }),
            new Set(new List<int> { 1, 2, 3, 4, 5 }),
            new Set(new List<int> { 4, 5 })
        };
    }
}
