using System;
using System.Diagnostics;
using System.Text;

namespace SortApp
{
    public class MainWindowModel
    {
        private int step = 1;
        private double totalTime = 0;
        private double timeStamp = 0;

        public event Action<string> SortingStepsChanged;

        private void NotifySortingStepsChanged(string steps)
        {
            SortingStepsChanged?.Invoke(steps);
        }

        private void PerformSort(int[] numbers, int gap, string sortName, Action<int[], int> sortMethod)
        {
            try
            {
                Console.WriteLine($"{sortName} started");
                step = 1;
                totalTime = 0;
                sortMethod(numbers, gap);
                Console.WriteLine($"{sortName} completed");

            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка в {sortName}: {ex.Message}");
            }
        }

        public int[] SelectionSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "SelectionSort", SelectionSortMethod);
            return numbers;
        }

        public int[] BubbleSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "BubbleSort", BubbleSortMethod);
            return numbers;
        }

        public int[] ShellaSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "ShellSort", ShellSortMethod);
            return numbers;
        }

        public int[] InsertSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "InsertionSort", InsertionSortMethod);
            return numbers;
        }

        public int[] QuickSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "QuickSort", QuickSortMethod);
            return numbers;
        }

        public int[] PyramidSort(int[] numbers, int gap)
        {
            PerformSort(numbers, gap, "PyramidSort", PyramidSortMethod);
            return numbers;
        }

        private void SelectionSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numbers.Length - 1; i++)
            {

                string previousState = string.Join(", ", numbers);

                int minIndex = i;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] < numbers[minIndex])
                    {
                        minIndex = j;
                    }
                }

                if (minIndex != i)
                {
                    int temp = numbers[i];
                    numbers[i] = numbers[minIndex];
                    numbers[minIndex] = temp;
                }

                string currentState = string.Join(", ", numbers);

                if (currentState != previousState)
                {
                    timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                    totalTime += timeStamp;
                    steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");

                }
            }
            stopwatch.Stop();
            NotifySortingStepsChanged(steps.ToString());
        }

        private void BubbleSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool swapped;

            do
            {
                swapped = false;

                string previousState = string.Join(", ", numbers);

                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    if (numbers[i] > numbers[i + 1])
                    {
                        int temp = numbers[i];
                        numbers[i] = numbers[i + 1];
                        numbers[i + 1] = temp;
                        swapped = true;
                    }
                }

                string currentState = string.Join(", ", numbers);

                if (currentState != previousState)
                {
                    timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                    totalTime += timeStamp;
                    steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");

                }

            } while (swapped);
            stopwatch.Stop();
            NotifySortingStepsChanged(steps.ToString());
        }

        private void ShellSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = numbers.Length;

            for (int h = gap; h > 0; h /= 2)
            {
                for (int i = h; i < n; i += 1)
                {
                    string previousState = string.Join(", ", numbers);

                    int temp = numbers[i];
                    int j;

                    for (j = i; j >= h && numbers[j - h] > temp; j -= h)
                    {
                        numbers[j] = numbers[j - h];
                    }

                    numbers[j] = temp;

                    string currentState = string.Join(", ", numbers);

                    if (currentState != previousState)
                    {
                        timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                        totalTime += timeStamp;
                        steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");

                    }
                }
            }
            stopwatch.Stop();
            NotifySortingStepsChanged(steps.ToString());
        }

        private void InsertionSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 1; i < numbers.Length; ++i)
            {
                string previousState = string.Join(", ", numbers);

                int key = numbers[i];
                int j = i - 1;

                while (j >= 0 && numbers[j] > key)
                {
                    numbers[j + 1] = numbers[j];
                    j = j - 1;
                }

                numbers[j + 1] = key;

                string currentState = string.Join(", ", numbers);

                if (currentState != previousState)
                {
                    timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                    totalTime += timeStamp;
                    steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");

                }
            }
            stopwatch.Stop();
            NotifySortingStepsChanged(steps.ToString());
        }

        private void QuickSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();

            QuickSortRecursive(numbers, 0, numbers.Length - 1, steps);

            NotifySortingStepsChanged(steps.ToString());
        }

        private void QuickSortRecursive(int[] numbers, int low, int high, StringBuilder steps)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            if (low < high)
            {
                int pi = Partition(numbers, low, high);

                string currentState = string.Join(", ", numbers);

                if (!steps.ToString().Contains(currentState))
                {
                    timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                    totalTime += timeStamp;
                    steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");
                }

                QuickSortRecursive(numbers, low, pi - 1, steps);
                QuickSortRecursive(numbers, pi + 1, high, steps);
            }
            stopwatch.Stop();
        }

        private int Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (numbers[j] < pivot)
                {
                    i++;

                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }

            int temp1 = numbers[i + 1];
            numbers[i + 1] = numbers[high];
            numbers[high] = temp1;

            return i + 1;
        }

        private void PyramidSortMethod(int[] numbers, int gap)
        {
            if (numbers.Length == 0)
            {
                return;
            }

            StringBuilder steps = new StringBuilder();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {

                int n = numbers.Length;

                for (int i = n / 2 - 1; i >= 0; i--)
                {
                    Heapify(numbers, n, i);
                }

                for (int i = n - 1; i >= 0; i--)
                {
                    string previousState = string.Join(", ", numbers);
                    int temp = numbers[0];
                    numbers[0] = numbers[i];
                    numbers[i] = temp;

                    Heapify(numbers, i, 0);

                    string currentState = string.Join(", ", numbers);

                    if (currentState != previousState)
                    {
                        timeStamp = stopwatch.Elapsed.TotalMilliseconds;
                        totalTime += timeStamp;
                        steps.AppendLine($"Шаг {step++}: {currentState}, Время: {timeStamp:f4} мс, СумВремя: {totalTime:f4}\n");

                    }
                }

                Console.WriteLine($"PyramidSort completed");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка в PyramidSort: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();

            }
            NotifySortingStepsChanged(steps.ToString());
        }


        private void Heapify(int[] numbers, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && numbers[left] > numbers[largest])
                largest = left;

            if (right < n && numbers[right] > numbers[largest])
                largest = right;

            if (largest != i)
            {
                int swap = numbers[i];
                numbers[i] = numbers[largest];
                numbers[largest] = swap;

                Heapify(numbers, n, largest);
            }
        }
    }
}