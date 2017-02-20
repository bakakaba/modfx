using System;

namespace ModFx.Validation.Validators
{
    public static class StringValidator
    {
        public static Validator<string> IsNotEmpty<TException>(this Validator<string> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => x != string.Empty, message);
            return validator;
        }

        public static Validator<string> IsNotEmpty(this Validator<string> validator, string message = null)
        {
            validator.IsNotEmpty<ArgumentException>(message);
            return validator;
        }

        public static Validator<string> IsEmpty<TException>(this Validator<string> validator, string message = null) where TException : Exception
        {
            validator.Validate<TException>(x => x == string.Empty, message);
            return validator;
        }

        public static Validator<string> IsEmpty(this Validator<string> validator, string message = null)
        {
            validator.IsEmpty<ArgumentException>(message);
            return validator;
        }

        public static Validator<string> IsNotNullOrEmpty<TException>(this Validator<string> validator, string message = null) where TException : Exception
        {
            validator
                .Validate<TException>(x => !string.IsNullOrEmpty(x), message);
            return validator;
        }

        public static Validator<string> IsNotNullOrEmpty(this Validator<string> validator, string message = null)
        {
            validator.IsNotNullOrEmpty<ArgumentException>(message);
            return validator;
        }

        public static Validator<string> IsNotNullOrWhitespace<TException>(this Validator<string> validator, string message = null) where TException : Exception
        {
            validator
                .Validate<TException>(x => !string.IsNullOrWhiteSpace(x), message);
            return validator;
        }

        public static Validator<string> IsNotNullOrWhitespace(this Validator<string> validator, string message = null)
        {
            validator.IsNotNullOrWhitespace<ArgumentException>(message);
            return validator;
        }
    }
}