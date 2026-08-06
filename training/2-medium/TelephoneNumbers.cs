using System;
using System.Collections.Generic;
using System.Linq;

/// https://www.codingame.com/training/medium/telephone-numbers
static class TelephoneNumbers {
    static void Main() {
        int phoneCount = int.Parse(Console.ReadLine()!);
        var phones = new List<string>(phoneCount);
        for (var i = 0; i < phoneCount; i++) {
            string phoneNumber = Console.ReadLine()!;
            phones.Add(phoneNumber);
        }

        List<string> uniquePhones = [phones.First()];
        for (var i = 1; i < phones.Count; i++) {
            string phone = phones[i];
            if (!uniquePhones.Any(it => it.StartsWith(phone))) {
                uniquePhones.Add(phone);
            }
        }
        // the number of elements (referencing a number) stored in the structure
        Console.WriteLine(uniquePhones.Select(it => it.Length).Sum());
    }
}
