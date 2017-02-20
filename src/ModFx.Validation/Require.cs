namespace ModFx.Validation
{
    public class Require
    {
        public static Validator<T> That<T>(T item)
        {
            return new Validator<T>(item);
        }
    }
}