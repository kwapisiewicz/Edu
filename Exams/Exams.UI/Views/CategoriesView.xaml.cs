using Exams.UI.Context;
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

namespace Exams.UI.Views
{
    /// <summary>
    /// Interaction logic for CategoriesView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        ODataClient _client;
        public CategoriesView(ODataClient client)
        {
            _client = client;
            InitializeComponent();
            try
            {
                var cats = _client.Context.Categories.ToArray().Select(a => a.Name);
                Categories.ItemsSource = cats;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}
