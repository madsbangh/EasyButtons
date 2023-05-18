namespace EasyButtons.Editor.Utils
{
    using System;

    public static class EnumExtensions
    {
        public static bool ContainsFlag<TEnum>(this TEnum thisEnum, TEnum flag) where TEnum : struct, Enum
        {
            Type enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type.");
            }

            Type underlyingType = Enum.GetUnderlyingType(enumType);

            switch (Type.GetTypeCode(underlyingType))
            {
                case TypeCode.Byte:
                    return (((byte)(object)thisEnum & (byte)(object)flag) > 0);
                case TypeCode.SByte:
                    return (((sbyte)(object)thisEnum & (sbyte)(object)flag) > 0);
                case TypeCode.Int16:
                    return (((short)(object)thisEnum & (short)(object)flag) > 0);
                case TypeCode.UInt16:
                    return (((ushort)(object)thisEnum & (ushort)(object)flag) > 0);
                case TypeCode.Int32:
                    return (((int)(object)thisEnum & (int)(object)flag) > 0);
                case TypeCode.UInt32:
                    return (((uint)(object)thisEnum & (uint)(object)flag) > 0);
                case TypeCode.Int64:
                    return (((long)(object)thisEnum & (long)(object)flag) > 0);
                case TypeCode.UInt64:
                    return (((ulong)(object)thisEnum & (ulong)(object)flag) > 0);
                default:
                    throw new Exception("Underlying type of the enum is not supported.");
            }
        }
    }
}
