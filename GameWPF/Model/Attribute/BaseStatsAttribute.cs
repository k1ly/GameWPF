
namespace GameWPF.Model.Attribute
{
    public class BaseStatsAttribute : System.Attribute
    {
        public int Health { get; set; }
        public int Endurance { get; set; }
        public int Damage { get; set; }
        public BaseStatsAttribute(int health = 0, int endurance = 0, int damage = 0)
        {
            Health = health;
            Endurance = endurance;
            Damage = damage;
        }
    }
}
