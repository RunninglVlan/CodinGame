using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/dwarfs-standing-on-the-shoulders-of-giants
static class DwarfsStandingOnTheShouldersOfGiants {
    static void Main() {
        var root = new Dictionary<int, Person>();
        // the number of relationships of influence
        int relationships = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
        for (var i = 0; i < relationships; i++) {
            string[] relationship = Console.ReadLine()!.Split(' ');
            // a relationship of influence between two people (x influences y)
            int x = Math.Clamp(int.Parse(relationship[0]), 1, 9999);
            int y = Math.Clamp(int.Parse(relationship[1]), 1, 9999);
            if (!Person.TryFind(root, x, out var xPerson)) {
                xPerson = new Person();
                root.Add(x, xPerson);
            }
            if (Person.TryFind(root, y, out var yPerson)) {
                var yParent = yPerson.Parent;
                yParent?.Influences.Remove(y);
            } else {
                yPerson = new Person();
            }
            xPerson.Influences.Add(y, yPerson);
            yPerson.Parent = xPerson;
        }
        Console.WriteLine(Person.CountSuccession(root));
    }
}

class Person {
    public Dictionary<int, Person> Influences { get; } = new();
    public Person? Parent { get; set; }

    public static bool TryFind(Dictionary<int, Person> current, int id, out Person person) {
        if (current.TryGetValue(id, out person!)) {
            return true;
        }
        foreach (var next in current.Values) {
            if (TryFind(next.Influences, id, out person)) {
                return true;
            }
        }
        person = null!;
        return false;
    }

    /// the number of people involved in the longest succession of influences
    public static int CountSuccession(Dictionary<int, Person> children, int depth = 1) {
        return children.Values
                .Where(it => it.Influences.Count > 0)
                .Select(it => CountSuccession(it.Influences, depth + 1))
                .Prepend(depth)
                .Max();
    }
}
