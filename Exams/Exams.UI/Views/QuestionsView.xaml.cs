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
    /// Interaction logic for QuestionsView.xaml
    /// </summary>
    public partial class QuestionsView : UserControl
    {
        public QuestionsView()
        {
            InitializeComponent();
            Fill("A");
        }

        private void Fill(string category)
        {
            TreeViewItem root = new TreeViewItem();
            ODataClient client = new ODataClient();
            root.Header = "Questions";


            foreach (var question in client.Context.Questions
                .Expand("Answers")
                //.Where(a => a.Categories.Any(b => b.Name == category))
                )
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

            QuestionsTree.Items.Add(root);
        }   
    }
}
