using MudFormBuilder.Attributes;
using System.Reflection;

namespace MudFormBuilder
{
    public class FormAttributes
    {
        public List<FormAttributeContainer> Attributes { get; set; } = new();

        public FormAttributes()
        {
            Attributes = new List<FormAttributeContainer>();
        }

        /// <summary>
        /// Add a form attribute from member.
        /// </summary>
        /// <param name="member">The member to add the attribute from.</param>
        public void Register(MemberInfo member)
        {
            var attrs = member.GetCustomAttributes<FormAttribute>();

            foreach (var item in attrs)
            {
                Attributes.Add(new FormAttributeContainer(item.GetType().Name, item));
            }
        }

        /// <summary>
        /// Check if the attribute is present.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>True if present, false otherwise.</returns>
        public bool HasAttribute(string name)
        {
            return Attributes.Exists(x => x.Name == name);
        }
    }
}