using System;
using NewsChronicle.Data.Interfaces;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.Repositories;
using NewsChronicle.Data.Services;
using NewsChronicle.Data.Services.Mock;
using Unity;
using Unity.ServiceLocation;

namespace NewsChronicle.Data
{
    public sealed class ViewModelLocator
    {
        private static readonly Lazy<ViewModelLocator> lazy = new Lazy<ViewModelLocator>(() => new ViewModelLocator());
        public static ViewModelLocator Instance => lazy.Value;

        private UnityContainer _container;
        private UnityServiceLocator _resolver;

        private ViewModelLocator()
        {
            _container = new UnityContainer();
            _resolver = new UnityServiceLocator(_container);

            // register the interfaces with their class implementations
            _container.RegisterSingleton<IConnectivityService,   ConnectivityService>();
            _container.RegisterSingleton<IArticleService,        ArticleService>();
            _container.RegisterSingleton<IDBRepository<Article>, ArticleRepository>();
            _container.RegisterSingleton<IAppLanguageSetting,    AppLanguageSetting>();
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TAbstraction">Abstraction type</typeparam>
        /// <typeparam name="TImpl">Type to create</typeparam>
        public void RegisterSingleton<TAbstraction, TImpl>()
        {
            _container.RegisterSingleton(typeof(TAbstraction), typeof(TImpl));
        }

        public BaseViewModel GetInstanceViewModelInstance<BaseViewModel>()
        {
            return _resolver.GetInstance<BaseViewModel>();
        }
    }
}
