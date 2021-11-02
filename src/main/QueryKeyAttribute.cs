using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Graph.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class QueryKeyAttribute : Attribute
    {
        public QueryKeyAttribute(string value = default)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}
