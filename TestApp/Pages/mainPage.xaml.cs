using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp.Class.DataFiles;
using TestApp.Class.Managment;

namespace TestApp.Pages
{
    /// <summary>
    /// Interaction logic for mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        public mainPage()
        {
            InitializeComponent();
        }

        private void txtPerson_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void PlayGame(object sender, RoutedEventArgs e)
        {
            LastGameHistory lastGame = new LastGameHistory();
            History history=new History();
            List<string> list = new List<string>(1);
            list.Add(DateTime.Now + " выбрана игра");
            history.WriteTextFile(ref list);
            List<string> input = new List<string>(3);
            input.Add(cmbLanguage.SelectedIndex.ToString());
            input.Add(cmbLevel.SelectedIndex.ToString());
            input.Add(txtPerson.Text.ToString());
            lastGame.WriteTextFile(ref input);
            Manager.mainFrame.Navigate(new GamePage());
            
        }
    }
}
