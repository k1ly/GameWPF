
namespace GameWPF.Model.Attribute
{
    public class DescriptionAttribute : System.Attribute
    {
        public string Text { get; set; }
        public DescriptionAttribute(string text)
        {
            Text = text;
        }
    }
}
