using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace ChocoUtil.Algorithms
{
    /// <summary>
    /// Generic class for caching a computable value.
    /// </summary>
    /// <typeparam name="T">The type of the value to cache. Can be anything.</typeparam>
    public class CachedValue<T>
    {
        private readonly Func<T> recomputeFunc;
        private T? cache;
        /// <summary>
        /// Indicates if the cache is valid.
        /// </summary>
        public bool CacheValid { get; private set; } = false;
        /// <summary>
        /// Retrieves the cache's value. If the cache isn't valid, 
        /// recomputation is triggered.
        /// </summary>
        /// <returns>The cache's value.</returns>
        public T Get()
        {
            if (cache == null || !CacheValid)
            {
                cache = recomputeFunc();
                CacheValid = true;
            
            }
            return cache;
        }
        /// <summary>
        /// Invalidates the cache. The next Get() invocation will trigger recomputation.
        /// The cache value will be reset to the type's default value. This can be used
        /// to reduce memory pressure for reference types (as they would be set to null).
        /// </summary>
        public void InvalidateCache()
        {
            cache = default;
            CacheValid = false;
        }
        /// <summary>
        /// Constructs a CachedValue.
        /// </summary>
        /// <param name="recomputeFunc">The function to call to update the cache's value.</param>
        public CachedValue(Func<T> recomputeFunc)
        {
            this.recomputeFunc = recomputeFunc;
        }
    }
}
