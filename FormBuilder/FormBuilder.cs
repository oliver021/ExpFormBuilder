using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace MudFormBuilder
{
    /// <summary>
    /// Help to build more fluency the Form.
    /// Add items through the fluent interface using FormFieldItem.
    /// </summary>
    public class FormBuilder : IFormBuilder
    {
        /// <summary>
        /// the property that contains the instance of the FormItems.
        /// </summary>
        private List<FormItem> _formItems = new();
        
        /// <summary>
        /// the property that contain form name
        /// </summary>
        private string Name { get; set; } = "";

        /// <summary>
        /// constructor parameterless.
        /// </summary>
        public FormBuilder()
        {
        }

        /// <summary>
        /// constructor.
        /// </summary>
        public FormBuilder(string name)
        {
            Name = name;
        }

        /// <summary>
        /// add a Form Field Item.
        /// </summary>
        /// <param name="formItem">the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(FormItem formItem)
        {
            _formItems.Add(formItem);
            return this;
        }

        /// <summary>
        /// add a Form Field Item from "name" and "value".
        /// </summary>
        /// <param name="name">the name of the Form Field Item.</param>
        /// <param name="type">the type of the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(string name, FormItemType type)
        {
            _formItems.Add(new FormItem(name, type));
            return this;
        }

        /// <summary>
        /// Add a Form Field Item from "name" and "value".
        /// </summary>
        /// <param name="name">the name of the Form Field Item.</param>
        /// <param name="type">the type of the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder AddRequired(string name, FormItemType type, string msgError = "")
        {
            FormItem added = new(name, type);
            added.IsRequired = true;
            added.RequiredError = msgError != "" ? msgError : "Required field: " + name;
            _formItems.Add(added);
            return this;
        }

        /// <summary>
        /// Add a Form Field Item from "name" and "value".
        /// </summary>
        /// <param name="name">the name of the Form Field Item.</param>
        /// <param name="type">the type of the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder AddSelect(string name,
                                     Dictionary<string, string> options,
                                     bool multiple = false,
                                     string defaultValue = null)
        {
            // TODO : implement
            FormItem added = new(name, FormItemType.Select);
            _formItems.Add(added);
            return this;
        }

        /// <summary>
        /// add a Form Field Item from "name" and "value", "default", "label", "requiredError", "placeholder"
        /// </summary>
        /// <param name="name">the name of the Form Field Item.</param>
        /// <param name="type">the type of the Form Field Item.</param>
        /// <param name="defaultValue">the default value of the Form Field Item.</param>
        /// <param name="label">the label of the Form Field Item.</param>
        /// <param name="requiredError">the required error of the Form Field Item.</param>
        /// <param name="placeholder">the placeholder of the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(string name, FormItemType type, object defaultValue, string label, string requiredError, string placeholder)
        {
            var item = new FormItem(name, type);
            item.DefaultValue = defaultValue;
            item.Label = label;
            item.RequiredError = requiredError;
            item.Placeholder = placeholder;
            _formItems.Add(item);
            return this;
        }

         /// <summary>
        /// add a Form Field Item using a member like property/field and callback to set the Form Field Item.
        /// </summary>
        /// <param name="member">the property/field of the Form Field Item.</param>
        /// <param name="callback">the callback to set the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(string name, FormItemType type, Action<FormFieldBuilder> callback)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }


            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var formFieldBuilder = new FormFieldBuilder(name);
            formFieldBuilder.SetType(type);
            // previous set the type
            callback(formFieldBuilder);
            _formItems.Add(formFieldBuilder.Build());
            return this;
        }

        /// <summary>
        /// add a Form Field Item using callback to set the Form Field Item.
        /// </summary>
        /// <param name="callback">the callback to set the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(Action<FormFieldBuilder> callback)
        {
            var formFieldBuilder = new FormFieldBuilder(Name);
            callback(formFieldBuilder);
            _formItems.Add(formFieldBuilder.Build());
            return this;
        }

        /// <summary>
        /// add a Form Field Item using a meber like property/field and callback to set the Form Field Item.
        /// </summary>
        /// <param name="member">the property/field of the Form Field Item.</param>
        /// <param name="callback">the callback to set the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(MemberInfo member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            var formFieldBuilder = new FormFieldBuilder(member.Name);
            return __Add(member, formFieldBuilder);
        }


        /// <summary>
        /// add a Form Field Item using a member like property/field and callback to set the Form Field Item.
        /// </summary>
        /// <param name="member">the property/field of the Form Field Item.</param>
        /// <param name="callback">the callback to set the Form Field Item.</param>
        /// <returns>the instance of the Form Builder.</returns>
        public FormBuilder Add(MemberInfo member, Action<FormFieldBuilder> callback)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var formFieldBuilder = new FormFieldBuilder(member.Name);
            _ = __Add(member, formFieldBuilder);
            callback(formFieldBuilder);
            return this;
        }

        /// <summary>
        /// add a Form Field Item using a member like property/field and callback to set the Form Field Item.
        /// recive a argument type and a callback to know what member to set.
        /// </summary>
        public FormBuilder Add<T, K>(Expression<Func<T, K>> getMember, Action<FormFieldBuilder> callback = null)
        
        {
            // get expression of getMember
            // using System.Linq.Expressions
            var expression = (MemberExpression)getMember.Body;
            // get the member
            var member = expression.Member;

            var formFieldBuilder = new FormFieldBuilder(member.Name);

            __Add(member, formFieldBuilder);

            // if callback is not null
            if (callback != null)
            {
                callback.Invoke(formFieldBuilder);
            }

            return this;
        }

        private FormBuilder __Add(MemberInfo member, FormFieldBuilder formFieldBuilder)
        {

            // check if the member is a property/field
            if (member is PropertyInfo prop)
            {
                // previous set the type
                formFieldBuilder.SetType(prop.PropertyType, BuilderHelper.FindAttributes(member));

                _formItems.Add(formFieldBuilder.Build());
                return this;
            }
            else if (member is FieldInfo field)
            {
                // previous set the type
                formFieldBuilder.SetType(field.FieldType, BuilderHelper.FindAttributes(member));

                _formItems.Add(formFieldBuilder.Build());
                return this;
            }
            else
            {
                throw new ArgumentException("The member must be a property/field.");
            }
        }

        /// <summary>
        /// get the instance of the FormMapped.
        /// </summary>
        /// <returns>the instance of the Form.</returns>
        public FormMapped Build()
        {
            return new FormMapped(Name, _formItems);
        }

        /// <summary>
        /// Implement <see cref="IFormBuilder"/>
        /// </summary>
        /// <returns></returns>
        public FormMapped GetForm()
        {
            return Build();
        }
    }

    /// <summary>
    /// Help to build more fluency the Form.
    /// Add items through the fluent interface using FormFieldItem.
    /// </summary>
    public class FormBuilder<T> : FormBuilder
    {
        /// <summary>
        /// constructor parameterless.
        /// </summary>
        public FormBuilder()
        {
        }

        /// <summary>
        /// Register a new field for object member.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="getMember"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public FormBuilder<T> For<K>(Expression<Func<T, K>> getMember, Action<FormFieldBuilder> builder)
        {
            Add(getMember, builder);
            return this;
        }

        /// <summary>
        /// All publics property or field will be set as form field.
        /// </summary>
        /// <returns></returns>
        public FormBuilder<T> AllPublics()
        {
            Type target = typeof(T);
            foreach (var item in target.GetProperties().Where(m => m.CanRead && m.CanWrite))
            {
                Add(item);
            }

            foreach (var item in target.GetFields().Where(m => m.IsPublic))
            {
                Add(item);
            }

            return this;
        }
    }
}
