using System;
using System.Collections.Generic;

namespace ModFx.Validation.Validators
{
    public static class ObjectValidator
    {
        public static Validator<T> IsNotNull<T, TException>(this Validator<T> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => x != null, message);
            return validator;
        }

        public static Validator<T> IsNotNull<T>(this Validator<T> validator, string message = null)
        {
            validator.IsNotNull<T, ArgumentException>(message);
            return validator;
        }

        public static Validator<T> IsNull<T, TException>(this Validator<T> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => x == null, message);
            return validator;
        }

        public static Validator<T> IsNull<T>(this Validator<T> validator, string message = null)
        {
            validator.IsNull<T, ArgumentException>(message);
            return validator;
        }

        public static Validator<T> IsNotDefault<T, TException>(this Validator<T> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => !EqualityComparer<T>.Default.Equals(x, default(T)), message);
            return validator;
        }

        public static Validator<T> IsNotDefault<T>(this Validator<T> validator, string message = null)
        {
            validator.IsNotNull<T, ArgumentException>(message);
            return validator;
        }

        public static Validator<T> IsDefault<T, TException>(this Validator<T> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => EqualityComparer<T>.Default.Equals(x, default(T)), message);
            return validator;
        }

        public static Validator<T> IsDefault<T>(this Validator<T> validator, string message = null)
        {
            validator.IsNull<T, ArgumentException>(message);
            return validator;
        }
    }
}