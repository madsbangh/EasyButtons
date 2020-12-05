namespace EasyButtons
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using UnityEngine;

    public static class ScriptableObjectGenerator
    {
        private const string AssemblyName = "DynamicAssembly";

        private static readonly AssemblyBuilder AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
            new AssemblyName(AssemblyName),
            AssemblyBuilderAccess.RunAndSave);

        private static readonly ModuleBuilder ModuleBuilder = AssemblyBuilder.DefineDynamicModule(AssemblyName, AssemblyFileName);

        private static string AssemblyFileName => $"{AssemblyName}.dll";

        public static Type CreateClass(string className, string fieldName, Type fieldType)
        {
            TypeBuilder typeBuilder = ModuleBuilder.DefineType(className, TypeAttributes.Public, typeof(ScriptableObject));
            typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Public);
            Type type = typeBuilder.CreateType();
            AssemblyBuilder.Save(AssemblyFileName);
            return type;
        }
    }
}