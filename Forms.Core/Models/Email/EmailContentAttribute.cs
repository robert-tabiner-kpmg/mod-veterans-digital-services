using System;

namespace Forms.Core.Models.Email
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailContentAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public EmailContentAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}