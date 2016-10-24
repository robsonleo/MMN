using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Базововое состояние валидатора
    /// </summary>
    public abstract class ValidatorStateBase
    {
        /// <summary>
        ///  Ссылка на валидатор
        /// </summary>
        public ValidatorBase Context { get; }

        /// <summary>
        /// Метод Валидации
        /// </summary>
        public abstract void Validate();

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected ValidatorStateBase(ValidatorBase context)
        {
            Context = context;
        }
    }
}
