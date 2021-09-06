using System;
using System.Collections.Generic;

namespace CurrencyApi.Infrastructure.Core
{
    /// <summary>
    /// Provides access to all "singletons" stored by <see cref="Singleton{T}" />.
    /// </summary>
    public abstract class SingletonBase
    {
        static SingletonBase()
        {
            AllSingletons = new Dictionary<Type, object?>();
        }

        /// <summary>
        /// Dictionary of type to singleton instances.
        /// </summary>
        public static IDictionary<Type, object?> AllSingletons { get; }
    }
}