using PasswordsBunker.Core;
using PasswordsBunker.MVVM.Model;
using System.Windows;
using System.Windows.Input;

namespace PasswordsBunker.MVVM.ViewModel.FormsViewModel
{
    internal class PasswordActionFormViewModel : Core.ViewModel
    {
        //конструктор при добавлении пароля
        public PasswordActionFormViewModel(string action)
        {
            Action = action;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            AcceptCommand = new RelayCommand(Accept);
            AddImageCommand = new RelayCommand(AddImage);
        }

        //конструктор при редактировании пароля
        public PasswordActionFormViewModel(string action, Password password)
        {
            Action = action;

            Id = password.Id;
            Title = password.Title;
            Image = password.Image;
            Login = password.Login;
            Password1 = password.Password1;
            Notes = password.Notes;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            AcceptCommand = new RelayCommand(Accept);
            AddImageCommand = new RelayCommand(AddImage);
        }

        #region Properties

        #region Action

        private string _action;
        public string Action
        {
            get { return _action; }
            set { _action = value; OnPropertyChanged(); }
        }

        #endregion

        #region Id

        private long _id;
        public long Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        #endregion

        #region Title

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        #endregion

        #region Image

        private string _image = "pack://application:,,,/Resources/IconPassword.png"; //путь по умолчанию
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        #endregion

        #region Login

        private string _login = "";
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        #endregion

        #region Password1

        private string _password1 = "";
        public string Password1
        {
            get { return _password1; }
            set { _password1 = value; OnPropertyChanged(); }
        }

        #endregion

        #region Notes

        private string _notes = "";
        public string Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand AddImageCommand { get; set; }
        public ICommand AcceptCommand { get; set; }

        private void Close(object obj)
        {
            if(obj is Window wnd)
                wnd.Close();
        }
        private void Minimize(object obj)
        {
            if(obj is Window wnd)
                wnd.WindowState = WindowState.Minimized;
        }

        private void AddImage(object obj) => Image = DataWorker.GetImage();
        private async void Accept(object obj)
        {
            if (obj is not Window form)
                return;

            string message = Validator.InputValidation(Title, Image, Login, Password1);

            if (message == "Ok")
            {
                if (Action == "Добавление")
                {
                    //вывод сообщения о том был ли добавлен пароль
                    Message.Show(DataWorker.GetMessageAboutActionWithThePassword(wasTheAction: await Database.AddPassword(new Password()
                    {
                        Title = Title,
                        Image = Image,
                        Login = Login,
                        Password1 = Password1,
                        Notes = Notes

                    }), msgTrue: "Пароль был успешно добавлен!", msgFalse: "Произошла ошибка! Пароль не был добавлен"));

                    form.Close();
                }
                else if (Action == "Редактирование")
                {
                    //вывод сообщения о том был ли отредактирован пароль
                    Message.Show(DataWorker.GetMessageAboutActionWithThePassword(wasTheAction: await Database.EditPassword(new Password()
                    {
                        Id = Id,
                        Title = Title,
                        Image = Image,
                        Login = Login,
                        Password1 = Password1,
                        Notes = Notes

                    }), msgTrue: "Пароль был успешно отредактирован!", msgFalse: "Произошла ошибка! Пароль не был отредактирован"));

                    form.Close();
                }                
            }
            else
            {
                Message.Show(message);
            }
                        
        }


        #endregion
    }
}
