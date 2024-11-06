using System;

namespace Helpers
{
    public class AbstractSingleton<T> where T : class
    {
        static readonly Lazy<T> Instance;

        public static T instance
        {
            get => Instance.Value;
        }
    }
}
