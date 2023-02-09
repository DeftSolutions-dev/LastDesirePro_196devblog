using LastDesirePro.Attributes;
using System; 
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace LastDesirePro.Replacement
{
    public static class ReplacementUtilities
    {
        public static object CallOriginalFunc(MethodInfo method, object instance = null, params object[] args)
        {
            if (ReplacementManager.Replacements.All(o => o.Value.Original != method))
                throw new Exception("The Replacement specified was not found!");
            ReplacementWrapper wrapper = ReplacementManager.Replacements.First(a => a.Value.Original == method).Value;
            return wrapper.CallOriginal(args, instance);
        }
        public static object CallOriginal(object instance = null, params object[] args)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(false);
            if (trace.FrameCount < 1)
                throw new Exception("Invalid trace back to the original method! Please provide the methodinfo instead!");
            MethodBase modded = trace.GetFrame(1).GetMethod();
            MethodInfo original = null;
            if (!Attribute.IsDefined(modded, typeof(ReplacementAttribute)))
                modded = trace.GetFrame(2).GetMethod();
            ReplacementAttribute att = (ReplacementAttribute)Attribute.GetCustomAttribute(modded, typeof(ReplacementAttribute));
            if (att == null)
                throw new Exception("This method can only be called from an overwritten method!");
            if (!att.MethodFound)
                throw new Exception("The original method was never found!");
            original = att.Method;
            if (ReplacementManager.Replacements.All(o => o.Value.Original != original))
                throw new Exception("The Replacement specified was not found!");
            ReplacementWrapper wrapper = ReplacementManager.Replacements.First(a => a.Value.Original == original).Value;
            return wrapper.CallOriginal(args, instance);
        }
        public static bool EnableReplacement(MethodInfo method)
        {
            ReplacementWrapper wrapper = ReplacementManager.Replacements.First(a => a.Value.Original == method).Value;
            return wrapper != null && wrapper.Replacement();
        }
        public static bool DisableReplacement(MethodInfo method)
        {
            ReplacementWrapper wrapper = ReplacementManager.Replacements.First(a => a.Value.Original == method).Value;
            return wrapper != null && wrapper.Revert();
        }
        public static bool ReplacementFunction(IntPtr ptrOriginal, IntPtr ptrModified)
        {
            try
            {
                switch (IntPtr.Size)
                {
                    case sizeof(Int32):
                        unsafe
                        {
                            byte* ptrFrom = (byte*)ptrOriginal.ToPointer();
                            *ptrFrom = 0x68;
                            *((uint*)(ptrFrom + 1)) = (uint)ptrModified.ToInt32();
                            *(ptrFrom + 5) = 0xC3;
                        }
                        break;
                    case sizeof(Int64):
                        unsafe
                        {
                            byte* ptrFrom = (byte*)ptrOriginal.ToPointer();
                            *ptrFrom = 0x48;
                            *(ptrFrom + 1) = 0xB8;
                            *((ulong*)(ptrFrom + 2)) = (ulong)ptrModified.ToInt64();
                            *(ptrFrom + 10) = 0xFF;
                            *(ptrFrom + 11) = 0xE0;
                        }
                        break;
                    default:
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return false;
            }
        }
        public static bool RevertReplacement(OffsetBackup backup)
        {
            try
            {
                unsafe
                {
                    byte* ptrOriginal = (byte*)backup.Method.ToPointer();
                    *ptrOriginal = backup.A;
                    *(ptrOriginal + 1) = backup.B;
                    *(ptrOriginal + 10) = backup.C;
                    *(ptrOriginal + 11) = backup.D;
                    *(ptrOriginal + 12) = backup.E;
                    if (IntPtr.Size == sizeof(Int32))
                    {
                        *((uint*)(ptrOriginal + 1)) = backup.F32;
                        *(ptrOriginal + 5) = backup.G;
                    }
                    else
                        *((ulong*)(ptrOriginal + 2)) = backup.F64;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public class OffsetBackup
        {
            public IntPtr Method;
            public byte A, B, C, D, E, G;
            public ulong F64;
            public uint F32;
            public OffsetBackup(IntPtr method)
            {
                Method = method;
                unsafe
                {
                    byte* ptrMethod = (byte*)method.ToPointer();
                    A = *ptrMethod;
                    B = *(ptrMethod + 1);
                    C = *(ptrMethod + 10);
                    D = *(ptrMethod + 11);
                    E = *(ptrMethod + 12);
                    if (IntPtr.Size == sizeof(Int32))
                    {
                        F32 = *((uint*)(ptrMethod + 1));
                        G = *(ptrMethod + 5);
                    }
                    else
                        F64 = *((ulong*)(ptrMethod + 2));
                }
            }
        }
    }
}