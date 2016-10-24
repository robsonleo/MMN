using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GUI
{
    /// <summary>
    /// Класс-обвертка для сериализации справочников
    /// </summary>
    /// <typeparam name="TKey">тип ключа</typeparam>
    /// <typeparam name="TValue">тип значения</typeparam>
    [Serializable]
    [XmlRoot("DictionaryOfSheet")]
    public class SerializableDictionary<TKey, TValue> : IXmlSerializable, IDictionary<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> _innerDictionary;
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SerializableDictionary()
        {
            _innerDictionary = new Dictionary<TKey, TValue>();
        }
        /// <summary>
        /// Конструктор копирования
        /// </summary>
        public SerializableDictionary(IDictionary<TKey, TValue> innerDictionary)
        {
            _innerDictionary = innerDictionary;
        }

        #region IXmlSerializable
        /// <summary>
        /// Метод IXmlSerializable
        /// </summary>
        /// <returns>null</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// Метод чтения из IXmlSerializable
        /// </summary>
        /// <param name="reader">экземпляр системного ридера</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();
            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                Add(key, value);
                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }
        /// <summary>
        /// Метод записи из IXmlSerializable
        /// </summary>
        /// <param name="writer">экземпляр системного врайтера</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();
                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        #endregion

        #region IDictionary<TKey, TValue>
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _innerDictionary.Add(item);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public void Clear()
        {
            _innerDictionary.Clear();
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _innerDictionary.Contains(item);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            _innerDictionary.CopyTo(array, arrayIndex);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _innerDictionary.Remove(item);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public int Count => _innerDictionary.Count;
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool IsReadOnly => _innerDictionary.IsReadOnly;
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            return _innerDictionary.ContainsKey(key);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            _innerDictionary.Add(key, value);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool Remove(TKey key)
        {
            return _innerDictionary.Remove(key);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _innerDictionary.TryGetValue(key, out value);
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public TValue this[TKey key]
        {
            get { return _innerDictionary[key]; }
            set { _innerDictionary[key] = value; }
        }
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public ICollection<TKey> Keys => _innerDictionary.Keys;
        /// <summary>
        /// Реализация IDictionary
        /// </summary>
        public ICollection<TValue> Values => _innerDictionary.Values;
        #endregion
    }
}
