namespace Sets;

public class TupleSet<T> : Set<(T, T)>
{
    public TupleSet() : base()
    {
    }

    public TupleSet(List<(T, T)> elements) : base(elements)
    {
    }

    public TupleSet<T> InverseRelation()
    {
        TupleSet<T> inverseSet = new TupleSet<T>();

        foreach (var pair in this.Elements)
        {
            inverseSet.AddElement((pair.Item2, pair.Item1));
        }

        return inverseSet;
    }
}
