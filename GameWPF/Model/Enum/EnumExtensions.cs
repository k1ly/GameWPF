using System;
using System.Linq;
using GameWPF.Model.Attribute;
using System.Data.Entity;

namespace GameWPF.Model.Enum
{
    public static class EnumExtensions
    {
        public static T GetModel<T, TEnum>(this TEnum @enum, DbSet<T> dbset) where T : class where TEnum : System.Enum
            => dbset.Find((int)Convert.ChangeType(@enum, @enum.GetTypeCode()));

        public static string GetImage(this ItemTypeEnum @enum) => GetImagePath(@enum);

        public static string GetDescription(this CharacterClassEnum @enum)
        {
            DescriptionAttribute description = @enum.GetType()
                .GetField(@enum.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return description != null ? description.Text : string.Empty;
        }

        public static int GetBaseHealth(this CharacterClassEnum @enum)
        {
            BaseStatsAttribute baseStats = @enum.GetType()
                .GetField(@enum.ToString())
                .GetCustomAttributes(typeof(BaseStatsAttribute), false)
                .Cast<BaseStatsAttribute>()
                .FirstOrDefault();
            return baseStats != null ? baseStats.Health : 0;
        }

        public static int GetBaseEndurance(this CharacterClassEnum @enum)
        {
            BaseStatsAttribute baseStats = @enum.GetType()
                   .GetField(@enum.ToString())
                   .GetCustomAttributes(typeof(BaseStatsAttribute), false)
                   .Cast<BaseStatsAttribute>()
                   .FirstOrDefault();
            return baseStats != null ? baseStats.Endurance : 0;
        }

        public static int GetBaseDamage(this CharacterClassEnum @enum)
        {
            BaseStatsAttribute baseStats = @enum.GetType()
                   .GetField(@enum.ToString())
                   .GetCustomAttributes(typeof(BaseStatsAttribute), false)
                   .Cast<BaseStatsAttribute>()
                   .FirstOrDefault();
            return baseStats != null ? baseStats.Damage : 0;
        }

        public static string GetImage(this CharacterClassEnum @enum) => GetImagePath(@enum);

        private static string GetImagePath<TEnum>(TEnum @enum) where TEnum : System.Enum
        {
            ImageAttribute image = @enum.GetType()
                .GetField(@enum.ToString())
                .GetCustomAttributes(typeof(ImageAttribute), false)
                .Cast<ImageAttribute>()
                .FirstOrDefault();
            return image != null ? image.Path : string.Empty;
        }
    }
}
