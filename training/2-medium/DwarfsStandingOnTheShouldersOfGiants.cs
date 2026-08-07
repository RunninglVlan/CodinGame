using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/dwarfs-standing-on-the-shoulders-of-giants
static class DwarfsStandingOnTheShouldersOfGiants {
    static void Main() {
        var persons = new Persons();
        // the number of relationships of influence
        int relationships = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
        for (var i = 0; i < relationships; i++) {
            string[] relationship = Console.ReadLine()!.Split(' ');
            int x = Math.Clamp(int.Parse(relationship[0]), 1, 9999);
            int y = Math.Clamp(int.Parse(relationship[1]), 1, 9999);
            persons.AddRelationship(x, y);
        }
        Console.WriteLine(persons.CountSuccession());
    }
}

class Persons {
    readonly Dictionary<int, Person> root = new();

    /// a relationship of influence between two people (x influences y)
    public void AddRelationship(int x, int y) {
        if (!TryFind(root, x, out var xPerson)) {
            xPerson = new Person();
            root.Add(x, xPerson);
        }
        if (!TryFind(root, y, out var yPerson)) {
            yPerson = new Person();
        }
        xPerson.Influences.Add(y, yPerson);
    }

    static bool TryFind(Dictionary<int, Person> current, int id, out Person person) {
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
    public int CountSuccession() {
        return Count(root);

        int Count(Dictionary<int, Person> current, int depth = 1) => current.Values
                .Where(it => it.Influences.Count > 0)
                .Select(it => Count(it.Influences, depth + 1))
                .Prepend(depth)
                .Max();
    }
}

class Person {
    public Dictionary<int, Person> Influences { get; } = new();
}
