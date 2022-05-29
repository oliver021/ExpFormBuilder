using System.Collections.Generic;
using System;
using System.Collections;

namespace MudFormBuilder
{
    /// <summary>
    /// FormMapped Contains basic information to build a form.
    /// </summary>
    public class FormMapped : IEnumerable<FormItem>
    {
        /// <summary>
        /// The name of the form.
        /// </summary>
        public string FormName { get; set; } = "";

        /// <summary>
        /// The items of the form.
        /// </summary>
        public List<FormItem> FormItems { get; set; }

        /// <summary>
        /// the delegate to call when the form is submitted.
        /// </summary>
        public Action<FormMapped> OnSubmit { get; set; } = null;

        /// <summary>
        /// The form mapped.
        /// </summary>
        public FormMapped()
        {
            FormItems = new List<FormItem>();
        }

        /// <summary>
        /// The form mapped.
        /// </summary>
        /// <param name="formName">The name of the form.</param>
        /// <param name="formItems">The items of the form.</param>
        public FormMapped(string formName, List<FormItem> formItems)
        {
            FormName = formName;
            FormItems = formItems;
        }

        public IEnumerator<FormItem> GetEnumerator()
        {
            return FormItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}