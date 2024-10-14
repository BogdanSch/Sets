using FluentAssertions;

namespace Sets.UnitTests.SetTests;

public class SetTests
{
    [Fact]
    public void Set_Union_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3, 1 });
        Set<int> setB = new Set<int>(new List<int> { 3, 4, 5 });

        Set<int> result = setA.Union(setB);

        result.Should().Be(new Set<int>(new List<int> { 1, 2, 3, 4, 5 }));
    }

    [Fact]
    public void Set_Intersection_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3 });
        Set<int> setB = new Set<int>(new List<int> { 3, 4, 5 });

        Set<int> result = setA.Intersection(setB);

        result.Should().Be(new Set<int>(new List<int> { 3 }));
    }

    [Fact]
    public void Set_Difference_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3 });
        Set<int> setB = new Set<int>(new List<int> { 3, 4, 5 });

        Set<int> result = setA.Difference(setB);

        result.Should().Be(new Set<int>(new List<int> { 1, 2 }));
    }

    [Theory]
    [MemberData(nameof(ComplementTestData))]
    public void Set_Complement_ReturnSet(Set<int> setA, Set<int> universalSet, Set<int> expectedSet)
    {
        Set<int> result = setA.Complement(universalSet);
        result.Should().Be(expectedSet);
    }

    [Fact]
    public void Set_EvaluateExpression_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3 });
        Set<int> setB = new Set<int>(new List<int> { 3, 4, 5 });
        Set<int> setC = new Set<int>(new List<int> { 5, 6, 7 });
        Dictionary<string, Set<int>> setsDict = new Dictionary<string, Set<int>>
        {
                { "A", setA },
                { "B", setB },
                { "C", setC }
            };

        string expression = "A intersection B union C";
        Set<int> result = Set<int>.EvaluateExpression(expression, setsDict);

        result.Should().Be(new Set<int>(new List<int> { 3, 5, 6, 7 }));
    }
    public static IEnumerable<object[]> ComplementTestData()
    {
        yield return new object[]
        {
            new Set<int>(new List<int> { 1, 2, 3 }),
            new Set<int>(new List<int> { 1, 2, 3, 4, 5 }),
            new Set<int>(new List<int> { 4, 5 })
        };
    }
}
