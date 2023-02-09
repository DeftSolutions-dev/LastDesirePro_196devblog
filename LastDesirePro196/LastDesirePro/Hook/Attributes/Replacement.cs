using System;
using System.Linq;
using System.Reflection;

namespace LastDesirePro.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReplacementAttribute : Attribute
    {
        public Type Class { get; private set; }
        public string MethodName { get; private set; }
        public MethodInfo Method { get; private set; }
        public BindingFlags Flags { get; private set; }
        public bool MethodFound { get; private set; }
        public ReplacementAttribute(Type tClass, string method, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, int index = 0)
        {
            Class = tClass;
            MethodName = method;
            Flags = flags;
            try
            {
                Method = Class.GetMethods(flags).Where(a => a.Name == method).ToArray()[index];
                MethodFound = true;
            }
            catch (Exception)
            {
                MethodFound = false;
            }
        }
    }
}
