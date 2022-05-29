namespace MudFormBuilder.Attributes
{
    public class RegexRuleAttribute : FormAttribute
    {
        public string Regex
        {
            get; set;
        }
        public RegexRuleAttribute(string regex)
        {
            Regex = regex;
        }
    }
}
