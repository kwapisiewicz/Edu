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

namespace Exams.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string serverUri = @"http://localhost:9000/";
            Default.Container container = new Default.Container(new Uri(serverUri));
            TreeViewItem root = new TreeViewItem();
            root.Header = "Questions from " + serverUri;

            foreach (var question in container.Questions.Expand("Answers"))
            {                
                var qItem = new TreeViewItem();
                qItem.Header = string.Format("{0} {1}", question.Text, question.MaxPoints);
                root.Items.Add(qItem);
                foreach (var answer in question.Answers)
                {
                    qItem.Items.Add(
                        new TreeViewItem()
                        {
                            Header = string.Format("{0} {1}", answer.Text, answer.NoPoints)
                        });
                }
            }

            Tree.Items.Add(root);
        }
    }
}
