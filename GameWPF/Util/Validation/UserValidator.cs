using System.Text.RegularExpressions;

namespace GameWPF.Util.Validation
{
    public class UserValidator
    {
        public static bool IsPasswordValid(string psw) => Regex.IsMatch(psw, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$");
        public static bool IsLoginValid(string login) => Regex.IsMatch(login, "^[A-Za-z]\\w{4,20}$");
        public static bool IsNameValid(string name) => Regex.IsMatch(name, "^(\\p{L})+([(. )'-](\\p{L})+)*$");
    }
}
