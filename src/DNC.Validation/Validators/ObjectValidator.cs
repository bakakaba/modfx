using System;

namespace DNC.Validation.Validators
{
    public static class ObjectValidator
    {
        public static Validator<string> IsNotNull<TException>(this Validator<string> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => x != null, message);
            return validator;
        }

        public static Validator<string> IsNotNull(this Validator<string> validator, string message = null)
        {
            validator.IsNotNull<ArgumentException>(message);
            return validator;
        }

        public static Validator<string> IsNull<TException>(this Validator<string> validator, string message) where TException : Exception
        {
            validator.Validate<TException>(x => x == null, message);
            return validator;
        }

        public static Validator<string> IsNull(this Validator<string> validator, string message)
        {
            validator.IsNull<ArgumentException>(message);
            return validator;
        }
    }
}