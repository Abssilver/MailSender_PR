using System.Threading;
using System.Windows;


namespace WPFTests_MVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        /*
        private void CalculateResultButton_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                var result = GetResultHard();
                //Application.Current.Dispatcher.Invoke(() => ResultText.Text = result);
                UpdateResultValue(result);
            }
            )
            {
                IsBackground = true
            }.Start();
            
        }
        private void UpdateResultValue(string result)
        {
            if (Dispatcher.CheckAccess())
            {
                ResultText.Text = result;
            }
            else
            {
                Dispatcher.Invoke(() => UpdateResultValue(result));
            }
        }

        private string GetResultHard()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(10);
            }
            return "Done!";
        }
        */
    }
}
