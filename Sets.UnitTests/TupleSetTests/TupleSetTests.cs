using FluentAssertions;

namespace Sets.UnitTests.TupleSetTests;


public class TupleSetTests
{

    [Fact]
    public void Set_InverseRelation_ReturnTupleSet()
    {
        TupleSet<int> relation = new TupleSet<int>(new List<(int, int)>
            {(1, 2), (3, 4), (5, 6)}
        );
        TupleSet<int> expectedResult = new TupleSet<int>(new List<(int, int)>
            {(2, 1), (4, 3), (6, 5)}
        );

        TupleSet<int> result = relation.InverseRelation();

        result.Should().BeEquivalentTo(expectedResult);
    }
}
