using System;
using System.Collections.Generic;
using System.Linq;

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
Dictionary<string, AttackStage[]> attacks = [];
for (var _ = 0; _ < attackCount; _++) {
    string[] input = Console.ReadLine()!.Split(':');
    string company = input[0];
    string stageName = input[1];
    int strength = Math.Clamp(int.Parse(input[2]), 1, 20_000);
    if (!attacks.TryGetValue(company, out var stages)) {
        attacks[company] = stages = AttackStage.All;
    }
    stages.First(it => it.Name == stageName).Strength += strength;
}
foreach (var company in companies) {
    foreach (var attackStage in attacks[company.Name]) {
        company.Attack(attackStage);
    }
    Console.WriteLine(company);
}

record Company(string Name, int ProtectionLevel) {
    string? AttackStatus { get; set; }

    public void Attack(AttackStage stage) {
        if (AttackStatus == AttackStatuses.BLOCKED) {
            return;
        }
        AttackStatus = stage.Name switch {
            AttackStage.PORT_SCANNING => ProtectionLevel >= 7 && stage.Strength < 10_000
                    ? AttackStatuses.BLOCKED
                    : AttackStatuses.HACKED,
            AttackStage.BRUTE_FORCE => ProtectionLevel >= 7
                    || ProtectionLevel is 5 or 6 && stage.Strength < 5_000
                    || stage.Strength < 500
                            ? AttackStatuses.BLOCKED
                            : AttackStatuses.HACKED,
            AttackStage.MALWARE_INSTALLATION => ProtectionLevel >= 7
                    || ProtectionLevel is 5 or 6 && stage.Strength < 3_000
                    || stage.Strength < 1_000
                            ? AttackStatuses.BLOCKED
                            : AttackStatuses.HACKED,
            _ => AttackStatus
        };
    }

    public override string ToString() => $"{Name}:{AttackStatus}";
}

record AttackStage(string Name) {
    public const string PORT_SCANNING = "ps";
    public const string BRUTE_FORCE = "bf";
    public const string MALWARE_INSTALLATION = "mi";

    public static AttackStage[] All => [new(PORT_SCANNING), new(BRUTE_FORCE), new(MALWARE_INSTALLATION)];

    public int Strength { get; set; }
}

static class AttackStatuses {
    public const string BLOCKED = "Blocked";
    public const string HACKED = "Hacked";
}
