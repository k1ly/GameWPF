
namespace GameWPF.Model.Attribute
{
    public class ImageAttribute : System.Attribute
    {
        public string Path { get; set; }
        public ImageAttribute(string path)
        {
            Path = path;
        }
    }
}
