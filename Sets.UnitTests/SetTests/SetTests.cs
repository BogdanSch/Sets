using FluentAssertions;
using System.Collections.Generic;

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
    [Fact]
    public void Set_CartesianProduct_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2 });
        Set<char> setB = new Set<char>(new List<char> { 'a', 'b' });

        Set<(int, char)> result = setA.CartesianProduct(setB);

        Set<(int, char)> expected = new Set<(int, char)>(new List<(int, char)>
        {
            (1, 'a'), (1, 'b'), (2, 'a'), (2, 'b')
        });
        result.Should().BeEquivalentTo(expected);
    }
    [Fact]
    public void Set_IsRelationValid_ReturnBool()
    {
        var relation = new List<(int, char)> { (1, 'a'), (2, 'b') };
        var setA = new Set<int>(new List<int> { 1, 2 });
        var setB = new Set<char>(new List<char> { 'a', 'b' });

        bool isValid = setA.IsRelationValid(relation, setB);

        isValid.Should().BeTrue();
    }
    [Fact]
    public void Set_FindRelations_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3, 4, 6 });
        Func<int, int, bool> isDivisible = (a, b) => a != b && a % b == 0;

        Set<(int, int)> result = setA.FindRelations(isDivisible);

        Set<(int, int)> expected = new Set<(int, int)>(new List<(int, int)>
        {
            (2, 1), (3, 1), (4, 1), (4, 2), (6, 1), (6, 2), (6, 3) //Ask about the expected output (3, 1)
        });
        result.Should().BeEquivalentTo(expected);
    }
    [Fact]
    public void Set_FilteredCartesianProduct_ReturnSet()
    {
        Set<int> setA = new Set<int>(new List<int> { 1, 2, 3 });
        Set<int> setB = new Set<int>(new List<int> { 3, 4, 5 });
        Func<int, int, bool> isLess = (a, b) => a < b;// && a != b;

        Set<(int, int)> result = setA.FilteredCartesianProduct(setB, isLess);

        Set<(int, int)> expected = new Set<(int, int)>(new List<(int, int)>
        {
            (1, 3),(1, 4),(1, 5),(2, 3),(2, 4),(2, 5),(3, 4),(3, 5)
        });
        result.Should().BeEquivalentTo(expected);
    }
    [Theory]
    [MemberData(nameof(IsReflexiveTestData))]
    public void Set_IsRelationReflexive_ReturnBool(Set<int> set, Set<(int, int)> relation)
    {
        bool result = set.IsRelationReflexive(relation);
        result.Should().BeTrue();
    }
    [Fact]
    public void Set_IsRelationSymmetric_ReturnBool()
    {
        Set<(int, int)> relation = new Set<(int, int)>(new List<(int, int)>
            {(1, 2), (2, 1), (3, 3)}
        );

        bool result = Set<int>.IsRelationSymmetric(relation);

        result.Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(IsAntisymmetricTestData))]
    public void Set_IsRelationAntisymmetric_ReturnBool(Set<(int, int)> relation)
    {
        bool result = Set<int>.IsRelationAntisymmetric(relation);

        result.Should().BeTrue();
    }
    [Fact]
    public void Set_IsRelationTransitive_ReturnBool()
    {
        Set<(int, int)> relation = new Set<(int, int)>(new List<(int, int)>
        {(1, 2), (2, 3), (1, 3)}
        );

        bool result = Set<int>.IsRelationTransitive(relation);

        result.Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(IsEquivalentTestData))]
    public void Set_IsRelationEquivalent_ReturnBool(Set<int> set, Set<(int, int)> relation)
    {
        bool result = set.IsRelationEquivalent(relation);
        result.Should().BeTrue();
    }
    [Theory]
    [MemberData(nameof(InverseRelationTestData))]
    public void Set_InverseRelation_ReturnSet(Set<(int, int)> relation, Set<(int, int)> expectedResult)
    {
        Set<(int, int)> result = Set<int>.InverseRelation(relation);
        result.Should().BeEquivalentTo(expectedResult);
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
    public static IEnumerable<object[]> IsReflexiveTestData()
    {
        yield return new object[]
        {
            new Set<int>(new List<int> { 1, 2, 3 }),
            new Set<(int, int)>(new List<(int, int)> { (1, 1), (2, 2), (3, 3)})
        };
        yield return new object[]
        {
            new Set<int>(new List<int> { 3, 4, 1 }),
            new Set<(int, int)>(new List<(int, int)> { (3, 3), (4, 4), (1, 1)})
        };
    }
    public static IEnumerable<object[]> IsAntisymmetricTestData()
    {
        yield return new object[]
        {
            new Set<(int, int)>(new List<(int, int)> {(1, 1), (1, 2), (2, 2), (3, 3)} )
        };
        yield return new object[]
        {
            new Set<(int, int)>(new List<(int, int)> {(1, 1), (2, 2), (3, 3), (4, 2)} )
        };
    }
    public static IEnumerable<object[]> IsEquivalentTestData()
    {
        yield return new object[]
        {
            new Set<int>(new List<int> { 1, 2, 3, 4 }),
            new Set <(int, int) >(new List<(int, int)>
                {(1, 1), (1, 3), (2, 2), (2, 4), (3, 1), (3, 3), (4, 2), (4, 4)}
            )
        };
        yield return new object[]
        {
           new Set<int>(new List<int> {3, 4, 5}),
           new Set<(int, int) >(new List<(int, int)>
                {(3, 3), (4, 4), (5, 5), (3, 5), (5, 3), (3, 4), (4, 5), (5, 4), (4, 3)}
           )
        };
    }
    public static IEnumerable<object[]> InverseRelationTestData()
    {
        yield return new object[]
        {
            new Set<(int, int)>(new List<(int, int)>  {(1, 2), (3, 4), (5, 6) }),
            new Set<(int, int) >(new List<(int, int)> { (4, 3), (6, 5), (2, 1) })
        };
        yield return new object[]
        {
            new Set<(int, int) >(new List<(int, int)> {  (3, 3), (4, 4), (1, 1), (int.MaxValue, 0) }),
            new Set<(int, int) >(new List<(int, int)> { (3, 3), (4, 4), (1, 1), (0, int.MaxValue) })
        };
    }
}
