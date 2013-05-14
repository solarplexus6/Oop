using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using Zad2.DataModels;
using Zad2.Events;

namespace Zad2
{
    public class MainWindowModel : ViewModelBase
    {
        #region Private fields

        private CollectionViewSource m_Cvs;
        private bool m_EditModeNewUser;
        private User m_EditUser;
        private ObservableCollection<User> m_FilteredUsers;
        private User m_SelectedUser;
        private ObservableCollection<GroupType> m_UserTypes;
        private ObservableCollection<User> m_Users;

        #endregion
        #region Properties

        public CollectionViewSource Cvs
        {
            get { return m_Cvs; }
            set { SetProperty(ref m_Cvs, value); }
        }

        public bool EditModeNewUser
        {
            get { return m_EditModeNewUser; }
            set { SetProperty(ref m_EditModeNewUser, value); }
        }

        public User EditUser
        {
            get { return m_EditUser; }
            set { SetProperty(ref m_EditUser, value); }
        }

        public EventPublisher EventPublisher { get; private set; }

        public ObservableCollection<User> FilteredUsers
        {
            get { return m_FilteredUsers; }
            set { SetProperty(ref m_FilteredUsers, value); }
        }

        public User SelectedUser
        {
            get { return m_SelectedUser; }
            set { SetProperty(ref m_SelectedUser, value); }
        }

        public ObservableCollection<GroupType> UserTypes
        {
            get { return m_UserTypes; }
            set { SetProperty(ref m_UserTypes, value); }
        }

        public ObservableCollection<User> Users
        {
            get { return m_Users; }
            set { SetProperty(ref m_Users, value); }
        }

        #endregion
        #region Public methods

        public void InitModel()
        {
            UserTypes = new ObservableCollection<GroupType>(Enum.GetValues(typeof (GroupType)).Cast<GroupType>());

            Users = new ObservableCollection<User>
                {
                    new User
                        {
                            FirstName = "Wieslav",
                            LastName = "Dobryszczyk",
                            Address = "ul. Dziadzia",
                            BirthDate = new DateTime(1999, 10, 10),
                            Type = GroupType.Students
                        },
                    new User
                        {
                            FirstName = "Arnold",
                            LastName = "Schwarz",
                            Address = "os. Gada 2/15",
                            BirthDate = new DateTime(1923, 1, 5),
                            Type = GroupType.Students
                        },
                    new User
                        {
                            FirstName = "Michał",
                            LastName = "Michalski",
                            Address = "ul. Hoho 45/34",
                            BirthDate = new DateTime(1999, 10, 10),
                            Type = GroupType.Lecturers
                        },
                    new User
                        {
                            FirstName = "Wiktor",
                            LastName = "Zborowski",
                            Address = "os. Nara 2/15",
                            BirthDate = new DateTime(1823, 5, 23),
                            Type = GroupType.Lecturers
                        }
                };

            var cvs = new CollectionViewSource {Source = Users};
            cvs.GroupDescriptions.Add(new PropertyGroupDescription("Type"));
            Cvs = cvs;

            SelectedUser = Users.First();
            FilteredUsers = new ObservableCollection<User>(Users);
            InitEventsLogic();
        }

        #endregion
        #region Private methods

        private void InitEventsLogic()
        {
            EventPublisher = new EventPublisher();
            // klikniecie na kategorie
            EventPublisher.GetEvent<CollectionViewGroup>()
                          .Subscribe(cvg => FilteredUsers = new ObservableCollection<User>(cvg.Items.Cast<User>()));
            // klikniecie na usera
            EventPublisher.GetEvent<User>()
                          .Subscribe(user => SelectedUser = user);
            // inicjalizacja edycji usera
            EventPublisher.GetEvent<EditUserInitEvent>()
                          .Subscribe(
                              _ =>
                                  {
                                      EditUser =
                                          new User
                                              {
                                                  Address = SelectedUser.Address,
                                                  FirstName = SelectedUser.FirstName,
                                                  LastName = SelectedUser.LastName,
                                                  BirthDate = SelectedUser.BirthDate,
                                                  Type = SelectedUser.Type
                                              };
                                      EditModeNewUser = false;
                                  });
            EventPublisher.GetEvent<NewUserInitEvent>().Subscribe(_ =>
                {
                    EditUser = new User();
                    EditModeNewUser = true;
                });
            // obsluga potwierdzenia edycji
            EventPublisher.GetEvent<EditUserFinishedEvent>().Subscribe(_ =>
                {
                    User selected;
                    if (EditModeNewUser)
                    {
                        Users.Add(EditUser);
                        selected = EditUser;
                    }
                    else
                    {
                        SelectedUser.FirstName = EditUser.FirstName;
                        SelectedUser.LastName = EditUser.LastName;
                        SelectedUser.Address = EditUser.Address;
                        SelectedUser.BirthDate = EditUser.BirthDate;
                        selected = SelectedUser;
                    }

                    //refresh powoduje wynullowanie SelectedUser, why?
                    Cvs.View.Refresh();
                    FilteredUsers = new ObservableCollection<User>(Users);
                    SelectedUser = selected;
                });
        }

        #endregion
    }
}