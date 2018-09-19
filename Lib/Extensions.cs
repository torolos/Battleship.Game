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
        /// Wraps the 'foreach' loop for <see cref="IEnumerable{T}"/> objects
        /// </summary>
        /// <param name="instance">The <see cref="IEnumerable{T}"/> instance</param>
        /// <param name="func">The <see cref="Func{T, TResult}"/> to invoke</param>
        /// <returns></returns>
        public static R ForEach<T, R>(this IEnumerable<T> instance, Func<T, R> func)
        {
            if (instance.Any())
            {
                foreach(var item in instance)
                {
                    return func.Invoke(item);
                }
            }
            return default(R);
        }
    }
}
