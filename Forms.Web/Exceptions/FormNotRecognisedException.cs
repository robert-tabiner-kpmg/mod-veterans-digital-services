using System;

namespace Forms.Web.Exceptions
{
    public class FormNotRecognisedException : Exception
    {
        public FormNotRecognisedException(string type) : base($"Form not recognised: {type}")
        {
        }
    }
}