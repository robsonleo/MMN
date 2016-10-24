using System;
using System.Xml.Serialization;

namespace GUI
{
    /// <summary>
    /// Класс для сериализации типов
    /// </summary>
    public class SerializebleType
    {
        private Type _type;
        /// <summary>
        /// тип для сериализации
        /// </summary>
        [XmlIgnore]
        public Type T
        {
            get { return _type; }
            set
            {
                _type = value;
                Name = value.AssemblyQualifiedName;
            }
        }
        /// <summary>
        /// Название обьекта
        /// </summary>
        public string Name
        {
            get { return _type.AssemblyQualifiedName; }
            set { _type = Type.GetType(value); }
        }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public SerializebleType()
        {

        }
        /// <summary>
        /// конструктор копирования
        /// </summary>
        public SerializebleType(Type t)
        {
            T = t;
        }
    }
}
