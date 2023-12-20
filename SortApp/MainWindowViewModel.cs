using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.IO;
using Microsoft.Win32;

namespace SortApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string inputNumbers;
        public string InputNumbers
        {
            get { return inputNumbers; }
            set
            {
                if (inputNumbers != value)
                {
                    inputNumbers = value;
                    OnPropertyChanged(nameof(InputNumbers));
                }
            }
        }

        private string sortingSteps;
        public string SortingSteps
        {
            get { return sortingSteps; }
            set
            {
                if (sortingSteps != value)
                {
                    sortingSteps = value;
                    OnPropertyChanged(nameof(SortingSteps));
                }
            }
        }

        private string sortedNumbers;
        public string SortedNumbers
        {
            get { return sortedNumbers; }
            set
            {
                if (sortedNumbers != value)
                {
                    sortedNumbers = value;
                    OnPropertyChanged(nameof(SortedNumbers));
                }
            }
        }

        private string gapSize;
        public string GapSize
        {
            get
            {
                return gapSize;
            }
            set
            {
                if (gapSize != value)
                {
                    gapSize = value;
                    OnPropertyChanged(nameof(GapSize));
                }
            }
        }

        public ICommand SelectionSortCommand { get; }
        public ICommand BubbleSortCommand { get; }
        public ICommand ShellaSortCommand { get; }
        public ICommand InsertSortCommand { get; }
        public ICommand QuickSortCommand { get; }
        public ICommand PyramidSortCommand { get; }
        public ICommand BrowseFileCommand { get; }

        private MainWindowModel model;

        public MainWindowViewModel()
        {

        }

        public MainWindowViewModel(MainWindowModel model)
        {
            this.model = model;
            InputNumbers = "";
            SortedNumbers = "";
            SortingSteps = "";
            GapSize = "";
            model.SortingStepsChanged += UpdateSortingSteps;
            SelectionSortCommand = new RelayCommand(SelectionSort);
            BubbleSortCommand = new RelayCommand(BubbleSort);
            ShellaSortCommand = new RelayCommand(ShellaSort);
            InsertSortCommand = new RelayCommand(InsertSort);
            QuickSortCommand = new RelayCommand(QuickSort);
            PyramidSortCommand = new RelayCommand(PyramidSort);
            BrowseFileCommand = new RelayCommand(BrowseFile);
        }

        private void UpdateSortingSteps(string steps)
        {
            SortingSteps = steps;
        }

        private void SelectionSort()
        {
            try
            {
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.SelectionSort(numbers, 0);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void BubbleSort()
        {
            try
            {
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.BubbleSort(numbers, 0);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ShellaSort()
        {
            try
            {
                int gap = Convert.ToInt32(GapSize);
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.ShellaSort(numbers,gap);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void InsertSort()
        {
            try
            {
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.InsertSort(numbers, 0);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void QuickSort()
        {
            try
            {
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.QuickSort(numbers, 0);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void PyramidSort()
        {
            try
            {
                int[] numbers = ParseInputNumbers(InputNumbers);
                int[] sortedNumbers = model.PyramidSort(numbers, 0);
                DisplaySortedNumbers(sortedNumbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private int[] ParseInputNumbers(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new Exception("Введите числа, разделенные запятыми или пробелами, или выберите файл.");
                }

                if (File.Exists(input))
                {
                    string fileContent = File.ReadAllText(input);
                    string[] stringNumbers = fileContent.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return Array.ConvertAll(stringNumbers, int.Parse);
                }
                else
                {
                    string[] stringNumbers = input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    return Array.ConvertAll(stringNumbers, int.Parse);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Некорректный ввод: {ex.Message}");
            }
        }

        private void BrowseFile()
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    InputNumbers = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе файла: {ex.Message}");
            }
        }


        private void DisplaySortedNumbers(int[] numbers)
        {
            SortedNumbers = string.Join(", ", numbers);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
