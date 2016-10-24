using System.Collections.ObjectModel;
using antonov.GUI;

namespace antonov.GUI.UsersControls.ControlMainPanel
{
    public class ControlPanelViewModel:ViewModelBase
    {
        protected ObservableCollection<MainPanelButton> _buttonsCollection;
        public ObservableCollection<MainPanelButton> ButtonsCollection
        {
            get { return _buttonsCollection ?? (_buttonsCollection = Test()); }
        }

        protected ObservableCollection<MainPanelButton> Test()
        {
            return new ObservableCollection<MainPanelButton>();
        }
    }

}
