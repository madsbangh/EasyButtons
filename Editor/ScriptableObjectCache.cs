namespace EasyButtons
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using NUnit.Framework;
    using UnityEngine;

    public static class ScriptableObjectCache
    {
        private const string AssemblyName = "DynamicAssembly";

        private static readonly AssemblyBuilder AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
            new AssemblyName(AssemblyName),
            AssemblyBuilderAccess.RunAndSave);

        private static readonly ModuleBuilder ModuleBuilder = AssemblyBuilder.DefineDynamicModule(AssemblyName, AssemblyFileName);

        private static readonly Dictionary<string, Type> ClassDict = new Dictionary<string, Type>();

        private static string AssemblyFileName => $"{AssemblyName}.dll";

        public static Type GetClass(string fieldName, Type fieldType)
        {
            string className = GetClassName(fieldName, fieldType);

            if (ClassDict.TryGetValue(className, out Type classType))
                return classType;

            classType = CreateClass(className, fieldName, fieldType);
            ClassDict[className] = classType;
            return classType;
        }

        public static Type CreateClass(string className, string fieldName, Type fieldType)
        {
            TypeBuilder typeBuilder = ModuleBuilder.DefineType(className, TypeAttributes.Public, typeof(ScriptableObject));
            typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Public);
            Type type = typeBuilder.CreateType();
            AssemblyBuilder.Save(AssemblyFileName);
            return type;
        }

        private static string GetClassName(string fieldName, Type fieldType)
        {
            string fullTypeName = fieldType.FullName;

            Assert.IsNotNull(fullTypeName);

            string classSafeTypeName = fullTypeName
                .Replace('.', '_')
                .Replace('`', '_');

            return $"{classSafeTypeName}_{fieldName}".CapitalizeFirstChar();
        }
    }
}