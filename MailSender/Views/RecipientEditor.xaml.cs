
using System.Windows.Controls;

namespace MailSender.Views
{
    /// <summary>
    /// Логика взаимодействия для RecipientEditor.xaml
    /// </summary>
    public partial class RecipientEditor
    {
        public RecipientEditor()
        {
            InitializeComponent();
        }
        private void OnDataValidationError(object? sender, ValidationErrorEventArgs e)
        {
            var control = (Control)e.OriginalSource;
            if (e.Action == ValidationErrorEventAction.Added)
            {
                control.ToolTip = e.Error.ErrorContent.ToString();
            }
            else
            {
                control.ClearValue(ToolTipProperty);
            }
        }
    }
}
