namespace EasyButtons.Editor.Utils
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public static class DefaultValuesUtility
    {
        public static void InjectDefaultValue(TypeBuilder typeBuilder, FieldBuilder fieldBuilder, object defaultValue)
        {
            var valueType = defaultValue.GetType();
            if (!valueType.IsEnum && !valueType.IsUnitySerializablePrimitive())
                return;

            // Define the constructor for the type
            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public,
                CallingConventions.Standard, Type.EmptyTypes);
            var constructorIL = constructorBuilder.GetILGenerator();

            // Load "this" onto the stack
            constructorIL.Emit(OpCodes.Ldarg_0);

            // Inject the default value
            PushOntoStack(constructorIL, defaultValue);

            // Store the converted value in the field
            constructorIL.Emit(OpCodes.Stfld, fieldBuilder);

            // Return from the constructor
            constructorIL.Emit(OpCodes.Ret);
        }

        private static void PushOntoStack(ILGenerator il, object defaultValue)
        {
            if (defaultValue.GetType().IsEnum)
            {
                il.Emit(OpCodes.Ldc_I4, (int)defaultValue);
                return;
            }

            switch (defaultValue)
            {
                case bool boolValue:
                    il.Emit(boolValue ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                    break;
                case byte byteValue:
                    il.Emit(OpCodes.Ldc_I4_S, byteValue);
                    break;
                case sbyte sbyteValue:
                    il.Emit(OpCodes.Ldc_I4_S, sbyteValue);
                    break;
                case char charValue:
                    il.Emit(OpCodes.Ldc_I4_S, charValue);
                    break;
                case double doubleValue:
                    il.Emit(OpCodes.Ldc_R8, doubleValue);
                    break;
                case float floatValue:
                    il.Emit(OpCodes.Ldc_R4, floatValue);
                    break;
                case int intValue:
                    il.Emit(OpCodes.Ldc_I4, intValue);
                    break;
                case uint uintValue:
                    il.Emit(OpCodes.Ldc_I4, uintValue);
                    break;
                case long longValue:
                    il.Emit(OpCodes.Ldc_I8, longValue);
                    break;
                case ulong ulongValue:
                    il.Emit(OpCodes.Ldc_I8, (long)ulongValue);
                    break;
                case short shortValue:
                    il.Emit(OpCodes.Ldc_I4_S, shortValue);
                    break;
                case ushort ushortValue:
                    il.Emit(OpCodes.Ldc_I4_S, ushortValue);
                    break;
                case string stringValue:
                    il.Emit(OpCodes.Ldstr, stringValue);
                    break;
                default:
                    throw new ArgumentException("Unexpected type of defaultValue: " + defaultValue.GetType());
            }
        }
    }
}