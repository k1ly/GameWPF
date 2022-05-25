using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Globalization;

namespace GameWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string LangPath = @"Resources\Localization\Lang";

        private ICollection<CultureInfo> languages;
        public ICollection<CultureInfo> Languages
        {
            get => languages;
        }

        public CultureInfo Language
        {
            get => GameWPF.Properties.Resources.Culture;
            set
            {
                if (value == null || value == Language) return;
                GameWPF.Properties.Resources.Culture = value;
                ResourceDictionary dictionary = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU": dictionary.Source = new Uri($@"{LangPath}.{value.Name}.xaml", UriKind.Relative); break;
                    default: dictionary.Source = new Uri($@"{LangPath}.xaml", UriKind.Relative); break;
                }
                ResourceDictionary oldDictionary = Current.Resources.MergedDictionaries.Where(d => d.Source.OriginalString.StartsWith(LangPath)).FirstOrDefault();
                if (oldDictionary != null)
                    Current.Resources.MergedDictionaries.Remove(oldDictionary);
                Current.Resources.MergedDictionaries.Add(dictionary);
                GameWPF.Properties.Settings.Default.DefaultLanguage = value;
                GameWPF.Properties.Settings.Default.Save();
            }
        }

        public App()
        {
            languages = new List<CultureInfo>();
            Languages.Add(CultureInfo.GetCultureInfo("en-US"));
            Languages.Add(CultureInfo.GetCultureInfo("ru-RU"));
            Language = GameWPF.Properties.Settings.Default.DefaultLanguage;
        }
    }
}
