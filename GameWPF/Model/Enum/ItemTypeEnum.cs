using GameWPF.Model.Attribute;

namespace GameWPF.Model.Enum
{

    public enum ItemTypeEnum
    {
        [Image(@"/Resources/Image/Weapon.png")]
        Weapon = 1,

        [Image(@"/Resources/Image/Util.png")]
        Util,

        [Image(@"/Resources/Image/Head.png")]
        Head,

        [Image(@"/Resources/Image/Armor.png")]
        Armor
    }
}
