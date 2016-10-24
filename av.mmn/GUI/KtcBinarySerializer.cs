using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GUI
{
    /// <summary>
    /// Бинарный сериализатор
    /// </summary>
    public class KtcBinarySerializer : IKtcSerializer
    {
        private readonly BinaryFormatter _serializer;

        private readonly KtcXmlSerializer _documentSerializer;
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public KtcBinarySerializer()
        {
            _serializer = new BinaryFormatter();
            _documentSerializer = new KtcXmlSerializer();
            CheckDirectory("cache");
        }

        private static void CheckDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        #region IKtcSerializer implementation
        /// <summary>
        /// реализация IKtcSerializer
        /// </summary>
        public bool Serialize(object document, string name)
        {
            return _documentSerializer.Serialize(document, name);
        }
        /// <summary>
        /// реализация IKtcSerializer
        /// </summary>
        public object Deserialize(string name, Type type)
        {
            return _documentSerializer.Deserialize(name, type);
        }
        /// <summary>
        /// реализация IKtcSerializer
        /// </summary>
        public bool SerializeDictionary(object dicionary, string name)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream($"cache\\{name}.ktcdb", FileMode.Create);
                _serializer.Serialize(fs, dicionary);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs?.Close();
            }
        }
        /// <summary>
        /// реализация IKtcSerializer
        /// </summary>
        public object DeserializeDictionary(string name)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream($"cache\\{name}.ktcdb", FileMode.Open);
                return _serializer.Deserialize(fs);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                fs?.Close();
            }
        }
        #endregion
    }
}
