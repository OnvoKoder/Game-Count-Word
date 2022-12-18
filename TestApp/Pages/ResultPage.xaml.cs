using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestApp.Class.DataFiles;

namespace TestApp.Pages
{
    /// <summary>
    /// Interaction logic for ResultPage.xaml
    /// </summary>
    public partial class ResultPage : Page
    {
        public ResultPage()
        {
            InitializeComponent();
            History history = new History();
            List<string> list = new List<string>();
            list.Add(DateTime.Now + " просмотр результатов");
            history.WriteTextFile(ref list);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lbResult.ItemsSource = null;
            Result result = new Result();
            result.ReadTextFile(out List<string> files);
            lbResult.ItemsSource = files;

        }
    }
}
