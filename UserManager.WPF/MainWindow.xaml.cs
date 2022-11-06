using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;
using UserManager.Blazor;
using UserManager.Blazor.Client;
using UserManager.Blazor.Services;
using UserManager.Blazor.State;

namespace UserManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var serviceCollection = new ServiceCollection();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            var config = builder.Build();

#if DEBUG
            serviceCollection.AddBlazorWebViewDeveloperTools();
#endif
            serviceCollection.AddWpfBlazorWebView();
            serviceCollection.AddMasaBlazor();
            serviceCollection.AddHttpClient<ManageClient>(client =>
            {
                client.BaseAddress = new Uri(config.GetValue<string>("ApiUri"));
            });
            serviceCollection.AddHttpClient<AuthClient>(client =>
            {
                client.BaseAddress = new Uri(config.GetValue<string>("ApiUri"));
            });
            serviceCollection.AddBlazoredLocalStorage();
            serviceCollection.AddAuthorizationCore();
            serviceCollection.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddSingleton<AuthState>();
            serviceCollection.AddSingleton<StateManager>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var blazor = new BlazorWebView()
            {
                HostPage = @"wwwroot\index.html",
                Services = serviceProvider,
                UrlLoading = (sender, urlLoadingEventArgs) =>
                    urlLoadingEventArgs.UrlLoadingStrategy = UrlLoadingStrategy.OpenInWebView
            };
            blazor.RootComponents.Add(new RootComponent()
            {
                ComponentType = typeof(Blazor.App),
                Selector = "#app"
            });
            Content = blazor;

        }
    }
}
