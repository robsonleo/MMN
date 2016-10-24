using System;
using System.IO;
using System.Xml.Serialization;

namespace GUI
{
    /// <summary>
    /// Xml сериализатор
    /// </summary>
    public class KtcXmlSerializer : IKtcSerializer
    {
        private const string TypeDictionaryFileName = "cache\\TypeHeading.ktcdb";

        private SerializableDictionary<string, SerializebleType> _typeDictionary;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public KtcXmlSerializer()
        {
            CheckDirectory("cache");
        }

        private static void CheckDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        #region IKtcSerializer Implemetation
        /// <summary>
        /// Реализация IKtcSerializer
        /// </summary>
        /// <param name="dicionary">обьект справочника</param>
        /// <param name="name"> название справочника</param>
        /// <returns> результат сериализации</returns>
        public bool SerializeDictionary(object dicionary, string name)
        {
            CheckDirectory("cache");

            //if (VerboseSerialize(dicionary, $"cache\\{name}.xml.ktcdb"))
            //{
            //    if (_typeDictionary == null)
            //        _typeDictionary = new SerializableDictionary<string, SerializebleType>();

            //    if (_typeDictionary.ContainsKey(name))
            //        _typeDictionary[name].T = dicionary.GetType();
            //    else
            //        _typeDictionary.Add(name, new SerializebleType(dicionary.GetType()));

            //    VerboseSerialize(_typeDictionary, TypeDictionaryFileName);

            //    return true;
            //}

            return false;
        }
        /// <summary>
        /// Реализация IKtcSerializer
        /// </summary>
        /// <param name="name">название справочника</param>
        /// <returns> упакованный оьект справочника</returns>
        public object DeserializeDictionary(string name)
        {
            if (_typeDictionary == null)
            {
                _typeDictionary = VerboseDeserialize(TypeDictionaryFileName, typeof(SerializableDictionary<string, SerializebleType>)) as SerializableDictionary<string, SerializebleType> ??
                                  new SerializableDictionary<string, SerializebleType>();
            }

            CheckDirectory("cache");

            return _typeDictionary.ContainsKey(name) ? VerboseDeserialize($"cache\\{name}.xml.ktcdb", _typeDictionary[name].T) : null;
        }
        /// <summary>
        /// Реализация IKtcSerializer
        /// </summary>
        /// <param name="_object">обьект для сериализации</param>
        /// <param name="name"> название</param>
        /// <returns> результат сериализации</returns>
        public bool Serialize(object _object, string name)
        {
            return VerboseSerialize(_object, name, true);
        }

        private bool VerboseSerialize(object _object, string name, bool verbose = false)
        {
            XmlSerializer serializer = new XmlSerializer(_object.GetType());

            FileStream fs = null;
            try
            {
                fs = new FileStream(name, FileMode.Create);
                serializer.Serialize(fs, _object);
                return true;
            }
            catch (Exception e)
            {
                //if (verbose)
                //    ExceptionLogger.Write(e);

                return false;
            }
            finally
            {
                fs?.Close();
            }
        }
        /// <summary>
        /// Реализация IKtcSerializer
        /// </summary>
        /// <param name="name">название обьекта</param>
        /// <param name="type"> тип</param>
        /// <returns>упакованный обьект класса</returns>
        public object Deserialize(string name, Type type)
        {
            return VerboseDeserialize(name, type, true);
        }

        private object VerboseDeserialize(string name, Type type, bool verbose = false)
        {
            FileStream fs = null;
            XmlSerializer serializer = new XmlSerializer(type);
            try
            {
                fs = new FileStream(name, FileMode.Open, FileAccess.Read);
                var res = serializer.Deserialize(fs);
                return res;
            }
            catch (Exception e)
            {
                //if (verbose)
                //    ExceptionLogger.Write(e);

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
