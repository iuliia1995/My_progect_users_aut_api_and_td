using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace WPF
{
    public partial class MainPage : Window
    {
        private string basePath = Path.Combine(Directory.GetCurrentDirectory(), "LocalNotes");

        public MainPage(string mode = "")
        {
            InitializeComponent();

            if (mode == "local")
            {
                LoginText.Text = "Login: local";
                EmailText.Text = "Email: local";
                DateText.Text = "Дата: " + DateTime.Now.ToString("dd.MM.yyyy");
            }

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            LoadTree();
        }

        private void LoadTree()
        {
            Tree.Items.Clear();
            foreach (var dir in Directory.GetDirectories(basePath))
            {
                TreeViewItem folderItem = new TreeViewItem { Header = Path.GetFileName(dir) };
                foreach (var file in Directory.GetFiles(dir, "*.json"))
                {
                    folderItem.Items.Add(new TreeViewItem { Header = Path.GetFileNameWithoutExtension(file) });
                }
                Tree.Items.Add(folderItem);
            }
        }

        private void CreateFolder_Click(object sender, RoutedEventArgs e)
        {
            string folderName = "Новая папка " + DateTime.Now.ToString("HHmmss");
            string path = Path.Combine(basePath, folderName);
            Directory.CreateDirectory(path);
            LoadTree();
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            if (Tree.SelectedItem == null)
            {
                MessageBox.Show("Выберите папку");
                return;
            }

            TreeViewItem selectedItem = (TreeViewItem)Tree.SelectedItem;
            string folderName = selectedItem.Header.ToString();

            string path = Path.Combine(basePath, folderName, NoteNameBox.Text + ".json");
            var noteData = new { Content = NoteTextBox.Text, Date = DateTime.Now };
            string json = JsonSerializer.Serialize(noteData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);

            LoadTree();
            MessageBox.Show("Заметка сохранена");
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}