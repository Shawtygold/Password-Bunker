using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordsBunker.MVVM.Model
{
    class Database
    {
        //ключ для шифрования пароля
        private const int key = 4;

        internal static List<Password> GetPasswords()
        {
            //список с паролями
            List<Password> passwords = new();

            try
            {
                using PasswordsDbContext db = new();
                //получение паролей
                passwords = db.Passwords.ToList();
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

            for (int i = 0; i < passwords.Count; i++)
            {
                //расшифровка логина каждого из элементов списка
                passwords[i].Login = CaesarCipher.Decrypt(passwords[i].Login, key);

                //расшифровка пароля каждого из элементов списка
                passwords[i].Password1 = CaesarCipher.Decrypt(passwords[i].Password1, key);

                //расшифровка описания пароля каждого из элементов списка
                passwords[i].Notes = CaesarCipher.Decrypt(passwords[i].Notes, key);
            }

            return passwords;
        }

        //метод добавления пароля
        internal async static Task<bool> AddPassword(Password password)
        {
            if (password == null)
                return false;

            bool result = false;

            //шифрование логина
            password.Login = CaesarCipher.Encrypt(password.Login, key);

            //шифрование пароля
            password.Password1 = CaesarCipher.Encrypt(password.Password1, key);

            //шифрование описания
            password.Notes = CaesarCipher.Encrypt(password.Notes, key);

            try
            {
                //добавление пароля в базу данных
                using PasswordsDbContext db = new();

                await db.Passwords.AddAsync(password);
                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

            return result;
        }

        //метод редактирования пароля
        internal async static Task<bool> EditPassword(Password password)
        {
            if (password == null)
                return false;

            bool result = false;

            //шифрование логина
            password.Login = CaesarCipher.Encrypt(password.Login, key);

            //шифрование пароля
            password.Password1 = CaesarCipher.Encrypt(password.Password1, key);

            //шифрование описания
            password.Notes = CaesarCipher.Encrypt(password.Notes, key);

            try
            {
                using PasswordsDbContext db = new();

                db.Update(password);
                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

            return result;
        }

        //метод удаления пароля
        internal async static Task<bool> DeletePassword(Password password)
        {
            if (password == null)
                return false;

            bool result = false;

            try
            {
                using (PasswordsDbContext db = new())
                {
                    await Task.Run(() => db.Remove(password));
                    await db.SaveChangesAsync();
                }

                result = true;
            }
            catch (Exception ex)
            {
                Message.Show(ex.Message);
            }

            return result;
        }
    }
}
