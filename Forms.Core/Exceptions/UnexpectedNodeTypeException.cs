using System;
using System.Linq;
using Forms.Core.Models.InFlight;

namespace Forms.Core.Exceptions
{
    public class UnexpectedNodeTypeException : Exception
    {
        private Type[] ExpectedTypes { get; }
        public UnexpectedNodeTypeException(FormNode actualNode, Type[] types)
            : base($"Expected one of: {string.Join(", ", types.Select(x => x.ToString()))} but found")
        {
            ExpectedTypes = types;
        }    
    }
    
    // We have some derived types for convenience in cases where we expected one of a number of types of form node
    public class UnexpectedNodeTypeException<T1> : UnexpectedNodeTypeException
    {
        public UnexpectedNodeTypeException(FormNode actualNode) : base(actualNode, new []{ typeof(T1)})
        {
        }
    }
    
    public class UnexpectedNodeTypeException<T1, T2> : UnexpectedNodeTypeException
    {
        public UnexpectedNodeTypeException(FormNode actualNode) : base(actualNode, new []{ typeof(T1), typeof(T2)})
        {
        }
    }
    
    public class UnexpectedNodeTypeException<T1, T2, T3> : UnexpectedNodeTypeException
    {
        public UnexpectedNodeTypeException(FormNode actualNode) : base(actualNode, new []{ typeof(T1), typeof(T2), typeof(T3)})
        {
        }
    }
}