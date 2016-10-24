using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления документа ЛИН
    /// </summary>
    public sealed class LinDocumentViewModel : DocumentViewModelBase, IList<LinRow>, INotifyCollectionChanged//, IProcessToDatabase
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinDocumentViewModel()
        {
            Rows = new ObservableCollection<LinRow>();
            Rows.CollectionChanged += (sender, args) =>
            {
                CollectionChanged?.Invoke(this, args);
                base.IsChanged = true;
            };
            ImageSourceUri = @"../../Resources/Images/LinIcon.png";
            FileGuid = Guid.NewGuid();
            Department = new LinDepartmentViewModel()
            {
                Name = "Цех(Отдел)",
                Value = string.Empty,
                AllowEmpty = true,
                Row = BridgeRow
            };

            NumberLin = new NumberLinViewModel()
            {
                Name = "№ ЛИН",
                Value = string.Empty,
                AllowEmpty = false,
                Row = BridgeRow
            };

            NumberDocument = new NumberDocumentViewModel()
            {
                Name = "Номер документа",
                Value = string.Empty,
                AllowEmpty = false,
                Row = BridgeRow
            };

            Date = new DateViewModel()
            {
                Name = "Дата",
                Value = string.Empty,
                AllowEmpty = false,
                Row = BridgeRow
            };

            FieldList = new ObservableCollection<FieldViewModelBase>()
            {
                Department,
                NumberLin,
                NumberDocument,
                Date
            };
            RegisteDependencies();
        }
        /// <summary>
        /// Признак валидности
        /// </summary>
        public override bool? IsValid => Rows.All(row => row.IsValid != false) && FieldList.All(field => field.IsValid != false);
        /// <summary>
        /// Сброс Валидности
        /// </summary>
        public override void RefreshIsValid()
        {
            foreach (FieldViewModelBase field in FieldList)
                field.RefreshIsValid();

            foreach (LinRow row in Rows)
                row.RefreshIsValid();
        }
        /// <summary>
        /// Признак изменения
        /// </summary>
        public override bool IsChanged
        {
            get { return base.IsChanged || FieldList.Any(field => field.IsChanged) || Rows.Any(row => row.IsChanged); }
        }
        /// <summary>
        /// Сброс изменений
        /// </summary>
        public override void RefreshChanges()
        {
            foreach (var field in FieldList) field.RefreshChanges();
            foreach (var linRow in Rows) linRow.RefreshChanges();
        }
        /// <summary>
        /// Реализация IFieldContainer
        /// </summary>
        public override ObservableCollection<FieldViewModelBase> FieldList { get; }
        /// <summary>
        /// Лог
        /// </summary>
        //public override DocumentLog Log => new DocumentLog(this);
        /// <summary>
        /// Цех
        /// </summary>
        public LinDepartmentViewModel Department { get; }
        /// <summary>
        /// Номер Лина
        /// </summary>
        public NumberLinViewModel NumberLin { get; }
        /// <summary>
        /// Строки Лина
        /// </summary>
        private ObservableCollection<LinRow> Rows { get; }

        /// <summary>
        /// Регистрация зависимостей
        /// </summary>
        protected override void RegisteDependencies()
        {
            RegisterElementPropertyDependencies(nameof(IsValid), this, new[] { nameof(FieldList) },
                () =>
                {
                    RegisterCollectionPropertyDependencies(nameof(IsValid), FieldList, new[] { nameof(IsValid) });
                    RegisterCollectionPropertyDependencies(nameof(IsValid), Rows, new[] { nameof(IsValid) });
                });
            RegisterElementPropertyDependencies(nameof(IsChanged), this, new[] { nameof(FieldList), nameof(Rows) },
               () =>
               {
                   RegisterCollectionPropertyDependencies(nameof(IsChanged), FieldList, new[] { nameof(IsChanged) });
                   RegisterCollectionPropertyDependencies(nameof(IsChanged), Rows, new[] { nameof(IsChanged) });
               });
        }

        #region ICollection implementation
        /// <summary>
        /// Реализация IList
        /// </summary>
        public IEnumerator<LinRow> GetEnumerator()
        {
            return Rows.GetEnumerator();
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public void Add(LinRow item)
        {
            item.Document = this;
            Rows.Add(item);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public void Clear()
        {
            Rows.Clear();
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public bool Contains(LinRow item)
        {
            return Rows.Contains(item);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public void CopyTo(LinRow[] array, int arrayIndex)
        {
            Rows.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public bool Remove(LinRow item)
        {
            return Rows.Count != 1 && Rows.Remove(item);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public int Count => Rows.Count;

        /// <summary>
        /// Реализация IList
        /// </summary>
        public LinRow this[int index]
        {
            get
            {
                return ((IList<LinRow>)Rows)[index];
            }

            set
            {
                ((IList<LinRow>)Rows)[index] = value;
            }
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public int IndexOf(LinRow item)
        {
            return ((IList<LinRow>)Rows).IndexOf(item);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public void Insert(int index, LinRow item)
        {
            ((IList<LinRow>)Rows).Insert(index, item);
        }

        /// <summary>
        /// Реализация IList
        /// </summary>
        public void RemoveAt(int index)
        {
            ((IList<LinRow>)Rows).RemoveAt(index);
        }

        /// <summary>
        /// Реализация INotifyCollectionChanged
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        #region IXmlSerialization implementation
        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);

            Department.Value = reader.GetAttribute(nameof(Department));

            NumberLin.Value = reader.GetAttribute(nameof(NumberLin));

            XmlSerializer serializer = new XmlSerializer(typeof(LinRow));

            if (!reader.ReadToDescendant(nameof(LinRow)))
                return;

            do
            {
                LinRow row = serializer.Deserialize(reader) as LinRow;

                if (row != null)
                    Add(row);
            }
            while (reader.ReadToNextSibling(nameof(LinRow)));
        }

        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteAttributeString(nameof(Department), Department.Value);
            writer.WriteAttributeString(nameof(NumberLin), NumberLin.Value);

            XmlSerializer serializer = new XmlSerializer(typeof(LinRow));

            foreach (LinRow row in this)
            {
                serializer.Serialize(writer, row);
            }
        }
        #endregion

        //void IProcessToDatabase.ProcessToDatabase()
        //{
        //    KtcDataBase.AddLinDocumentToDataBase(this);
        //}

        /// <summary>
        /// Возвращает строку представляющую данный объект (тип документа)
        /// </summary>
        public override string ToString()
        {
            return "ЛИН";
        }
    }
}
