using System;
using System.Collections.Generic;
using System.Windows;
using TestApp.Class.DataFiles;
using TestApp.Class.Managment;
using TestApp.Pages;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new mainPage());
            Manager.mainFrame = mainFrame;
            History history = new History();
            List<string> list = new List<string>();
            list.Add(DateTime.Now + " запуск приложения");
            history.WriteTextFile(ref list);
        }

        private void mainFrame_ContentRendered(object sender, EventArgs e) => btnBack.Visibility = mainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;

        private void Back(object sender, RoutedEventArgs e) => mainFrame.GoBack();

        private void Result(object sender, RoutedEventArgs e) => mainFrame.Navigate(new ResultPage());

        private void Game(object sender, RoutedEventArgs e) => mainFrame.Navigate(new GamePage());

        private void History(object sender, RoutedEventArgs e)=> mainFrame.Navigate(new HistoryPage());

        private void Settings(object sender, RoutedEventArgs e) => mainFrame.Navigate(new SettingsPage());
    }
}
