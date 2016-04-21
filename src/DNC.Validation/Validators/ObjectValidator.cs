using System;

namespace DNC.Validation.Validators
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
    }
}