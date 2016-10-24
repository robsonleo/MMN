using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    /// <summary>
    /// Содержит набор описания полей.
    /// </summary>
    public class InfoFieldAttribute : Attribute
    {
        private string _showName;

        /// <summary>
        /// Имя поля
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Отображаемое название поля.
        /// </summary>
        public string ShowName { get { return _showName.Length > 0 ? _showName : Name; } set { _showName = value; } }

        public InfoFieldAttribute(string name, string showName = "")
        {
            Name = name;
            ShowName = showName;

        }
    }
}
