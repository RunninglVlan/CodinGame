using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

/// https://www.codingame.com/training/easy/isbn-check-digit
static partial class ISBNCheckDigit {
    [GeneratedRegex(@"^(\d{9})(\d|X)$")]
    private static partial Regex ISBN10();

    [GeneratedRegex(@"^(\d{13})$")]
    private static partial Regex ISBN13();

    static void Main() {
        int n = int.Parse(Console.ReadLine()!);
        var invalidNumbers = new List<string>();
        for (var _ = 0; _ < n; _++) {
            string isbn = Console.ReadLine()!;
            if (!IsValid(isbn)) {
                invalidNumbers.Add(isbn);
            }
        }
        Console.WriteLine($"{invalidNumbers.Count} invalid:");
        foreach (string number in invalidNumbers) {
            Console.WriteLine(number);
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    static bool IsValid(string isbn) {
        var sum = 0;
        if (ISBN10().IsMatch(isbn)) {
            var weight = 10;
            for (var index = 0; index < 9; index++) {
                sum += (int)char.GetNumericValue(isbn[index]) * weight--;
            }
            return IsValidCheckDigit(11);
        }
        if (ISBN13().IsMatch(isbn)) {
            for (var index = 0; index < 12; index++) {
                int weight = index % 2 == 0 ? 1 : 3;
                sum += (int)char.GetNumericValue(isbn[index]) * weight;
            }
            return IsValidCheckDigit(10);
        }
        return false;

        bool IsValidCheckDigit(int division) {
            int remainder = sum % division;
            int checkNumber = remainder == 0 ? 0 : division - remainder;
            string checkDigit = checkNumber == 10 ? "X" : checkNumber.ToString();
            return isbn.Last().ToString() == checkDigit;
        }
    }
}
