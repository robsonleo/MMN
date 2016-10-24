using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Абстракная модель представления рабочей области(вкладки)
    /// </summary>
    public abstract class WorkSpaceViewModel : ValidatedViewModelBase
    {
        /// <summary>
        /// Ссылка на главную модель представления
        /// </summary>
        //public MainWindowViewModel Parent { get; set; }
        /// <summary>
        /// Событие закрытмя рабочей области
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// Виртуальный метод обработки вызова закрытия вкладки
        /// </summary>
        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Статус режима только для чтения (заглушка => всегда нет)
        /// </summary>
        public override bool IsReadOnly => false;

        /// <summary>
        /// Закрытие вкладки
        /// </summary>
        public void Close()
        {
            //Parent?.Remove(this);
            OnClosed();
        }
    }
}
