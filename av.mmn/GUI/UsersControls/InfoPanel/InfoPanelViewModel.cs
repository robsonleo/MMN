using System;

namespace antonov.GUI.UsersControls.InfoPanel
{
    public class InfoPanelViewModel:ViewModelBase
    {
        private String _user ;
        public String User { get { return _user ?? (_user = InitialUser()); }}

        private DateTime _date;
        public DateTime Date{ get { return _date == DateTime.MinValue ? (_date = InitialDate()) : _date; }}

        private int _page;
        public int Page { get { return _page > 0 ? _page : (_page = InitialPage()); }}

        private int _pages;
        public int Pages { get { return _pages >= _page ? _pages : (_pages = InitialPages()); }}


        private string InitialUser()
        {
            return "pr074061";
        }

        private DateTime InitialDate()
        {
            return DateTime.Now;
        }

        private int InitialPage()
        {
            return 1;
        }

        private int InitialPages()
        {
            return 10;
        }
    }
}
