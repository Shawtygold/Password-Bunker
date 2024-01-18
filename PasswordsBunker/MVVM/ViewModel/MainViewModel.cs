using PasswordsBunker.Core;
using PasswordsBunker.Services;
using System.Windows;
using System.Windows.Input;

namespace PasswordsBunker.MVVM.ViewModel
{
    class MainViewModel : Core.ViewModel
    {
        public MainViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);

            Navigation.NavigateTo<PasswordsViewModel>();
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }

        private void Close(object obj) => Application.Current.Shutdown();
        private void Minimize(object obj) => Application.Current.MainWindow.WindowState = WindowState.Minimized;

        #endregion

        #region Methods

        //private void NavigateToPasswords()
        //{
        //    //вызов экрана загрузки
        //    Navigation.NavigateTo<LoadingScreenViewModel>();

        //    //создание фонового потока
        //    Thread thread = new(LoadPasswords);
        //    thread.Start();
        //}

        //private void LoadPasswords()
        //{
        //    //получение и передача паролей
        //    PasswordsViewModel.StaticPasswords = DataWorker.GetPasswords();

        //    Navigation.NavigateTo<PasswordsViewModel>();
        //}

        #endregion
    }
}
