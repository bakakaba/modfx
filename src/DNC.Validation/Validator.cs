using System;

namespace DNC.Validation
{
    public class Validator<T>
    {
        private readonly T _item;

        internal Validator(T item)
        {
            _item = item;
        }

        public void Validate<TException>(Func<T, bool> evaluator, string message) where TException : Exception
        {
            if (!evaluator(_item))
                throw (Exception)Activator.CreateInstance(typeof(TException), message);
        }

        public void Validate(Func<T, bool> evaluator, string message)
        {
            Validate<ArgumentException>(evaluator, message);
        }
    }
}