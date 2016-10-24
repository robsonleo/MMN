using System.Windows.Controls;
using System.Windows.Media;

namespace av.mmn.gui.View.CustomControls
{
    public class MainPanelButton:Button
    {
        public string UriSource { get; set; }
        public string Caption { get; set; }
        public ImageSource PictureSource { get; set; }
    }
}
