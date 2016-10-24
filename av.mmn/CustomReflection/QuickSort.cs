using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CustomFunctions
{
    /// <summary>
    /// Реализация быстрой сортировки.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QuickSort<T>
    {
        /// <summary>
        /// Запуск быстрой сортировки.
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="order"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> StartSort(T[] mas, SortOrder order, Func<T, T, int> comparer)
        {
            int sortOrderIndex = order == SortOrder.Ascending ? 1 : -1;
            QuickSorting(mas, 0, mas.Count()-1, sortOrderIndex, comparer);
            return mas;
        }

        /// <summary>
        /// Быстрая сортировка
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="firstIndex"></param>
        /// <param name="lastIndex"></param>
        /// <param name="sortOrderIndex"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static T[] QuickSorting(T[] mas, int firstIndex, int lastIndex, int sortOrderIndex,
            Func<T, T, int> comparer)
        {
            int left = firstIndex;
            int right = lastIndex;

            T midElement = mas[(firstIndex + lastIndex) >> 1];

            do
            {
                while (comparer(mas[left], midElement)*sortOrderIndex == -1) left++;
                while (comparer(midElement, mas[right])*sortOrderIndex == -1) right--;
                if (left <= right)
                {
                    T temp = mas[left];
                    mas[left] = mas[right];
                    mas[right] = temp;
                    left++;
                    right--;
                }
            } while (left < right);

            if (firstIndex < right) QuickSorting(mas, firstIndex, right, sortOrderIndex, comparer);
            if (left < lastIndex) QuickSorting(mas, left, lastIndex, sortOrderIndex, comparer);

            return mas;
        } 
    }
}
