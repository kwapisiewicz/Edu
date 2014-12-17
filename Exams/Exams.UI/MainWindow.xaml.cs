using Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        static string serverUri = @"http://localhost:9000/";
        Container odataContext;

        string masterPassword = "abc";

        public MainWindow()
        {
            odataContext = new Default.Container(new Uri(serverUri));
            odataContext.SendingRequest2 += SetMasterPasswordHeader;

            InitializeComponent();
            try
            {
                foreach (var item in odataContext.Categories)
                {
                    var b = new Button() { Content = item.Name };
                    b.Click += b_Click;
                    Categ.Children.Add(b);
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }

        void b_Click(object sender, RoutedEventArgs e)
        {
            Fill((string)((Button)sender).Content);
        }

        private void Fill(string category)
        {
            TreeViewItem root = new TreeViewItem();
            root.Header = "Questions from " + serverUri;


            foreach (var question in odataContext.Questions.Expand("Answers")
                .Where(a=>a.Categories.Any(b=>b.Name==category)))
            {
                var qItem = new TreeViewItem();
                qItem.Header = string.Format("{0} {1}", question.Text, question.MaxPoints);
                root.Items.Add(qItem);
                foreach (var answer in question.Answers)
                {
                    qItem.Items.Add(
                        new TreeViewItem()
                        {
                            Header = string.Format("{0} {1}", answer.Text, answer.Score)
                        });
                }
            }

            Tree.Items.Add(root);
        }

        void SetMasterPasswordHeader(object sender, Microsoft.OData.Client.SendingRequest2EventArgs e)
        {
            e.RequestMessage.SetHeader(
                "MasterPassword",
                new Md5PasswordHash().Evaluate(masterPassword));
        }
    }
}
