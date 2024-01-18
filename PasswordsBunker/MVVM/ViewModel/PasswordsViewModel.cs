using PasswordsBunker.Core;
using PasswordsBunker.MVVM.Model;
using PasswordsBunker.MVVM.View.Forms;
using PasswordsBunker.MVVM.ViewModel.FormsViewModel;
using PasswordsBunker.Services;
using System.Collections.Generic;
using System.Windows.Input;

namespace PasswordsBunker.MVVM.ViewModel
{
    class PasswordsViewModel : Core.ViewModel
    {
        public PasswordsViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);

            Passwords = Database.GetPasswords();

            //if(LoadPasswords() == 0)
            //{
            //    OpacityNoPasswords = 1;
            //    OpacityPasswords = 0;
            //}  
        }

        #region Properties

        #region Navigation
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }
        #endregion

        #region Passwords

        private List<Password> _passwords = new();
        public List<Password> Passwords
        {
            get { return _passwords; }
            set { _passwords = value; OnPropertyChanged(); }
        }

        #endregion

        #region SelectedItem

        private Password _selectedItem;
        public Password SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        #endregion

        #region OpacityNoPasswords

        private int _opacityNoPasswords = 0;
        public int OpacityNoPasswords
        {
            get { return _opacityNoPasswords; }
            set { _opacityNoPasswords = value; OnPropertyChanged(); }
        }

        #endregion

        #region OpacityPasswords

        private int _opacityPasswords = 1;

        public int OpacityPasswords
        {
            get { return _opacityPasswords; }
            set { _opacityPasswords = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        private void Add(object obj)
        {
            PasswordActionForm form = new();
            form.DataContext = new PasswordActionFormViewModel("Добавление");

            form.ShowDialog();

            //если есть пароли в базе данных
            if(UpdatePasswords() != 0)
            {
                OpacityPasswords = 1;
                OpacityNoPasswords = 0;
            }
        }
        private void Edit(object obj)
        {
            if (SelectedItem != null)
            {
                PasswordActionForm form = new();
                form.DataContext = new PasswordActionFormViewModel("Редактирование", SelectedItem);

                form.ShowDialog();

                //при редактировании в любом случае будут пароли в базе данных
                //нет смысла прописывать изменения для OpacityPassword и OpacityNoPassword
                if (UpdatePasswords() != 0)
                {
                    return;
                }
            }
            else
            {
                Message.Show("Выберите пароль для изменения!");
            }
        }
        private async void Delete(object obj)
        {
            if(SelectedItem != null)
            {
                //вывод сообщения о том был ли удален пароль
                Message.Show(DataWorker.GetMessageAboutActionWithThePassword(wasTheAction: await Database.DeletePassword(SelectedItem),
                    msgTrue: "Пароль был успешно удален!",
                    msgFalse: "Произошла ошибка! Пароль не был удален"));
            }
            else
            {
                Message.Show("Выберите пароль для удаления!");
            }

            //если есть пароли в базе данных
            if (UpdatePasswords() != 0)
            {
                //изменение видимости надписи "нет паролей"
                OpacityPasswords = 1;
                OpacityNoPasswords = 0;
            }
            else if(UpdatePasswords() == 0)
            {
                //изменение видимости надписи "нет паролей"
                OpacityPasswords = 0;
                OpacityNoPasswords = 1;
            }
        }

        #endregion

        #region Methods

        //обновление паролей
        private int UpdatePasswords()
        {
            Passwords = Database.GetPasswords();

            return Passwords.Count;
        }

        #endregion
    }
}
