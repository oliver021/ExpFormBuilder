namespace MudFormBuilder.Attributes
{
    public class DependsOnAttribute : FormAttribute
    {
        public string[] DependsOn
        {
            get; set;
        }
        public DependsOnAttribute(params string[] dependsOn)
        {
            DependsOn = dependsOn;
        }
    }
}
