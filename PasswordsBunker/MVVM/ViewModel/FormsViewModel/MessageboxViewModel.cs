﻿using PasswordsBunker.Core;
using System.Windows;
using System.Windows.Input;

namespace PasswordsBunker.MVVM.ViewModel.FormsViewModel
{
    class MessageboxViewModel : Core.ViewModel
    {
        public MessageboxViewModel(string message)
        {
            Message = message;

            OkCommand = new RelayCommand(Ok);
        }

        #region Properties

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }
        private void Ok(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.Close();
        }

        #endregion
    }
}
