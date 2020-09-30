using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using WPFTests_MVVM.Infrastructure.Commands;
using WPFTests_MVVM.ViewModels.Base;

namespace WPFTests_MVVM.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _title = "Тестовое окно";
        private readonly Timer _timer;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
            /*
            set
            {
                if (_Title == value) return;
                _Title = value;
                //OnPropertyChanged(nameof(Title));
                OnPropertyChanged();
            }
            */
        }
        public DateTime CurrentTime => DateTime.Now;

        private bool _timerEnabled = true;

        public bool TimerEnabled
        {
            get => _timerEnabled;
            set
            {
                if (!Set(ref _timerEnabled, value)) return;
                _timer.Enabled = value;
            }
        }
        private ICommand _showDialogueCommand;
        public ICommand ShowDialogueCommand => _showDialogueCommand ?? new LambdaCommand(OnShowDialogueCommandExecuted);
        public MainWindowViewModel()
        {
            _timer = new Timer(100);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }
        private void OnShowDialogueCommandExecuted(object p)
        {
            MessageBox.Show("Wow!");
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(CurrentTime));
        }
    }
}
