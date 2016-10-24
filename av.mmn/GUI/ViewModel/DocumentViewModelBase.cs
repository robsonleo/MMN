using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace GUI.ViewModel
{
    /// <summary>
    /// абстрактная модель представления документа
    /// </summary>
    public abstract class DocumentViewModelBase : WorkSpaceViewModel,
        IFieldContainer, IImageSourceContainer, IXmlSerializable
    {
        private static int _count;
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        protected DocumentViewModelBase()
        {
            Name = $@"Новый {this} {++_count}";
            BridgeRow = new CorkRowViewModel() { Parent = this };
        }
        /// <summary>
        /// Лог
        /// </summary>
        /// <see cref="DocumentLog"/>
        //todo public abstract DocumentLog Log { get; }
        /// <summary>
        /// Номер документа
        /// </summary>
        public NumberDocumentViewModel NumberDocument { get; protected set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateViewModel Date { get; protected set; }
        /// <summary>
        /// путь сохранения файла
        /// </summary>
        public virtual string Filepath { get; set; }
        /// <summary>
        /// Гуид для автосохранения
        /// </summary>
        public Guid FileGuid { get; protected set; }
        /// <summary>
        /// Строка для связывания с полями шапки
        /// </summary>
        protected CorkRowViewModel BridgeRow { get; }
        /// <summary>
        /// Реализация IFieldContainer
        /// </summary>
        public abstract ObservableCollection<FieldViewModelBase> FieldList { get; }
        /// <summary>
        /// Реализация IImageSourceContainer
        /// </summary>
        public string ImageSourceUri { get; protected set; }

        private bool _isBusy;
        /// <summary>
        /// Статус занесения в базу
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        private bool _isReadOnly;
        /// <summary>
        /// Режим только для чтения
        /// </summary>
        public override bool IsReadOnly => _isReadOnly;
        /// <summary>
        /// Измение режима только для чтения
        /// </summary>
        public void SetReadOnlyMode(bool value)
        {
            _isReadOnly = value;
            RaisePropertyChanged(nameof(IsReadOnly));
        }
        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        /// <returns>null</returns>
        public XmlSchema GetSchema()
        {
            return null;
        }
        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public virtual void ReadXml(XmlReader reader)
        {
            //todo uncoment
            //reader.MoveToContent();

            //Name = reader.GetAttribute(nameof(Name));
            //NumberDocument.Value = reader.GetAttribute(nameof(NumberDocument));
            //Date.Value = reader.GetAttribute(nameof(Date));

            //Guid temp;
            //FileGuid = Guid.TryParse(reader.GetAttribute(nameof(FileGuid)), out temp) ? temp : Guid.Empty;
            //Filepath = reader.GetAttribute(nameof(Filepath));
        }
        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Name), Name);
            writer.WriteAttributeString(nameof(NumberDocument), NumberDocument.Value);
            writer.WriteAttributeString(nameof(Date), Date.Value);
            writer.WriteAttributeString(nameof(FileGuid), FileGuid.ToString());
            writer.WriteAttributeString(nameof(Filepath), Filepath);
        }
    }
}
