namespace PasswordsBunker.MVVM.Model
{
    class Validator
    {
        internal static string InputValidation(string title, string image, string login, string password)
        {
            string exception = "";
            int exceptionCount = 0;

            //если передано пустое занчение
            if (title.Trim() == "")
            {
                exception += "Введите заголовок пароля\n";
                exceptionCount++;
            }
            if (image.Trim() == "")
            {
                exception += "Выберите изображение\n";
                exceptionCount++;
            }
            if (login.Trim() == "")
            {
                exception += "Введите логин\n";
                exceptionCount++;
            }
            if (password.Trim() == "")
            {
                exception += "Введите пароль\n";
                exceptionCount++;
            }

            //если все введено правильно
            if (exceptionCount == 0)
            {
                //ок на английском языке
                return "Ok";
            }
            else
            {
                return exception;
            }
        }
    }
}
