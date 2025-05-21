using System;
using System.Collections.ObjectModel; // Для использования ObservableCollection
using System.IO; // Для работы с файлами
using System.Windows; // Основные классы WPF (Window, Application)
using System.Windows.Controls; // Классы элементов управления (Button, TextBox, Label и т.д.)
using Newtonsoft.Json; // Для сериализации и десериализации JSON

namespace OrganizerWPF
{
    public partial class MainWindow : Window // MainWindow - окно приложения, наследуется от Window
    {
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>(); // tasks - коллекция задач, ObservableCollection автоматически уведомляет UI об изменениях
        private string dataFilePath = "tasks.json"; // Путь к файлу, где хранятся задачи

        public MainWindow()
        {
            InitializeComponent(); // Инициализация UI (загрузка XAML)

            PriorityComboBox.Items.Add("Низкий");
            PriorityComboBox.Items.Add("Средний");
            PriorityComboBox.Items.Add("Высокий");
            // Заполнение ComboBox элементами (приоритеты задач)
            PriorityComboBox.SelectedIndex = 1; // Установка "Средний" как приоритет по умолчанию

            TasksListView.ItemsSource = tasks; // Установка коллекции tasks как источник данных для ListView

            LoadTasks(); // Загрузка задач при запуске
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) // Обработчик события Click кнопки "Добавить"
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text)) // Проверка, что поле "Название задачи" не пустое
            {
                MessageBox.Show("Пожалуйста, введите название задачи.");
                return;
            }

            TaskItem newTask = new TaskItem()
            {
                Title = TitleTextBox.Text, // Установка названия задачи из TextBox
                Description = DescriptionTextBox.Text, // Установка описания задачи из TextBox
                DueDate = DueDateDatePicker.SelectedDate ?? DateTime.Now, // Установка даты выполнения из DatePicker (если дата не выбрана, то текущая дата)
                Priority = PriorityComboBox.SelectedItem.ToString() // Установка приоритета из ComboBox
            };

            tasks.Add(newTask); // Добавление новой задачи в коллекцию tasks
            ClearInputFields(); // Очистка полей ввода
            SaveTasks(); // Сохранение задач в файл
        }

        private void TasksListView_SelectionChanged(object sender, SelectionChangedEventArgs e) // Обработчик события SelectionChanged в ListView
        {
            if (TasksListView.SelectedItem is TaskItem selectedTask) // Проверка, что выбран элемент
            {
                TitleTextBox.Text = selectedTask.Title;
                DescriptionTextBox.Text = selectedTask.Description;
                DueDateDatePicker.SelectedDate = selectedTask.DueDate;
                PriorityComboBox.SelectedItem = selectedTask.Priority;
                // Заполнение полей ввода данными выбранной задачи

                SaveButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            else
            {
                ClearInputFields();
                SaveButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) // Обработчик события Click кнопки "Сохранить"
        {
            if (TasksListView.SelectedItem is TaskItem selectedTask) // Проверка, что выбрана задача для сохранения
            {
                selectedTask.Title = TitleTextBox.Text;
                selectedTask.Description = DescriptionTextBox.Text;
                selectedTask.DueDate = DueDateDatePicker.SelectedDate ?? DateTime.Now;
                selectedTask.Priority = PriorityComboBox.SelectedItem.ToString();
                // Обновление свойств выбранной задачи данными из полей ввода

                TasksListView.Items.Refresh();
                ClearInputFields();
                SaveTasks(); // Сохранение задач после редактирования
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) // Обработчик события Click кнопки "Удалить"
        {
            if (TasksListView.SelectedItem is TaskItem selectedTask)
            {
                tasks.Remove(selectedTask); // Удаление задачи из коллекции
                ClearInputFields();
                SaveTasks(); // Сохранение задач после удаления
            }
        }

        private void ClearInputFields() // Метод для очистки полей ввода и деактивации кнопок
        {
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            DueDateDatePicker.SelectedDate = DateTime.Now;
            PriorityComboBox.SelectedIndex = 1;
            SaveButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
        }

        private void SaveTasks() // Метод для сохранения задач в файл (JSON)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasks, Newtonsoft.Json.Formatting.Indented); // Сериализация коллекции tasks в JSON-строку
                File.WriteAllText(dataFilePath, json); // Запись JSON-строки в файл
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении задач: {ex.Message}");
            }
        }

        private void LoadTasks() // Метод для загрузки задач из файла (JSON)
        {
            if (File.Exists(dataFilePath)) // Проверка, существует ли файл с задачами
            {
                try
                {
                    string json = File.ReadAllText(dataFilePath); // Чтение JSON-строки из файла
                    if (!string.IsNullOrEmpty(json)) // Проверка, что строка не пустая
                    {
                        var loadedTasks = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json); // Десериализация JSON-строки в коллекцию ObservableCollection<TaskItem>
                        if (loadedTasks != null) // Проверка, что десериализация прошла успешно
                        {
                            tasks = loadedTasks; // Присвоение загруженных задач коллекции tasks
                        }
                        else
                        {
                            tasks = new ObservableCollection<TaskItem>(); // Создать новый список, если deserialization вернул null
                        }
                    }
                    else
                    {
                        tasks = new ObservableCollection<TaskItem>(); // Создать новый список, если файл пустой
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке задач: {ex.Message}");
                }
            }
            else
            {
                tasks = new ObservableCollection<TaskItem>(); // Создать новый список, если файл не существует
            }
            TasksListView.ItemsSource = tasks; //Убедитесь, что tasks задано как источник данных для TasksListView
        }
    }

    public class TaskItem // Класс для представления задачи
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
    }
}
