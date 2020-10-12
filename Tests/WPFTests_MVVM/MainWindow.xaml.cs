using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
        private CancellationTokenSource _readingFileCancellation;
        private async void OnOpenFileClick(object sender, RoutedEventArgs e)
        {
            //Thread id = 1 (только для id = 1)
            //await Task.Yield(); //Время на обработку
            //Thread id = 1
            var dialog = new OpenFileDialog
            {
                Title = "Выбор файла для чтения",
                Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            OpenAction.IsEnabled = false;
            CancelAction.IsEnabled = true;
            _readingFileCancellation = new CancellationTokenSource();

            var cancel = _readingFileCancellation.Token;
            IProgress<double> progress = new Progress<double>(p => ProgressInfo.Value = p);

            try
            {
                var count = await GetWordsCountInFileAsync(dialog.FileName, progress, cancel);
                Result.Text = $"Число различных слов в файле {count}";
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Операция чтения файла была отменена");
                Result.Text = "Canceled";
                progress.Report(0);
            }

            CancelAction.IsEnabled = false;
            OpenAction.IsEnabled = true;
            /*
            ProgressInfo.Dispatcher.Invoke(() =>
            {
                ProgressInfo.Value = reader.BaseStream.Position / (double)reader.BaseStream.Length;
            });
            */
        }
        private static async Task<int> GetWordsCountInFileAsync(
            string fileName, IProgress<double> progress = null, CancellationToken cancel = default)
        {
            //Thread id = 7
            //await Task.Yield(); //Время на обработку
            //Thread id = 12 или 7 или длугой

            var wordDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            cancel.ThrowIfCancellationRequested();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    cancel.ThrowIfCancellationRequested();
                    var line = await reader.ReadLineAsync().ConfigureAwait(false);
                    //ConfigureAwait(false); - требование вернуться в произвольный поток из пула потока
                    var words = line.Split(' ');
                    //Thread.Sleep(1);
                    //await Task.Delay(1);
                    foreach (var word in words)
                    {
                        if (wordDictionary.ContainsKey(word))
                        {
                            wordDictionary[word]++;
                        }
                        else
                        {
                            wordDictionary.Add(word, 1);
                        }
                    }
                    progress?.Report(reader.BaseStream.Position / (double)reader.BaseStream.Length);
                }
            }
            return wordDictionary.Count;
        }
        private void OnCancelReadingClick(object sender, RoutedEventArgs e)
        {
            _readingFileCancellation?.Cancel();
        }
    }
}
