namespace Web.Api.Error
{
    public static class ValidationHelper
    {
        public static void ValidateNotNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, "Object cannot be null.");
            }
        }

        public static void ValidateNonNegative(int value, string paramName)
        {
            if (value < 0)
            {
                throw new ArgumentException("Value cannot be negative.", paramName);
            }
        }
    }
}
