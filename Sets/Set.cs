    using System.Text;

    namespace Sets;

    public class Set
    {
        public List<int> Numbers { get; set; } = new List<int>();
        private char _connectionCharacter = ',';
        private static string[] _setOperations = { "intersection", "union" };
    
        public Set(List<int> numbers)
        {
            Numbers = numbers;
        }
        public Set(int[] numbers)
        {
            CreateSet(numbers);
        }
        public Set() : this(new List<int>()) { }
    
        public bool ContainsElement(int number) => Numbers.Contains(number);
        public void CreateSet(int[] numbers)
        {
            Numbers = new List<int>();
            AddElements(numbers);
        }
        public void AddElement(int numberToAdd)
        {
            if (!ContainsElement(numberToAdd))
                {
                    Numbers.Add(numberToAdd);
                }
        }
        public void AddElements(int[] numbersToAdd)
        {
            for (int i = 0; i < numbersToAdd.Length; i++)
            {
                if (!ContainsElement(numbersToAdd[i]))
                {
                    Numbers.Add(numbersToAdd[i]);
                }
            }
        }
        public void RemoveElement(int numberToRemove)
        {
            Numbers.Remove(numberToRemove);
        }

        public Set Union(Set otherSet) 
        {
            Set unionSet = new Set(this.Numbers);
            unionSet.AddElements(otherSet.Numbers.ToArray());
            unionSet.Numbers = unionSet.Numbers.OrderBy(n => n).ToList();
        return unionSet;
        }

        public Set Intersection(Set otherSet)
        {
            Set intersectionSet = new Set();
            foreach (int number in Numbers)
            {
                if (otherSet.ContainsElement(number))
                {
                    intersectionSet.AddElement(number);
                }
            }
            intersectionSet.Numbers = intersectionSet.Numbers.OrderBy(n => n).ToList();
            return intersectionSet;
        }

        public Set Difference(Set otherSet)
        {
            Set differenceSet = new Set();
            foreach (int number in Numbers)
            {
                if (!otherSet.ContainsElement(number))
                {
                    differenceSet.AddElement(number);
                }
            }
            return differenceSet;
        }
        public Set Complement(Set universalSet)
        {
            Set complementSet = new Set();

            foreach (int number in universalSet.Numbers)
            {
                if (!ContainsElement(number))
                {
                    complementSet.AddElement(number);
                }
            }

            return complementSet;
        }

    public static Set EvaluateExpression(string expression, Dictionary<string, Set> setDictionary)
    {
        string[] tokens = expression.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Queue<object> queue = new Queue<object>();

        foreach (string token in tokens)
        {
            if (setDictionary.ContainsKey(token))
            {
                queue.Enqueue(setDictionary[token]);
            }
            else if (_setOperations.Contains(token))
            {
                queue.Enqueue(token);
            }
        }

        Stack<Set> resultSetStack = new Stack<Set>();

        while (queue.Count > 1)
        {
            Set setA, setB;
            string operant;

            if(resultSetStack.Count > 0)
            {
                setA = resultSetStack.Pop();
            }
            else
            {
                setA = (Set)queue.Dequeue();
            }
            operant = (string)queue.Dequeue();
            setB = (Set)queue.Dequeue();

            Set result;
            if (operant == _setOperations[0])
            {
                result = setA.Intersection(setB);
            }
            else if (operant == _setOperations[1])
            {
                result = setA.Union(setB);
            }
            else
            {
                throw new InvalidOperationException($"Error: Unsupported operation: {operant}");
            }

            resultSetStack.Push(result);
        }

        return resultSetStack.Pop();
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[" + String.Join(_connectionCharacter, this.Numbers.ToArray()) + "]");
        return stringBuilder.ToString();
    }

    //public bool CompareSet(Set setToCompare)
    //{
    //    if (setToCompare.Numbers.Count != Numbers.Count) return false;

    //    for (int i = 0; i < Numbers.Count; i++)
    //    {
    //        if (setToCompare.Numbers[i] != Numbers[i]) return false;
    //    }

    //    return true;
    //}
    public bool CompareSet(Set setToCompare)
    {
        return new HashSet<int>(Numbers).SetEquals(setToCompare.Numbers);
    }

    public override bool Equals(object? obj)
    {
        Set set = obj as Set;

        if (set != null) 
        {
            return CompareSet(set);
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numbers, _connectionCharacter);
    }
}

