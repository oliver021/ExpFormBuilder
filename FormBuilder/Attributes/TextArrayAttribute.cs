namespace MudFormBuilder.Attributes
{
    public class TextArrayAttribute : FormAttribute
    {
        public char Separator { get; set; }

        public TextArrayAttribute(char separator = ',')
        {
            Separator = separator;
        }
    }
}