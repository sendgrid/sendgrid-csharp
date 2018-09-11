using System;
using System.Collections.Generic;

namespace SendGrid.Helpers.Utility
{
    /// <summary>
    /// This class could be used to write delegate comparers.
    /// Especially useful for linq queries https://gist.github.com/kjellski/6267515
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> lambdaComparer;
        private readonly Func<T, int> lambdaHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
        /// The comparer itself, initialized with
        /// </summary>
        /// <param name="lambdaComparer">Comparer</param>
        public LambdaComparer(Func<T, T, bool> lambdaComparer)
            : this(lambdaComparer, o => 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaComparer{T}"/> class.
        /// Constructor for generating the Comparer itself
        /// </summary>
        /// <param name="lambdaComparer">Comparer</param>
        /// <param name="lambdaHash">Hash</param>
        public LambdaComparer(Func<T, T, bool> lambdaComparer, Func<T, int> lambdaHash)
        {
            this.lambdaComparer = lambdaComparer ?? throw new ArgumentNullException("lambdaComparer");
            this.lambdaHash = lambdaHash ?? throw new ArgumentNullException("lambdaHash");
        }

        /// <summary>
        /// Equals with _labdaComparer
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns>Result</returns>
        public bool Equals(T x, T y)
        {
            return this.lambdaComparer(x, y);
        }

        /// <summary>
        /// GetHashCode with _labdaHash
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Hash code</returns>
        public int GetHashCode(T obj)
        {
            return this.lambdaHash(obj);
        }
    }
}
