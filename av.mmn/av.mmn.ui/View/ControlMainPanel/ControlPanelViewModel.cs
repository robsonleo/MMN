﻿using System.Collections.ObjectModel;
using av.mmn.gui.View.CustomControls;
using GalaSoft.MvvmLight;

namespace av.mmn.gui.View.ControlMainPanel
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
