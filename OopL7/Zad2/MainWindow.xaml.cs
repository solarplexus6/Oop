using System;
using System.Windows;
using System.Windows.Data;
using Zad2.DataModels;
using Zad2.Events;

namespace Zad2
{
    public partial class MainWindow
    {
        #region Properties

        public MainWindowModel ViewModel
        {
            get { return (MainWindowModel) DataContext; }
        }

        #endregion
        #region Ctors

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion
        #region Event handlers

        private void EditBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.EventPublisher.Publish(new EditUserInitEvent());
            EditPopup.IsOpen = true;
        }

        private void NewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.EventPublisher.Publish(new NewUserInitEvent());
            EditPopup.IsOpen = true;
        }

        private void PopupCancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditPopup.IsOpen = false;
        }

        private void PopupOkBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.EventPublisher.Publish(new EditUserFinishedEvent());
            EditPopup.IsOpen = false;
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is CollectionViewGroup)
            {
                ViewModel.EventPublisher.Publish(e.NewValue as CollectionViewGroup);
            }
            else
            {
                ViewModel.EventPublisher.Publish(e.NewValue as User);
            }
        }

        #endregion
        #region Overrides

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ViewModel.InitModel();
        }

        #endregion
    }
}