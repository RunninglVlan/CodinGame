using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/dwarfs-standing-on-the-shoulders-of-giants
static class DwarfsStandingOnTheShouldersOfGiants {
    static void Main() {
        Person? rootPerson = null;
        // the number of relationships of influence
        int relationships = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 9999);
        for (var i = 0; i < relationships; i++) {
            string[] relationship = Console.ReadLine()!.Split(' ');
            // a relationship of influence between two people (x influences y)
            int x = Math.Clamp(int.Parse(relationship[0]), 1, 9999);
            int y = Math.Clamp(int.Parse(relationship[1]), 1, 9999);
            rootPerson ??= new Person(x, []);
            if (rootPerson.TryFind(x, out var xPerson)) {
                xPerson.Influences.Add(new Person(y, []));
            } else if (rootPerson.TryFind(y, out var yPerson)) {
                rootPerson = new Person(x, [yPerson]);
            }
        }
        Console.WriteLine(rootPerson!.CountSuccession());
    }
}

record Person(int Id, HashSet<Person> Influences) {
    public bool TryFind(int id, out Person person) {
        if (Id == id) {
            person = this;
            return true;
        }
        foreach (var influence in Influences) {
            if (influence.TryFind(id, out person)) {
                return true;
            }
        }
        person = null!;
        return false;
    }

    /// the number of people involved in the longest succession of influences
    public int CountSuccession(int depth = 1) {
        return Influences.Select(it => it.CountSuccession(depth + 1)).Prepend(depth).Max();
    }
}
