using FluentAssertions;

namespace Sets.UnitTests.TupleSetTests;


public class TupleSetTests
{

    [Theory]
    [MemberData(nameof(InverseRelationTestData))]
    public void Set_InverseRelation_ReturnTupleSet(TupleSet<int> relation, TupleSet<int> expectedResult)
    {
        TupleSet<int> result = relation.InverseRelation();
        result.Should().BeEquivalentTo(expectedResult);
    }
    public static IEnumerable<object[]> InverseRelationTestData()
    {
        yield return new object[]
        {
            new TupleSet<int>(new List<(int, int)>  {(1, 2), (3, 4), (5, 6) }),
            new TupleSet<int>(new List<(int, int)> { (4, 3), (6, 5), (2, 1) })
        };
        yield return new object[]
        {
            new TupleSet<int>(new List<(int, int)> {  (3, 3), (4, 4), (1, 1), (int.MaxValue, 0) }),
            new TupleSet<int>(new List<(int, int)> { (3, 3), (4, 4), (1, 1), (0, int.MaxValue) })
        };
    }
}
