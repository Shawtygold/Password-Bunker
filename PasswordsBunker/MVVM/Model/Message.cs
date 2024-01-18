using PasswordsBunker.MVVM.View.Forms;
using PasswordsBunker.MVVM.ViewModel.FormsViewModel;

namespace PasswordsBunker.MVVM.Model
{
    internal class Message
    {
        internal static void Show(string message)
        {
            Messagebox msgBox = new();
            msgBox.DataContext = new MessageboxViewModel(message);

            msgBox.Show();
        }
    }
}
