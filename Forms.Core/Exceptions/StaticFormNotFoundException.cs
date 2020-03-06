using System;
using Forms.Core.Forms;

namespace Forms.Core.Exceptions
{
    public class StaticFormNotFoundException : Exception
    {
        public StaticFormNotFoundException(FormType formType)
            : base($"Could not find form type: {formType}")
        {
        }
    }
}