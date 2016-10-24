using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace antonov.GUI.UsersControls.ControlMainPanel
{
    public class MainPanelButton:Button
    {
        public string UriSource { get; set; }
        public string Caption { get; set; }
        public ImageSource PictureSource { get; set; }
    }
}
