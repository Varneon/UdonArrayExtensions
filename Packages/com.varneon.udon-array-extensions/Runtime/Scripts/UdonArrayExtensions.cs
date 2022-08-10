﻿using System;
using UnityEngine;

namespace Varneon.UdonArrayExtensions
{
    /// <summary>
    /// Array extension methods for adding partial feature set from List<T> to Udon
    /// </summary>
    public static class UdonArrayExtensions
    {
        /// <summary>
        /// Adds item to the end of the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        public static T[] Add<T>(this T[] array, T item)
        {
            int length = array.Length;

            T[] newArray = (T[])Array.CreateInstance(array.GetElementType(), length + 1);

            array.CopyTo(newArray, 0);

            newArray.SetValue(item, length);

            return newArray;
        }

        /// <summary>
        /// Adds item to the end of the array while ensuring that duplicates are not added
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        public static T[] AddUnique<T>(this T[] array, T item)
        {
            if(Array.IndexOf(array, item) >= 0) { return array; }

            return array.Add(item);
        }

        /// <summary>
        /// Inserts item to array at index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T[] Insert<T>(this T[] array, int index, T item)
        {
            int length = array.Length;

            index = Mathf.Clamp(index, 0, length);

            T[] newArray = (T[])Array.CreateInstance(array.GetElementType(), length + 1);

            newArray.SetValue(item, index);

            if (index == 0)
            {
                Array.Copy(array, 0, newArray, 1, length);
            }
            else if (index == length)
            {
                Array.Copy(array, 0, newArray, 0, length);
            }
            else
            {
                Array.Copy(array, 0, newArray, 0, index);
                Array.Copy(array, index, newArray, index + 1, length - index);
            }

            return newArray;
        }

        /// <summary>
        /// Removes item from the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        public static T[] Remove<T>(this T[] array, T item)
        {
            int index = Array.IndexOf(array, item);

            if (index == -1) { return array; }

            return array.RemoveAt(index);
        }

        /// <summary>
        /// Removes item from the array at index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            int length = array.Length;

            if(index >= length || index < 0) { return array; }

            int maxIndex = length - 1;

            T[] newArray = (T[])Array.CreateInstance(array.GetElementType(), maxIndex);

            if (index == 0)
            {
                Array.Copy(array, 1, newArray, 0, maxIndex);
            }
            else if(index == maxIndex)
            {
                Array.Copy(array, 0, newArray, 0, maxIndex);
            }
            else
            {
                Array.Copy(array, 0, newArray, 0, index);
                Array.Copy(array, index + 1, newArray, index, maxIndex - index);
            }

            return newArray;
        }

        /// <summary>
        /// Resizes the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="newSize"></param>
        /// <returns></returns>
        public static T[] Resize<T>(this T[] array, int newSize)
        {
            if(newSize < 0) { newSize = 0; }

            T[] newArray = (T[])Array.CreateInstance(array.GetElementType(), newSize);

            Array.Copy(array, 0, newArray, 0, Mathf.Min(newSize, array.Length));

            return newArray;
        }

        /// <summary>
        /// Reverses the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T[] Reverse<T>(this T[] array)
        {
            Array.Reverse(array);

            return array;
        }

        /// <summary>
        /// Gets the element type of the array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Type GetElementType(this Array array)
        {
            return Type.GetType(array.GetType().FullName.TrimEnd(']', '['));
        }
    }
}