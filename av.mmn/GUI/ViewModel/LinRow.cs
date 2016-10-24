using System;
using System.Xml;
using GalaSoft.MvvmLight.Command;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления строки ЛИН
    /// </summary>
    public sealed class LinRow : RowViewModelBase, IContainsSeries
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinRow()
        {
            Pkp = new LinPkpViewModel()
            {
                Name = "ПКП",
                Value = string.Empty,
                AllowEmpty = true,
                IsValid = null,
                Row = this,
            };

            Kc = new KcViewModel()
            {
                Name = "Обозначение",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Mass = new NumericFieldViewModel()
            {
                Name = "Масса",
                Value = string.Empty,
                AllowFloat = true,
                AllowEmpty = true,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Customer = new LinDepartmentViewModel()
            {
                Name = "Цех потребитель",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Chipher = new MaterialChiperViewModel()
            {
                Name = "Шифр материала",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Waste = new LinWasteAttribute()
            {
                Name = "Признак отхода",
                Value = string.Empty,
                AllowEmpty = true,
                IsValid = null,
                Row = this,
            };

            MaterialSize = new LinSizeViewModel()
            {
                Name = "Размер заготовки",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Norma = new NumericFieldViewModel()
            {
                Name = "Норма расхода",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            Measure = new UnitMeasureViewModel()
            {
                Name = "Единица измерения",
                Value = string.Empty,
                AllowEmpty = false,
                IsValid = false,
                Row = this,
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY)
            };

            PartQuantity = new NumericFieldViewModel()
            {
                Name = "Количество деталей в заготовке",
                AllowFloat = false,
                AllowEmpty = true,
                IsValid = null,
                Row = this,
                Value = string.Empty
            };

            SampleQuantity = new NumericFieldViewModel()
            {
                Name = "Количество деталей в образце",
                AllowFloat = false,
                AllowEmpty = true,
                IsValid = null,
                Row = this,
                Value = string.Empty
            };

            Chpi = new NumericFieldViewModel()
            {
                Name = "ШПИ",
                Value = string.Empty,
                AllowFloat = false,
                AllowEmpty = true,
                IsValid = null,
                Row = this
            };

            ProductChipher = new ProductCipherViewModel()
            {
                Name = "Шифр изделия",
                Value = string.Empty,
                AllowEmpty = true,
                IsValid = null,
                Row = this
            };

            SeriesFrom = new SeriesViewModel()
            {
                Name = "Серия С",
                Value = string.Empty,
                AllowEmpty = true,
                IsValid = null,
                Row = this,
            };

            SeriesTo = new SeriesViewModel()
            {
                Name = "Серия ПО",
                Value = string.Empty,
                AllowEmpty = true,
                IsValid = null,
                Row = this
            };

            Array.ForEach(
                new FieldViewModelBase[]
                {
                    Pkp,
                    Kc,
                    Mass,
                    Customer,
                    Chipher,
                    Waste,
                    MaterialSize,
                    Norma,
                    Measure,
                    PartQuantity,
                    SampleQuantity,
                    Chpi,
                    ProductChipher,
                    SeriesFrom,
                    SeriesTo
                }, item => FieldList.Add(item));

            Pkp.Validator.CustomState = new PkpValidatorState(Pkp.Validator);
            Pkp.Validator.AttachedFields.Add(Norma);
            Pkp.Validator.AttachedFields.Add(MaterialSize);
            Pkp.Validator.AttachedFields.Add(Mass);

            Mass.Validator.CustomState = new MassValidationState(Mass.Validator);
            Mass.Validator.AttachedFields.Add(Norma);

            Norma.Validator.CustomState = new NormaValidationState(Norma.Validator);


            ProductChipher.Validator.CustomState = new ProductCipherValidatorState(ProductChipher.Validator);
            ProductChipher.Validator.AttachedFields.Add(SeriesFrom);
            ProductChipher.Validator.AttachedFields.Add(SeriesTo);

            SeriesFrom.Validator.CustomState = new SerieFromValidationState(SeriesFrom.Validator) { ProductChipher = ProductChipher };
            SeriesFrom.Validator.AttachedFields.Add(SeriesTo);

            SeriesTo.Validator.CustomState = new SerieToValidationState(SeriesTo.Validator) { ProductChipher = ProductChipher };

            Chpi.Validator.CustomState = new ChpiValidationState(Chpi.Validator);

            _insertRowCommand = new RelayCommand(() => Document.Add(new LinRow()), () => !IsReadOnly);
            _deleteRowCommand = new RelayCommand(() => Document.Remove(this), () => !IsReadOnly);
            _dublicateRowCommand = new RelayCommand(() => Document.Add(new LinRow(this)), () => !IsReadOnly);
            RegisteDependencies();
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="row">Объект для копирования</param>
        private LinRow(LinRow row) : this()
        {
            Pkp.Value = row.Pkp.Value;
            Kc.Value = row.Kc.Value;
            Mass.Value = row.Mass.Value;
            Customer.Value = row.Customer.Value;
            Chipher.Value = row.Chipher.Value;
            Waste.Value = row.Waste.Value;
            MaterialSize.Value = row.MaterialSize.Value;
            Norma.Value = row.Norma.Value;
            Measure.Value = row.Measure.Value;
            PartQuantity.Value = row.PartQuantity.Value;
            SampleQuantity.Value = row.SampleQuantity.Value;
            Chpi.Value = row.Chpi.Value;
            ProductChipher.Value = row.ProductChipher.Value;
            SeriesFrom.Value = row.SeriesFrom.Value;
            SeriesTo.Value = row.SeriesTo.Value;
        }

        private class PkpValidatorState : ValidatorStateBase
        {
            public PkpValidatorState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                LinRow _this = (LinRow)Context.Field.Row;

                _this.MaterialSize.AllowEmpty = !string.IsNullOrEmpty(_this.Pkp.Value);
                _this.Mass.AllowEmpty = !string.IsNullOrEmpty(_this.Pkp.Value);

                Context.State = Context.PreparatoryState;
                Context.Validate();
            }
        }

        private class MassValidationState : ValidatorStateBase
        {
            public MassValidationState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                LinRow _this = (LinRow)Context.Field.Row;

                if (_this.Mass.DecimalValue != null && _this.Mass.DecimalValue.Value < 0.0000001m)
                {
                    _this.Mass.IsValid = false;
                    _this.Mass.ErrorInfo = new ErrorInfoViewModel(ErrorType.OTHER, "Масса детали недопустимо мала", "Масса меньше 0,1 мг");
                    return;
                }

                Context.State = Context.PreparatoryState;
                Context.Validate();
            }
        }

        private class NormaValidationState : ValidatorStateBase
        {
            public NormaValidationState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                LinRow _this = (LinRow)Context.Field.Row;

                if (_this.Pkp.Value != "A")
                {
                    if (_this.Pkp.Value == string.Empty)
                    {
                        if (_this.Mass.IsValid != true)
                        {
                            Context.Field.IsValid = false;
                            Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY, "Для проверки нормы необходимо ввести массу детали");
                            return;
                        }

                        if (_this.Norma.DecimalValue < _this.Mass.DecimalValue)
                        {
                            Context.Field.IsValid = false;
                            Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.QUANTITIES_UNSUGGESTION, "Норма основного материала меньше чем масса детали");
                            return;
                        }
                    }
                }

                Context.State = Context.PreparatoryState;
                Context.Validate();
            }
        }

        private class ProductCipherValidatorState : ValidatorStateBase
        {
            public ProductCipherValidatorState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                LinRow _this = (LinRow)Context.Field.Row;

                _this.SeriesFrom.AllowEmpty = string.IsNullOrEmpty(_this.ProductChipher.Value);

                Context.State = Context.PreparatoryState;
                Context.Validate();
            }
        }

        private class ChpiValidationState : ValidatorStateBase
        {
            public ChpiValidationState(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                LinRow _this = (LinRow)Context.Field.Row;

                if (_this.Chpi.DecimalValue != 1)
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_DIRECTORY_NOT_FOUND, "Поле не принимает значения кроме \"1\"", "Недопустимое значение поля", "Повторная проверка значения поля");
                }

                Context.State = Context.PreparatoryState;
                Context.Validate();
            }
        }

        /// <summary>
        /// Родительский документ ЛИН
        /// </summary>
        public LinDocumentViewModel Document
        {
            get { return _document; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (_document != null)
                    throw new InvalidOperationException("Неможливо повторно задати документ для рядку");

                _document = value;
            }
        }
        private LinDocumentViewModel _document;

        /// <summary>
        /// ПКП
        /// </summary>
        public LinPkpViewModel Pkp { get; }
        /// <summary>
        /// КС
        /// </summary>
        public override KcViewModel Kc { get; protected set; }
        /// <summary>
        /// Масса
        /// </summary>
        public NumericFieldViewModel Mass { get; }
        /// <summary>
        /// Цех потребитель
        /// </summary>
        public LinDepartmentViewModel Customer { get; }
        /// <summary>
        /// Шифр материала
        /// </summary>
        public MaterialChiperViewModel Chipher { get; }
        /// <summary>
        /// Признак "Из отхода"
        /// </summary>
        public LinWasteAttribute Waste { get; }
        /// <summary>
        /// Размер заготовки
        /// </summary>
        public LinSizeViewModel MaterialSize { get; }
        /// <summary>
        /// Норма
        /// </summary>
        public NumericFieldViewModel Norma { get; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public UnitMeasureViewModel Measure { get; }
        /// <summary>
        /// Количество на заготовку
        /// </summary>
        public NumericFieldViewModel PartQuantity { get; }
        /// <summary>
        /// Количество образцов
        /// </summary>
        public NumericFieldViewModel SampleQuantity { get; }
        /// <summary>
        /// Шифр причиныизменения
        /// </summary>
        public NumericFieldViewModel Chpi { get; }
        /// <summary>
        /// Шифр изделия
        /// </summary>
        public ProductCipherViewModel ProductChipher { get; }
        /// <summary>
        /// Серия С
        /// </summary>
        public SeriesViewModel SeriesFrom { get; }
        /// <summary>
        /// Серия ПО
        /// </summary>
        public SeriesViewModel SeriesTo { get; }

        /// <summary>
        /// Является ли эта строка последней
        /// </summary>
        public override bool IsLast => Document[Document.Count - 1] == this;

        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        public override void ReadXml(XmlReader reader)
        {
            base.ReadXml(reader);

            Pkp.Value = reader.GetAttribute(nameof(Pkp));
            Mass.Value = reader.GetAttribute(nameof(Mass));
            Customer.Value = reader.GetAttribute(nameof(Customer));
            Chipher.Value = reader.GetAttribute(nameof(Chipher));
            Waste.Value = reader.GetAttribute(nameof(Waste));
            MaterialSize.Value = reader.GetAttribute(nameof(MaterialSize));
            Norma.Value = reader.GetAttribute(nameof(Norma));
            Measure.Value = reader.GetAttribute(nameof(Measure));
            PartQuantity.Value = reader.GetAttribute(nameof(PartQuantity));
            SampleQuantity.Value = reader.GetAttribute(nameof(SampleQuantity));
            Chpi.Value = reader.GetAttribute(nameof(Chpi));
            ProductChipher.Value = reader.GetAttribute(nameof(ProductChipher));
            SeriesFrom.Value = reader.GetAttribute(nameof(SeriesFrom));
            SeriesTo.Value = reader.GetAttribute(nameof(SeriesTo));
        }

        /// <summary>
        /// Реализация IXmlSerializable
        /// </summary>
        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);

            writer.WriteAttributeString(nameof(Pkp), Pkp.Value);
            writer.WriteAttributeString(nameof(Mass), Mass.Value);
            writer.WriteAttributeString(nameof(Customer), Customer.Value);
            writer.WriteAttributeString(nameof(Chipher), Chipher.Value);
            writer.WriteAttributeString(nameof(Waste), Waste.Value);
            writer.WriteAttributeString(nameof(MaterialSize), MaterialSize.Value);
            writer.WriteAttributeString(nameof(Norma), Norma.Value);
            writer.WriteAttributeString(nameof(Measure), Measure.Value);
            writer.WriteAttributeString(nameof(PartQuantity), PartQuantity.Value);
            writer.WriteAttributeString(nameof(SampleQuantity), SampleQuantity.Value);
            writer.WriteAttributeString(nameof(Chpi), Chpi.Value);
            writer.WriteAttributeString(nameof(ProductChipher), ProductChipher.Value);
            writer.WriteAttributeString(nameof(SeriesFrom), SeriesFrom.Value);
            writer.WriteAttributeString(nameof(SeriesTo), SeriesTo.Value);
        }

        /// <summary>
        /// Признак режима "только для чтения"
        /// </summary>
        public override bool IsReadOnly => Document.IsReadOnly;
    }
}
