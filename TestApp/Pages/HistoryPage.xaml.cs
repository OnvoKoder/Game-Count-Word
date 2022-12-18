using System;
using System.Collections.Generic;
using System.Windows.Controls;
using TestApp.Class.DataFiles;

namespace TestApp.Pages
{
    /// <summary>
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        History history = new History();
        public HistoryPage()
        {
            InitializeComponent();
            List<string> list = new List<string>();
            list.Add(DateTime.Now + " просмотр истории");
            history.WriteTextFile(ref list);
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            lbResult.ItemsSource = null;
            history.ReadTextFile(out List<string> files);
            lbResult.ItemsSource = files;
        }
    }
}
