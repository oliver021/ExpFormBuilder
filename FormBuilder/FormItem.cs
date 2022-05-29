namespace MudFormBuilder
{
    public class FormItem
    {

        public FormItem(string name)
        {
            Name = name;
        }

        public FormItem(string name, FormItemType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }

        public FormItemType Type { get; set; }

        public object DefaultValue { get; set; } = "";

        public string Label { get; set; } = "";

        public string RequiredError { get; set; } = "";

        public string Placeholder { get; set; } = "";

        public string Description { get; set; } = "";

        public string Help { get; set; } = "";

#nullable enable
        public Dictionary<string, string>? SelectItems { get; set; } = null;
#nullable disable
        
        public bool IsRequired { get; set; } = false;

        public bool IsMultiple { get; set; } = false;

        public int ColumnSize { get; set; } = -1; // -1 means not set, from 1 to 12
    }
}