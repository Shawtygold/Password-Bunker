using PasswordsBunker.Core;
using PasswordsBunker.MVVM.View.Forms;
using PasswordsBunker.MVVM.ViewModel.FormsViewModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Input;

namespace PasswordsBunker;

public partial class Password
{
    public Password()
    {
        GetPasswordCommand = new RelayCommand(GetPassword);
    }

    #region Properties

    public long Id { get; set; }
    public string Image { get; set; } = null!;  //в этом свойстве будет храниться путь к изображению
    public string Title { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Password1 { get; set; } = null!;
    public string Notes { get; set; } = null!;

    #endregion

    #region Command

    //команда вывода пароля в диалоговое окно по кнопке
    [NotMapped]public ICommand GetPasswordCommand { get; set; }

    private void GetPassword(object obj)
    {
        try
        {
            //вызов окна с логином и паролем
            PasswordDialog passwordDialog = new();
            passwordDialog.DataContext = new PasswordDialogViewModel(Login, Password1, Notes);

            passwordDialog.ShowDialog();           

        }
        catch { }
    }

    #endregion
}
