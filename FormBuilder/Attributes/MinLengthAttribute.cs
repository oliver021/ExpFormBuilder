using MudFormBuilder.Attributes;

public class MinLengthAttribute : FormAttribute
{
    public int MinLength { get; set; }

    public MinLengthAttribute(int minLength)
    {
        MinLength = minLength;
    }
}
