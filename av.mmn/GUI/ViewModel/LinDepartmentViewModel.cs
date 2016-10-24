using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления Цеха в документе ЛИН
    /// </summary>
    public class LinDepartmentViewModel : FieldViewModelBase
    {
        /// <summary>
        /// Идентификатор записи(для модели)
        /// </summary>
        public short Id { get; private set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinDepartmentViewModel()
        {
            Validator = new WerkstattValidator(this,
                @"\d{1,3}"/*,
                WerkstattFlags.All*/);
        }
    }
}
