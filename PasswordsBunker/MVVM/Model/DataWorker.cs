using Microsoft.Win32;
using System;

namespace PasswordsBunker.MVVM.Model
{
    class DataWorker
    {      
        //получение сообщения о том, как прошло действие (добавление/редактирование/удаление)  Например: "Пароль успешно добавлен!"
        internal static string GetMessageAboutActionWithThePassword(bool wasTheAction, string msgTrue, string msgFalse)
        {
            string message;

            //если действие было совершено успешно
            if (wasTheAction == true)
            {
                message = msgTrue;
            }
            else
            {
                message = msgFalse;
            }

            return message;
        }

        //метод получающий изображение
        internal static string GetImage()
        {
            string image = "";

            try
            {
                OpenFileDialog openFileDialog = new();

                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                    image = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

            return image;
        }
    }
}
