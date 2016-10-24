using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Xsl;
using GalaSoft.MvvmLight.Command;
using GUI.Interfaces;

namespace GUI.ViewModel
{
    /// <summary>
    /// Абстрактная модель представления строки
    /// </summary>
    public abstract class RowViewModelBase : ValidatedViewModelBase, IFieldContainer, IXmlSerializable
    {
        /// <summary>
        /// Реализация IFieldContainer
        /// </summary>
        public ObservableCollection<FieldViewModelBase> FieldList { get; } =
            new ObservableCollection<FieldViewModelBase>();

        /// <summary>
        /// Регистрация вычесляемых полей
        /// </summary>
        protected override void RegisteDependencies()
        {
            //IsValid
            RegisterElementPropertyDependencies(nameof(IsValid), this, new[] {nameof(FieldList)}, () =>
                RegisterCollectionPropertyDependencies(nameof(IsValid), FieldList, new[] {nameof(IsValid)}));
            //IsChanged
            RegisterElementPropertyDependencies(nameof(IsChanged), this, new[] {nameof(FieldList)}, () =>
                RegisterCollectionPropertyDependencies(nameof(IsChanged), FieldList, new[] {nameof(IsChanged)}));
        }

        /// <summary>
        /// Делегат для подій RowViewModelBase
        /// </summary>
        /// <param name="sender">Об'єкт, що викликає подію</param>
        /// <param name="e">Параметри</param>
        public delegate void RowViewModelDelegate(object sender, EventArgs e);

        /// <summary>
        /// КС строки
        /// </summary>
        public abstract KcViewModel Kc { get; protected set; }

        /// <summary>
        /// Подія втрати фокусу рядком
        /// </summary>
        public event RowViewModelDelegate LostFocusEvent;

        /// <summary>
        /// Подія отримання фокусу рядком
        /// </summary>
        public event RowViewModelDelegate GotFocusEvent;

        #region Commands

        private RelayCommand _lostFocusCommand;

        /// <summary>
        /// Команда втрати фокусу рядком
        /// </summary>
        public virtual ICommand LostFocus
        {
            get
            {
                return _lostFocusCommand ??
                       (_lostFocusCommand = new RelayCommand(
                           () => { LostFocusEvent?.Invoke(this, new EventArgs()); }));
            }
        }

        private RelayCommand _gotFucosCommand;

        /// <summary>
        /// Команда отримання фокусу рядком
        /// </summary>
        public virtual ICommand GotFocus
        {
            get
            {
                return _gotFucosCommand ??
                       (_gotFucosCommand = new RelayCommand(() => GotFocusEvent?.Invoke(this, new EventArgs())));
            }
        }

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Внутреняя команда вставки
        /// </summary>
        protected RelayCommand _insertRowCommand;

        /// <summary>
        /// Команда вставки строки
        /// </summary>
        public ICommand InsertRowCommand => _insertRowCommand;

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Внутреняя команда удаления
        /// </summary>
        protected RelayCommand _deleteRowCommand;

        /// <summary>
        /// Команда удаления  
        /// </summary>
        public ICommand DeleteRowCommand => _deleteRowCommand;

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Внутреняя комманда дублирования
        /// </summary>
        protected RelayCommand _dublicateRowCommand;

        /// <summary>
        /// Комманда дублирования
        /// </summary>
        public ICommand DublicateRowCommand => _dublicateRowCommand;

        #endregion

        /// <summary>
        /// Статус соответсвия строки  всем правилам заполнения
        /// </summary>
        public override bool? IsValid => FieldList.All(field => field.IsValid != false);

        /// <summary>
        /// Метод обновления поля IsValid
        /// </summary>
        public override void RefreshIsValid()
        {
            foreach (FieldViewModelBase field in FieldList)
                field.RefreshIsValid();
        }

        /// <summary>
        /// Стуатус изменения строки
        /// </summary>
        public override bool IsChanged => base.IsChanged || FieldList.Any(field => field.IsChanged);

        /// <summary>
        /// Сброси изменений
        /// </summary>
        public override void RefreshChanges()
        {
            foreach (var fieldViewModelBase in FieldList) fieldViewModelBase.RefreshChanges();
        }

        /// <summary>
        /// Признак последней строки
        /// </summary>
        public abstract bool IsLast { get; }

        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Виртуальная реализация IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public virtual void ReadXml(XmlReader reader)
        {
            Kc.Value = reader.GetAttribute(nameof(Kc));
        }

        /// <summary>
        /// Виртуальная реализация IXmlSerializable
        /// </summary>
        /// <returns></returns>
        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Kc), Kc.Value);
        }
    }
}
