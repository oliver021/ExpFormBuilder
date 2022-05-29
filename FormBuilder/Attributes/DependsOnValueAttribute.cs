namespace MudFormBuilder.Attributes
{
    public class DependsOnValueAttribute : FormAttribute
    {
        public string Field { get; set; }
        public string[] Values { get; set; }

        public DependsOnValueAttribute(string field, string[] values)
        {
            Field = field;
            Values = values;
        }
    }
}