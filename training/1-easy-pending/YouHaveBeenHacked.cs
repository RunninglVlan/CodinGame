using System;
using System.Collections.Generic;

// https://www.codingame.com/contribute/view/78658089fc5f6fedf11c055682408582f2eb
int companyCount = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 10);
Dictionary<string, Company> companies = [];
for (var _ = 0; _ < companyCount; _++) {
    string[] input = Console.ReadLine()!.Split(':');
    string name = input[0];
    int protectionLevel = Math.Clamp(int.Parse(input[1]), 1, 10);
    companies[name] = new Company(name, protectionLevel);
}
int attacks = Math.Clamp(int.Parse(Console.ReadLine()!), 1, 100);
for (var _ = 0; _ < attacks; _++) {
    string[] input = Console.ReadLine()!.Split(':');
    var company = companies[input[0]];
    string stage = input[1];
    int strength = Math.Clamp(int.Parse(input[2]), 1, 20_000);
    company.Attack(stage, strength);
}
foreach (var company in companies.Values) {
    Console.WriteLine(company);
}

record Company(string Name, int ProtectionLevel) {
    string? AttackStatus { get; set; }

    public void Attack(string stage, int strength) {
        if (AttackStatus == AttackStatuses.BLOCKED) {
            return;
        }
        AttackStatus = stage switch {
            AttackStage.PORT_SCANNING => ProtectionLevel >= 7 && strength < 10_000
                    ? AttackStatuses.BLOCKED
                    : AttackStatuses.HACKED,
            AttackStage.BRUTE_FORCE => ProtectionLevel >= 7
                    || ProtectionLevel is 5 or 6 && strength < 5_000
                    || strength < 500
                            ? AttackStatuses.BLOCKED
                            : AttackStatuses.HACKED,
            AttackStage.MALWARE_INSTALLATION => ProtectionLevel >= 7
                    || ProtectionLevel is 5 or 6 && strength < 3_000
                    || strength < 1_000
                            ? AttackStatuses.BLOCKED
                            : AttackStatuses.HACKED,
            _ => AttackStatus
        };
    }

    public override string ToString() => $"{Name}:{AttackStatus}";
}

static class AttackStage {
    public const string PORT_SCANNING = "ps";
    public const string BRUTE_FORCE = "bf";
    public const string MALWARE_INSTALLATION = "mi";
}

static class AttackStatuses {
    public const string BLOCKED = "Blocked";
    public const string HACKED = "Hacked";
}
