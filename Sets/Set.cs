using System.Text;

namespace Sets;

public class Set<T>
{
    public List<T> Elements { get; set; } = new List<T>();
    private char _connectionCharacter = ',';

    public Set(List<T> elements)
    {
        Elements = elements;
    }

    public Set(T[] elements)
    {
        CreateSet(elements);
    }

    public Set() : this(new List<T>()) { }

    public bool ContainsElement(T element) => Elements.Contains(element);

    public void CreateSet(T[] elements)
    {
        Elements = new List<T>();
        AddElements(elements);
    }

    public void AddElement(T elementToAdd)
    {
        if (!ContainsElement(elementToAdd))
        {
            Elements.Add(elementToAdd);
        }
    }

    public void AddElements(T[] elementsToAdd)
    {
        foreach (var element in elementsToAdd)
        {
            AddElement(element);
        }
    }

    public void RemoveElement(T elementToRemove)
    {
        Elements.Remove(elementToRemove);
    }

    public Set<T> Union(Set<T> otherSet)
    {
        Set<T> unionSet = new Set<T>(this.Elements);
        unionSet.AddElements(otherSet.Elements.ToArray());
        unionSet.Elements = unionSet.Elements.OrderBy(e => e).ToList();
        return unionSet;
    }

    public Set<T> Intersection(Set<T> otherSet)
    {
        Set<T> intersectionSet = new Set<T>();
        foreach (T element in Elements)
        {
            if (otherSet.ContainsElement(element))
            {
                intersectionSet.AddElement(element);
            }
        }
        intersectionSet.Elements = intersectionSet.Elements.OrderBy(e => e).ToList();
        return intersectionSet;
    }

    public Set<T> Difference(Set<T> otherSet)
    {
        Set<T> differenceSet = new Set<T>();
        foreach (T element in Elements)
        {
            if (!otherSet.ContainsElement(element))
            {
                differenceSet.AddElement(element);
            }
        }
        return differenceSet;
    }

    public Set<T> Complement(Set<T> universalSet)
    {
        Set<T> complementSet = new Set<T>();
        foreach (T element in universalSet.Elements)
        {
            if (!ContainsElement(element))
            {
                complementSet.AddElement(element);
            }
        }
        return complementSet;
    }

    public static Set<T> EvaluateExpression(string expression, Dictionary<string, Set<T>> setDictionary)
    {
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Queue<object> queue = new Queue<object>();

        foreach (string token in tokens)
        {
            if (setDictionary.ContainsKey(token))
            {
                queue.Enqueue(setDictionary[token]);
            }
            else if (Enum.TryParse(typeof(SetOperation), token, true, out var operation))
            {
                queue.Enqueue((SetOperation)operation);
            }
        }

        Stack<Set<T>> resultSetStack = new Stack<Set<T>>();

        while (queue.Count > 1)
        {
            Set<T> setA, setB;
            SetOperation operation;

            if (resultSetStack.Count > 0)
            {
                setA = resultSetStack.Pop();
            }
            else
            {
                setA = (Set<T>)queue.Dequeue();
            }

            operation = (SetOperation)queue.Dequeue();
            setB = (Set<T>)queue.Dequeue();

            Set<T> result = operation switch
            {
                SetOperation.Intersection => setA.Intersection(setB),
                SetOperation.Union => setA.Union(setB),
                SetOperation.Difference => setA.Difference(setB),
                SetOperation.Complement => setA.Complement(setB),
                _ => throw new InvalidOperationException($"Unsupported operation: {operation}")
            };

            resultSetStack.Push(result);
        }

        return resultSetStack.Pop();
    }
    public Set<(T1, T2)> CartesianProduct<T2>(Set<T2> otherSet)
    {
        Set<(T1, T2)> productSet = new Set<(T1, T2)>();
        foreach (T1 elementA in this.Elements)
        {
            foreach (T2 elementB in otherSet.Elements)
            {
                productSet.AddElement((elementA, elementB));
            }
        }
        return productSet;
    }
    public bool IsRelationValid<T2>(List<(T1, T2)> relation, Set<T2> setB)
    {
        Set<(T1, T2)> cartesianProduct = this.CartesianProduct(setB);

        foreach ((T1, T2) pair in relation)
        {
            if (!cartesianProduct.Elements.Contains(pair))
            {
                return false;
            }
        }

        return true;
    }
    public Set<(T, T)> FindRelations(Func<T, T, bool> relationFunc)
    {
        Set<(T, T)> relationSet = new Set<(T, T)>();

        foreach (T elementA in this.Elements)
        {
            foreach (T elementB in this.Elements)
            {
                if (relationFunc(elementA, elementB))
                {
                    relationSet.AddElement((elementA, elementB));
                }
            }
        }

        return relationSet;
    }
    public Set<(T, T)> FilteredCartesianProduct(Set<T> otherSet, Func<T, T, bool> filterFunc)
    {
        Set<(T, T)> relationSet = new Set<(T, T)>();

        foreach (T elementA in this.Elements)
        {
            foreach (T elementB in otherSet.Elements)
            {
                if (filterFunc(elementA, elementB))
                {
                    relationSet.AddElement((elementA, elementB));
                }
                else
                {
                    break;
                }
            }
        }

        return relationSet;
    }
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[" + String.Join(_connectionCharacter, this.Elements) + "]");
        return stringBuilder.ToString();
    }

    public bool CompareSet(Set<T> setToCompare)
    {
        return new HashSet<T>(Elements).SetEquals(setToCompare.Elements);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Set<T> set)
        {
            return CompareSet(set);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Elements, _connectionCharacter);
    }
}