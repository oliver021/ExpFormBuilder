namespace MudFormBuilder.Attributes
{
    public class MaxLengthAttribute : FormAttribute
    {
        public int MaxLength
        {
            get; set;
        }
        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}
