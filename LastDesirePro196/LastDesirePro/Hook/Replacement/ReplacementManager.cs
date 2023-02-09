using LastDesirePro.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LastDesirePro.Replacement
{ 
        public static class ReplacementManager
        {
            private static Dictionary<ReplacementAttribute, ReplacementWrapper> _Replacements =
                new Dictionary<ReplacementAttribute, ReplacementWrapper>();
            public static Dictionary<ReplacementAttribute, ReplacementWrapper> Replacements => _Replacements;
            public static void LoadReplacement(MethodInfo method)
            {
                ReplacementAttribute attribute =
                    (ReplacementAttribute)Attribute.GetCustomAttribute(method, typeof(ReplacementAttribute));
                if (Replacements.Count(a => a.Key.Method == attribute.Method) > 0)
                    return;
                ReplacementWrapper wrapper = new ReplacementWrapper(attribute.Method, method, attribute);
                wrapper.Replacement();
            }
        }
    } 