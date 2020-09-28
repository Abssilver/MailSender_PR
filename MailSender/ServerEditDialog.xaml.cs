using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MailSender
{
     
    public partial class ServerEditDialog 
    {
        /// <summary>
        /// Конструктор окна - формирует внешний вид с помощью
        /// <see cref="InitializeComponent"/>
        /// Конструктор делам приватным чтобы окно можно было создать только одним
        /// из методов, объявленных внутри класса этого окна.
        /// </summary>
        public ServerEditDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик события ввода текста
        /// Блокирует ввод нечисловых данных
        /// </summary>
        private void OnPortTextInput(object Sender, TextCompositionEventArgs E)
        {
            // Если источник события - не текстовое поле ввода
            // или текст в поле ввода отсутствует, то...
            // ничего не делаем
            if (!(Sender is TextBox text_box) || text_box.Text == "") return;
            // иначе если не удалось превратить текст в число, то
            // отмечаем событие как обработанное - текст не введётся
            E.Handled = !int.TryParse(text_box.Text, out _);
        }
        /// <summary>
        /// Обработчик события кнопки
        /// Если кнопка IsCancel == true, то результатом диалога будет false
        /// </summary>
        private void OnButtonClick(object Sender, RoutedEventArgs E)
        {
            DialogResult = !((Button)E.OriginalSource).IsCancel;
            Close();
        }
        /// <summary>
        /// Метод, позволяющий отобразить диалог для редактирования данных
        /// Редактируемые параметры передаются по ссылке
        /// Если пользователь выбрал Ok, то метод возвращает true
        /// Если пользователь выбрал Cancel, то параметры не меняются.
        /// </summary>
        public static bool ShowDialog(
        string Title, ref string Name,
        ref string Address, ref int Port, ref bool UseSSL,
        ref string Description,
        ref string Login, ref string Password)
        {
            var window = new ServerEditDialog
            {
                Title = Title,
                ServerName = { Text = Name },
                ServerAddress = { Text = Address },
                ServerPort = { Text = Port.ToString() },
                ServerSSL = { IsChecked = UseSSL },
                Login = { Text = Login },
                Password = { Password = Password },
                ServerDescription = { Text = Description },
                Owner = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window.IsActive)
            };
            if (window.ShowDialog() != true) return false;

            Name = window.ServerName.Text;
            Address = window.ServerAddress.Text;
            Port = int.Parse(window.ServerPort.Text);
            Login = window.Login.Text;
            Password = window.Password.Password;

            return true;
        }
        /// <summary>
        /// Метод, позволяющий отобразить диалог создания нового сервера
        /// Редактируемые параметры передаются по ссылке
        /// Если пользователь выбрал Ok, то метод возвращает true
        /// Если пользователь выбрал Cancel, то возвращаются дефолтные значения
        /// </summary>
        public static bool Create(
        out string Name,
        out string Address,
        out int Port,
        out bool UseSSL,
        out string Description,
        out string Login,
        out string Password)
        {
            // Инициализируем переменные значениями на случай отмены операции
            Name = null;
            Address = null;
            Port = 25;
            UseSSL = false;
            Description = null;
            Login = null;
            Password = null;
            return ShowDialog("Создать сервер",
            ref Name,
            ref Address,
            ref Port,
            ref UseSSL,
            ref Description,
            ref Login,
            ref Password);
        }
    }
}