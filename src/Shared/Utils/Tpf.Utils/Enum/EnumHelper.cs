using System;
using System.Linq;

namespace Tpf.Utils
{
    public static class EnumHelper
    {
        public static TEnum GetValue<TEnum>(string name) where TEnum : struct
        {
            if (string.IsNullOrEmpty(name))
            {
                return default(TEnum);
            }

            var result = default(TEnum);
            if (Enum.TryParse<TEnum>(name, out result))
            {
                return result;
            }

            return default(TEnum);
        }

        public static TEnum? GetValue<TEnum>(string name, bool ifNullReturnDefault) where TEnum : struct
        {
            if (string.IsNullOrEmpty(name))
            {
                return ifNullReturnDefault ? default(TEnum) : null;
            }

            var result = default(TEnum);
            if (Enum.TryParse<TEnum>(name, out result))
            {
                return result;
            }

            return ifNullReturnDefault ? default(TEnum) : null;
        }

        
        public static T Next<T>(this T v) where T : struct
        {
            return Enum.GetValues(v.GetType())
                .Cast<T>()
                .Concat(new[] { default(T) })
                .SkipWhile(e => !v.Equals(e))
                .Skip(1)
                .First();
        }

        public static T Previous<T>(this T v) where T : struct
        {
            return Enum.GetValues(v.GetType())
                .Cast<T>()
                .Concat(new[] { default(T) })
                .Reverse()
                .SkipWhile(e => !v.Equals(e))
                .Skip(1)
                .First();
        }

    }
}
