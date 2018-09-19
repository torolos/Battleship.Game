﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Extension methods class
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Wraps the 'foreach' loop for <see cref="IEnumerable{T}"/> objects
        /// </summary>
        /// <typeparam name="T">The generic argument</typeparam>
        /// <param name="instance">the <see cref="IEnumerable{T}"/> instance</param>
        /// <param name="action">the <see cref="Action"/> to invoke</param>
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            if (instance.Any())
            {
                foreach (var item in instance)
                {
                    action.Invoke(item);
                }
            }
        }
        /// <summary>
        /// Returns a randomly selected item from a <see cref="IEnumerable{T}"/> instance
        /// </summary>
        /// <typeparam name="T">The enumerable argument</typeparam>
        /// <param name="instance">The <see cref="IEnumerable{T}"/> instance</param>
        /// <returns></returns>
        public static T Random<T>(this IEnumerable<T> instance)
        {
            if (instance.Any())
            {
                var index = GameUtility.CreateRandom(0, instance.Count());
                return instance.ElementAt(index);
            }
            return default(T);
        }
    }
}
