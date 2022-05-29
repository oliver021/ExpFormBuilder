using MudFormBuilder;
using System;

namespace MudFormBuilder
{
    /// <summary>
    /// This interface provides the method to create a form through the FormMapped.
    public interface IFormBuilder
    {
        /// <summary>
        /// The form mapped.
        /// </summary>
        FormMapped GetForm();
    }
}