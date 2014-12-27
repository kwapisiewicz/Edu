using Exams.UI.Infrastructure;
using Exams.UI.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
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
    /// Interaction logic for PasswordView.xaml
    /// </summary>
    public partial class PasswordView : UserControl
    {
        IRegionManager _regionManager;
        IRegionNavigationJournal _journal;
        LoginContext _loginContext;        

        public PasswordView(IRegionManager regionManager, LoginContext loginContext)
        {
            _regionManager = regionManager;
            _loginContext = loginContext;

            DataContext = this;
            NavigateTeacher = new DelegateCommand<string>(LoginTeacher);
            NavigateBack = new DelegateCommand(GoBack);
            InitializeComponent();
        }

        private void GoBack()
        {
            _regionManager.RequestNavigate(Regions.MainWindow, typeof(LoginView).FullName);
        }

        public ICommand NavigateTeacher { get; set; }
        public ICommand NavigateBack { get; set; }

        public void LoginTeacher(string password)
        {
            _loginContext.LoginAsTeacher(password);
            _regionManager.RequestNavigate(Regions.MainWindow, typeof(MainView).FullName);
        }
    }
}
