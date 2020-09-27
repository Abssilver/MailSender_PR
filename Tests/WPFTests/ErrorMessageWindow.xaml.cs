using System;
using System.Collections.Generic;
using System.Windows;

namespace WPFTests
{
    public partial class ErrorMessageWindow
    {
        public ErrorMessageWindow(string msgError)
        {
            InitializeComponent();
            SetupErrorMesssage(msgError);
        }
        private void SetupErrorMesssage(string msg) => lDisplayError.Content = msg;

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
