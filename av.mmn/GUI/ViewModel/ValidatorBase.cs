using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Абстрактный базовый класс Механизма проверки 
    /// </summary>
    public abstract class ValidatorBase
    {
        private class PreparatoryStateClass : ValidatorStateBase
        {
            public PreparatoryStateClass(ValidatorBase context) : base(context)
            {

            }

            public override void Validate()
            {
                Context.Field.IsValid = string.IsNullOrEmpty(Context.Field.Value) && Context.Field.AllowEmpty
                    ? (bool?)null
                    : true;

                foreach (FieldViewModelBase field in Context.AttachedFields)
                {
                    field.Validator.Reset();
                    field.RefreshIsValid();
                }

                Context.State = Context.FinalState;
            }
        }

        private class FinalStateClass : ValidatorStateBase
        {
            public FinalStateClass(ValidatorBase context) : base(context)
            {
            }

            public override void Validate()
            {
                //Тупікова гілка валідації, далі перевіряти нема чого
                // ReSharper disable once RedundantJumpStatement
                return;
            }
        }

        /// <summary>
        /// Поле использущееся для проверок
        /// </summary>
        public FieldViewModelBase Field { get; }
        /// <summary>
        /// Связаные поля
        /// </summary>
        public ICollection<FieldViewModelBase> AttachedFields { get; }

        /// <summary>
        /// Запуск проверки
        /// </summary>
        public void Validate()
        {
            State.Validate();
        }

        /// <summary>
        /// Сброс состояния проверки
        /// </summary>
        protected void Reset()
        {
            Field.IsValid = null;
            State = StartState;
        }

        /// <summary>
        /// Финальное состояние
        /// </summary>
        public ValidatorStateBase FinalState { get; }
        /// <summary>
        /// Подготовительное состояние
        /// </summary>
        public ValidatorStateBase PreparatoryState { get; }

        private ValidatorStateBase _customState;
        /// <summary>
        ///Пользовательское состояние
        /// </summary>
        public ValidatorStateBase CustomState
        {
            get { return _customState; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                _customState = value;
            }
        }

        /// <summary>
        /// Начальное состояние
        /// </summary>
        public ValidatorStateBase StartState { get; protected set; }
        /// <summary>
        /// Текущее состояние
        /// </summary>
        public ValidatorStateBase State { get; internal set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected ValidatorBase(FieldViewModelBase field)
        {
            AttachedFields = new List<FieldViewModelBase>();

            FinalState = new FinalStateClass(this);
            PreparatoryState = new PreparatoryStateClass(this);
            _customState = PreparatoryState;

            Field = field;

            field.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(FieldViewModelBase.Value))
                {
                    Reset();
                }
            };
        }
    }
}
