using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestApp.Class.DataFiles;

namespace TestApp.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void txtPerson_PreviewTextInput(object sender, TextCompositionEventArgs e) => txtPerson.TextWrapping = TextWrapping.Wrap;

        private void AddText(object sender, RoutedEventArgs e)
        {
            if(cmbLanguage.SelectedIndex!=-1 && cmbLevel.SelectedIndex != -1 && txtPerson.Text!="")
            {
                string type = cmbLanguage.SelectedIndex == 0 ? "RU" : "EN";
                type += cmbLevel.SelectedIndex == 0 ? "_EASY_" : cmbLevel.SelectedIndex == 1 ? "_MIDDLE_" : "_HARD_";
                Content text = new Content();
                type+=text.Counting(type)+1;
                text.SetLanguageLevel(type);
                List<string> vs = new List<string>(1);
                vs.Add(txtPerson.Text);
                text.WriteTextFile(ref vs);
                List<string> list = new List<string>(1);
                History history = new History();
                list.Add(DateTime.Now+" добавлено предложение к уровню");
                history.WriteTextFile(ref list);
            }
        }
    }
}
