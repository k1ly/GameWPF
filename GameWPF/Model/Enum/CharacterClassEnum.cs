using GameWPF.Model.Attribute;

namespace GameWPF.Model.Enum
{

    public enum CharacterClassEnum
    {
        [Image(@"/Resources/Image/Knight.png")]
        [Description("Knight is a powerful and brave melee fighter, chosen only by forward")]
        [BaseStats(health: 100, endurance: 75, damage: 10)]
        Knight = 1,

        [Image(@"/Resources/Image/Archer.png")]
        [Description("Archer is the best option if you prefer ranged combat and average stats")]
        [BaseStats(health: 75, endurance: 100, damage: 10)]
        Archer,

        [Image(@"/Resources/Image/Witch.png")]
        [Description("Witch has the convenient magic damage, ideal if you like complications")]
        [BaseStats(health: 75, endurance: 75, damage: 15)]
        Witch
    }
}
