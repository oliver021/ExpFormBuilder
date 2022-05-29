using System;
using System.Collections.Generic;

namespace MudFormBuilder
{
    public class SubmissionResult
    {
        public SubmissionResult(Dictionary<string, object> values)
        {
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public SubmissionResult()
        {
        }

        public SubmissionResult(Dictionary<string, object> values, bool isSuccess) : this(values)
        {
            IsSuccess = isSuccess;
        }

        internal Dictionary<string, object> Values { get; set; }

        public bool IsSuccess { get; internal set; }

        public object this[string key] => Values[key];

        public bool ContainsKey(string key)
        {
            return Values.ContainsKey(key);
        }

        public IEnumerable<string> Keys => Values.Keys;

        public string GetString(string key, string defaultValue = "")
        {
            return Values[key].ToString() ?? defaultValue;
        }

        public int GetInt(string key)
        {
            return Convert.ToInt32(Values[key]);
        }

        public bool GetBool(string key)
        {
            return Convert.ToBoolean(Values[key]);
        }

        public double GetDouble(string key)
        {
            return Convert.ToDouble(Values[key]);
        }

        public DateTime GetDateTime(string key)
        {
            return Convert.ToDateTime(Values[key]);
        }

        public IEnumerable<string> GetStringList(string key)
        {
            return (IEnumerable<string>)Values[key];
        }

        public TimeSpan GetTimeSpan(string key)
        {
            return (TimeSpan)Values[key];
        }

        public object GetObject(string key)
        {
            return Values[key];
        }

        public object GetObject(string key, Type type)
        {
            return Convert.ChangeType(Values[key], type);
        }

        public T From<T>()
        {
            // get type instance
            var instance = Activator.CreateInstance<T>();
            // type instance
            var type = typeof(T);
            // by properties and fields find in values dictionary
            // and fill instance with values

            // get properties
            foreach (var property in type.GetProperties())
            {
                if (Values.ContainsKey(property.Name))
                {
                    var currentValue = Values[property.Name].GetType() == property.PropertyType ? Values[property.Name] : Convert.ChangeType(Values[property.Name], property.PropertyType);
                    property.SetValue(instance, currentValue);
                }
            }

            // this loop is for fields
            foreach (var field in type.GetFields())
            {
                if (Values.ContainsKey(field.Name))
                {
                    var currentValue = Values[field.Name].GetType() == field.FieldType ? Values[field.Name] : Convert.ChangeType(Values[field.Name], field.FieldType);
                    field.SetValue(instance, currentValue);
                }
            }

            return instance;
        }

    }

    public class SubmissionResult<T> : SubmissionResult
    {
        public SubmissionResult()
        {
        }

        public SubmissionResult(Dictionary<string, object> values) : base(values)
        {
        }

        public SubmissionResult(Dictionary<string, object> values, bool isSuccess) : base(values, isSuccess)
        {
        }

        public SubmissionResult(SubmissionResult result)
        {
            Values = result.Values;
            IsSuccess = result.IsSuccess;
        }

        public T Result => base.From<T>();
    }
}
