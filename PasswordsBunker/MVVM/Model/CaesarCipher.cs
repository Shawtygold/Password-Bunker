namespace PasswordsBunker.MVVM.Model
{
    internal class CaesarCipher
    {

        //символы
        private const string alfabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@№#;$%^:&?*()_-+=/\"\"`~";

        private static string CodeEncode(string text, int k)
        {
            var fullAlfabet = alfabet;
            var letterQty = fullAlfabet.Length;
            var retVal = "";
            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                var index = fullAlfabet.IndexOf(c);
                if (index < 0)
                {
                    //если символ не найден, то добавляем его в неизменном виде
                    retVal += c.ToString();
                }
                else
                {
                    var codeIndex = (letterQty + index + k) % letterQty;
                    retVal += fullAlfabet[codeIndex];
                }
            }

            return retVal;
        }

        //шифрование пароля
        public static string Encrypt(string password, int key)
            => CodeEncode(password, key);

        //расшифровка пароля
        public static string Decrypt(string encryptedPassword, int key)
            => CodeEncode(encryptedPassword, -key);
    }
}
