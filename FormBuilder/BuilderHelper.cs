using MudFormBuilder.Attributes;
using System.Reflection;

namespace MudFormBuilder
{
    public static class BuilderHelper
    {
        /// <summary>
        /// map the attributes of the Form Field.
        /// </summary>
        public static FormAttributes FindAttributes(MemberInfo member)
        {
            var attributes = new FormAttributes();
            attributes.Register(member);
            return attributes;
        }

        /// <summary>
        /// Map from System.Type to FormItemType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FormItemType MapType(Type type)
        {
            // for correspoding type return form item type
            // using switch case
            // if not found return custom
            // the float, double, integer, decimal, signed and unsigned types are return the same: number

            switch (type.Name)
            {
                case "String":
                    return FormItemType.Text;
                case "Int16":
                case "UInt16":
                case "Int32":
                case "UInt32":
                case "Int64":
                case "UInt64":
                case "Decimal":
                case "Double":
                case "Single":
                    return FormItemType.Number;
                case "DateTime":
                    return FormItemType.DateTime;
                case "TimeSpan":
                    return FormItemType.Time;
                case "Boolean":
                    return FormItemType.CheckBox;
                case "Byte":
                    return FormItemType.Number;
                case "SByte":
                    return FormItemType.Number;
                case "Char":
                    return FormItemType.Text;
                case "Guid":
                    return FormItemType.Text;
                default:
                    return FormItemType.NonSpecific;

            }
        }

        /// <summary>
        /// Map from System.Type to FormItemType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FormItemType MapType(Type type, FormAttributes attrtibutes)
        {
            // for correspoding type return form item type
            // using switch case
            // if not found return custom
            // the float, double, integer, decimal, signed and unsigned types are return the same: number
            Console.WriteLine(type.Name);
            switch (type.Name)
            {
                case "String":
                    if (attrtibutes.HasAttribute(nameof(TextAreaAttribute)))
                    {
                        return FormItemType.TextArea;
                    }
                    else if (attrtibutes.HasAttribute(nameof(PasswordAttribute)))
                    {
                        return FormItemType.Password;
                    }
                    else
                    {
                        return FormItemType.Text;
                    }
                case "Int16":
                case "UInt16":
                case "Int32":
                case "UInt32":
                case "Int64":
                case "UInt64":
                case "Decimal":
                case "Double":
                case "Single":
                    return FormItemType.Number;
                case "DateTime":
                    if (attrtibutes.HasAttribute(nameof(OnlyDateAttribute)))
                    {
                        return FormItemType.Date;
                    }
                    else
                    {
                        return FormItemType.DateTime;
                    }
                case "TimeSpan":
                    return FormItemType.Time;
                case "Boolean":
                    if (attrtibutes.HasAttribute(nameof(SwitchBooleanAttribute)))
                    {
                        return FormItemType.Switch;
                    }
                    else
                    {
                        return FormItemType.CheckBox;
                    }
                case "Byte":
                    return FormItemType.Number;
                case "Char":
                    return FormItemType.Text;
                case "Guid":
                    return FormItemType.Text;
                default:
                    // check if type is a enum
                    if (type.IsEnum)
                    {
                        return FormItemType.Select;
                    }

                    return FormItemType.NonSpecific;

            }
        }

        /// <summary>
        /// Get the FormItemType from the string
        /// </summary>
        public static FormItemType GetFormItemType(string type)
        {
            switch (type)
            {
                case "text":
                    return FormItemType.Text;
                case "textarea":
                    return FormItemType.TextArea;
                case "dropdown":
                    return FormItemType.DropDown;
                case "checkbox":
                    return FormItemType.CheckBox;
                case "radiobutton":
                    return FormItemType.RadioButton;
                case "date":
                    return FormItemType.Date;
                case "datetime":
                    return FormItemType.DateTime;
                case "time":
                    return FormItemType.Time;
                case "number":
                    return FormItemType.Number;
                case "email":
                    return FormItemType.Email;
                case "password":
                    return FormItemType.Password;
                case "file":
                    return FormItemType.File;
                case "hidden":
                    return FormItemType.Hidden;
                case "image":
                    return FormItemType.Image;

                case "custom":
                    return FormItemType.NonSpecific;
                default:
                    return FormItemType.Text;
            }
        }
    }
}