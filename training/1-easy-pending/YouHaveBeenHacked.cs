using System;

// https://www.codingame.com/contribute/view/78658089fc5f6fedf11c055682408582f2eb
int companyCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
var companies = new Company[companyCount];
for (var i = 0; i < companyCount; i++) {
    string[] input = Console.ReadLine()!.Split(':');
    string name = input[0];
    int protectionLevel = Math.Clamp(int.Parse(input[1]), 1, 10);
    companies[i] = new Company(name, protectionLevel);
}
int attackCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
var attacks = new Attack[attackCount];
for (var i = 0; i < attackCount; i++) {
    string[] input = Console.ReadLine()!.Split(':');
    string company = input[0];
    string stage = input[1];
    int strength = Math.Clamp(int.Parse(input[2]), 1, 20_000);
    attacks[i] = new Attack(company, stage, strength);
}
foreach (var company in companies) {
    Console.WriteLine(company);
}

record struct Company(string Name, int ProtectionLevel) {
    public string Status { get; set; } = CompanyStatus.BLOCKED;

    public override string ToString() => $"{Name}:{Status}";
}

record struct Attack(string Company, string Stage, int Strength);

static class CompanyStatus {
    public const string BLOCKED = "Blocked";
    public const string HACKED = "Hacked";
}
