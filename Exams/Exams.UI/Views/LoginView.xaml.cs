using Exams.UI.Infrastructure;
using Exams.UI.Views;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
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

namespace Exams.UI.Windows
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        IRegionManager _regionManager;
        LoginContext _loginContext;

        public LoginView(IRegionManager regionManager, LoginContext loginContext)
        {
            _regionManager = regionManager;
            _loginContext = loginContext;

            DataContext = this;
            NavigateStudent = new DelegateCommand(LoginStudent);
            NavigateGetTeacherPassword = new DelegateCommand(GetTeacherPassword);
            InitializeComponent();
        }

        public ICommand NavigateStudent { get; set; }
        public ICommand NavigateGetTeacherPassword { get; set; }

        public void LoginStudent()
        {
            _loginContext.LoginAsStudent();
            _regionManager.RequestNavigate(Regions.MainWindow, typeof(MainView).FullName);
        }

        public void GetTeacherPassword()
        {
            _regionManager.RequestNavigate(Regions.MainWindow, typeof(PasswordView).FullName);        
        }
    }
}
