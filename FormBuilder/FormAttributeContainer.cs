using MudFormBuilder.Attributes;

namespace MudFormBuilder
{
    public class FormAttributeContainer
    {
        public string Name { get; set; }

        // key-value pair
        public FormAttribute Attr { get; set; }

        public FormAttributeContainer(string name, FormAttribute attr)
        {
            Name = name;
            Attr = attr;
        }
    }
}