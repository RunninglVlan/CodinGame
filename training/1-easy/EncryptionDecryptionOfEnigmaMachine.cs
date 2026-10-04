using System;

/// https://www.codingame.com/training/easy/encryptiondecryption-of-enigma-machine
static class EncryptionDecryptionOfEnigmaMachine {
    const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    static readonly string[] rotors = new string[3];

    static void Main() {
        var operation = Enum.Parse<Operation>(Console.ReadLine()!, ignoreCase: true);
        int pseudoRandomNumber = int.Parse(Console.ReadLine()!);
        for (var i = 0; i < 3; i++) {
            rotors[i] = Console.ReadLine()!;
        }
        string message = Console.ReadLine()!;
        char[] newMessage = message.ToCharArray();
        if (operation == Operation.Encode) {
            CaesarShift(newMessage, pseudoRandomNumber, operation);
            for (var i = 0; i < 3; i++) {
                Substitute(newMessage, ALPHABET, rotors[i]);
            }
        } else {
            for (var i = 2; i >= 0; i--) {
                Substitute(newMessage, rotors[i], ALPHABET);
            }
            CaesarShift(newMessage, pseudoRandomNumber, operation);
        }
        Console.WriteLine(newMessage);
    }

    static void CaesarShift(char[] value, int incrementedNumber, Operation operation) {
        for (var i = 0; i < value.Length; i++) {
            char letter = value[i];
            int direction = operation == Operation.Encode ? 1 : -1;
            int newLetterIndex = ALPHABET.IndexOf(letter) + direction * incrementedNumber++;
            while (newLetterIndex < 0) {
                newLetterIndex += ALPHABET.Length;
            }
            if (newLetterIndex > ALPHABET.Length - 1) {
                newLetterIndex %= ALPHABET.Length;
            }
            value[i] = ALPHABET[newLetterIndex];
        }
    }

    static void Substitute(char[] value, string from, string to) {
        for (var i = 0; i < value.Length; i++) {
            char letter = value[i];
            int newLetterIndex = from.IndexOf(letter);
            value[i] = to[newLetterIndex];
        }
    }
}

enum Operation {
    Encode,
    Decode
}
