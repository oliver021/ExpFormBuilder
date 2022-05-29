using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudFormBuilder
{
    /// <summary>
    /// Help to build more fluency the Form Field.
    public class FormFieldBuilder
    {
        /// <summary>
        /// the property that contains the instance of the Form Field Item.
        /// </summary>
        private FormItem _formItem;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="name">the name of the Form Field.</param>
        public FormFieldBuilder(string name)
        {
            _formItem = new FormItem(name);
        }

        /// <summary>
        /// set form item type.
        /// </summary>
        /// <param name="type">the type of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetType(FormItemType type)
        {
            _formItem.Type = type;
            return this;
        }

        /// <summary>
        /// set form item type.
        /// </summary>
        /// <param name="type">the type of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetType(Type type)
        {
            _formItem.Type = BuilderHelper.MapType(type);
            return this;
        }

        /// <summary>
        /// set form item type.
        /// </summary>
        /// <param name="type">the type of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetType(Type type, FormAttributes attributes)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            _formItem.Type = BuilderHelper.MapType(type, attributes);
            return this;
        }

        /// <summary>
        /// set form item label.
        /// </summary>
        /// <param name="label">the label of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetLabel(string label)
        {
            _formItem.Label = label;
            return this;
        }

        /// <summary>
        /// set form item value.
        /// </summary>
        /// <param name="value">the value of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetDefaultValue(object value)
        {
            _formItem.DefaultValue = value;
            return this;
        }

        /// <summary>
        /// set required flag.
        /// </summary>
        public FormFieldBuilder Required()
        {
            _formItem.IsRequired = true;
            return this;
        }

        /// <summary>
        /// set select items.
        /// </summary>
        /// <param name="items">the items of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetSelectItems(Dictionary<string, string> items, bool isMultiple = false)
        {
            _formItem.SelectItems = items;
            _formItem.IsMultiple = isMultiple;
            return this;
        }

        /// <summary>
        /// set column size.
        /// </summary>
        /// <param name="size">the column size of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetColumnSize(int size)
        {
            // check if the size is valid
            if (size < 1 || size > 12)
            {
                throw new ArgumentException("The column size must be between 1 and 12.");
            }
            _formItem.ColumnSize = size;
            return this;
        }

        /// <summary>
        /// set placeholder.
        /// </summary>
        /// <param name="placeholder">the placeholder of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetPlaceholder(string placeholder)
        {
            _formItem.Placeholder = placeholder;
            return this;
        }

        /// <summary>
        /// set description.
        /// </summary>
        /// <param name="description">the description of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetDescription(string description)
        {
            _formItem.Description = description;
            return this;
        }

        /// <summary>
        /// set help.
        /// </summary>
        /// <param name="help">the help of the Form Field.</param>
        /// <returns>the instance of the Form Field Builder.</returns>
        public FormFieldBuilder SetHelp(string help)
        {
            _formItem.Help = help;
            return this;
        }

        /// <summary>
        /// get the instance of the Form Field Item.
        /// </summary>
        /// <returns>the instance of the Form Field Item.</returns>
        public FormItem Build()
        {
            return _formItem;
        }
    }
}
