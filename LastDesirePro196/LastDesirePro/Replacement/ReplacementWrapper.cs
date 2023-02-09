using LastDesirePro.Attributes;
using System; 
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LastDesirePro.Replacement
{
        public class ReplacementWrapper
        {
            public MethodInfo Original { get; private set; }
            public MethodInfo Modified { get; private set; }
            public IntPtr PtrOriginal { get; private set; }
            public IntPtr PtrModified { get; private set; }
            public ReplacementUtilities.OffsetBackup OffsetBackup { get; private set; }
            public ReplacementAttribute Attribute { get; private set; }
            public bool Detoured { get; private set; }
            public object Instance { get; private set; }
            public bool Local { get; private set; }
            public ReplacementWrapper(MethodInfo original, MethodInfo modified, ReplacementAttribute attribute, object instance = null)
            {
                Original = original;
                Modified = modified;
                Instance = instance;
                Attribute = attribute;
                Local = Modified.DeclaringType.Assembly == Assembly.GetExecutingAssembly();
                RuntimeHelpers.PrepareMethod(original.MethodHandle);
                RuntimeHelpers.PrepareMethod(modified.MethodHandle);
                PtrOriginal = Original.MethodHandle.GetFunctionPointer();
                PtrModified = Modified.MethodHandle.GetFunctionPointer();
                OffsetBackup = new ReplacementUtilities.OffsetBackup(PtrOriginal);
                Detoured = false;
            }
            public bool Replacement()
            {
                if (Detoured)
                    return true;
                bool result = ReplacementUtilities.ReplacementFunction(PtrOriginal, PtrModified);
                if (result)
                    Detoured = true;
                return result;
            }
            public bool Revert()
            {
                if (!Detoured)
                    return false;
                bool result = ReplacementUtilities.RevertReplacement(OffsetBackup);
                if (result)
                    Detoured = false;
                return result;
            }
            public object CallOriginal(object[] args, object instance = null)
            {
                Revert();
                object result = null;
                try
                {
                    result = Original.Invoke(instance ?? Instance, args);
                }
                catch (Exception){ }
                Replacement();
                return result;
            }
        }
    }