namespace EasyButtons.Editor.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Configuration.Assemblies;
    using System.Globalization;
    using System.Reflection;
    using System.Reflection.Emit;
    using UnityEngine;
    using UnityEngine.Assertions;

    internal static class ScriptableObjectCache
    {
        private const string AssemblyName = "EasyButtons.DynamicAssembly";

        private static readonly AssemblyBuilder AssemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
            new AssemblyName(AssemblyName)
        {
            CultureInfo = CultureInfo.InvariantCulture,
            Flags = AssemblyNameFlags.None,
            ProcessorArchitecture = ProcessorArchitecture.MSIL,
            VersionCompatibility = AssemblyVersionCompatibility.SameDomain
        }, AssemblyBuilderAccess.Run);

        private static readonly ModuleBuilder ModuleBuilder = AssemblyBuilder.DefineDynamicModule(AssemblyName, true);

        private static readonly Dictionary<string, Type> ClassDict = new Dictionary<string, Type>();

        public static Type GetClass(string fieldName, Type fieldType)
        {
            string className = GetClassName(fieldName, fieldType);

            if (ClassDict.TryGetValue(className, out Type classType))
                return classType;

            if ( ! fieldType.IsUnitySerializable())
            {
                fieldType = typeof(NonSerializedError);
            }

            classType = CreateClass(className, fieldName, fieldType);
            ClassDict[className] = classType;
            return classType;
        }

        private static Type CreateClass(string className, string fieldName, Type fieldType)
        {
            TypeBuilder typeBuilder = ModuleBuilder.DefineType(
                $"{AssemblyName}.{className}",
                TypeAttributes.NotPublic,
                typeof(ScriptableObject));

            typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Public);
            Type type = typeBuilder.CreateType();
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