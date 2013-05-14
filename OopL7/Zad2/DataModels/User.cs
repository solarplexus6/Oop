using System;

namespace Zad2.DataModels
{
    public class User : ViewModelBase
    {
        #region Private fields

        private string m_Address;
        private DateTime m_BirthDate;
        private string m_FirstName;

        private string m_LastName;
        private GroupType m_Type;

        #endregion
        #region Properties

        public string Address
        {
            get { return m_Address; }
            set { SetProperty(ref m_Address, value); }
        }

        public DateTime BirthDate
        {
            get { return m_BirthDate; }
            set { SetProperty(ref m_BirthDate, value); }
        }

        public string FirstName
        {
            get { return m_FirstName; }
            set { SetProperty(ref m_FirstName, value); }
        }

        public string LastName
        {
            get { return m_LastName; }
            set { SetProperty(ref m_LastName, value); }
        }

        public GroupType Type
        {
            get { return m_Type; }
            set { SetProperty(ref m_Type, value); }
        }

        #endregion
    }
}