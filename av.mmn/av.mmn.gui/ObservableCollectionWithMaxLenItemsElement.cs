using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using av.mmn.gui.View.Tables.UniversalTable;

namespace av.mmn.gui
{
    /// <summary>
    /// ObservableCollection с автовычислением максимальной длинны полей.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    public class ObservableCollectionWithMaxLenElement<TInput> : ObservableCollection<TInput>
    {
        private ChangeableKeyValuePair<string, int>[] _maxLengthItemCollection;

        /// <summary>
        /// Максимальное количество символов в указаном поле. 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public int GetMaxLengthColumn(string columnName)
        {
            return _maxLengthItemCollection.FirstOrDefault(p => p.Key == columnName).Value;
        }

        public ObservableCollectionWithMaxLenElement()
        { }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified collection.
        //
        // Parameters:
        //   collection:
        //     The collection from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The collection parameter cannot be null.
        public ObservableCollectionWithMaxLenElement(IEnumerable<TInput> collection)
        {
            Initialize(collection);
        }

        //
        // Summary:
        //     Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection<T>
        //     class that contains elements copied from the specified list.
        //
        // Parameters:
        //   list:
        //     The list from which the elements are copied.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The list parameter cannot be null.
        public ObservableCollectionWithMaxLenElement(List<TInput> list)
        {
            Initialize(list);
        }

        private void Initialize(IEnumerable<TInput> collection)
        {
            if (collection.Any())
            {
                var propertiesNames = collection.First().GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name);
                _maxLengthItemCollection = new ChangeableKeyValuePair<string, int>[propertiesNames.Count()];
                for (int i = 0; i < _maxLengthItemCollection.Length; i++)
                {
                    _maxLengthItemCollection[i] = new ChangeableKeyValuePair<string, int>(propertiesNames.ElementAt(i),
                        0);
                }
            }
            CopyFrom(collection);
        }

        private void CopyFrom(IEnumerable<TInput> collection)
        {
            IList<TInput> items = Items;
            if ((collection != null) && (items != null))
            {
                using (IEnumerator<TInput> enumerator = collection.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        items.Add(enumerator.Current);
                        CalculateMaxLengthColumn(enumerator.Current);
                    }
                }
            }
        }

        /// <summary>
        /// Вычисление максимальной длинны полей в колонке.
        /// </summary>
        /// <param name="value"></param>
        protected virtual void CalculateMaxLengthColumn(Object value)
        {
            if (_maxLengthItemCollection == null) return;
            for (int i = 0; i < _maxLengthItemCollection.Length; i++)
            {
                int lengthItem = value.GetType().GetProperty(_maxLengthItemCollection[i].Key).GetValue(value, null).ToString().Length;
                if (_maxLengthItemCollection[i].Value < lengthItem)
                    _maxLengthItemCollection[i].Value = lengthItem;
            }
        }
    }
}
