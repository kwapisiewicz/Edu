using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Exams.UI.Teacher.Categories;
using Exams.UI.Teacher.Questions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exams.UI.Teacher
{
    public class TeacherModule : IModule
    {
        IRegionManager _regionManager;
        IWindsorContainer _container;

        public TeacherModule(IRegionManager regionManager, IWindsorContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.Register(Component.For<CategoriesView>().Named(typeof(CategoriesView).FullName));
            _container.Register(Component.For<QuestionsView>().Named(typeof(QuestionsView).FullName));
            _container.Register(Component.For<AddQuestionView>().Named(typeof(AddQuestionView).FullName));            
            //Starting regions registration

            //View models
            _container.Register(Component.For<CategoriesViewModel>());            
            _container.Register(Component.For<QuestionsViewModel>());
            _container.Register(Component.For<AddQuestionViewModel>());   
        }
    }
}
