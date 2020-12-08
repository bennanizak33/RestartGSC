using RestartGSC_WPF.Helpers;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace ExecutionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LogWriter _logWriter;
        private GlobalKeyboardHook _globalKeyboardHook;
        private readonly string cheminBatchSucces = @"" + ConfigurationManager.AppSettings["CheminBatchSucces"];
        private readonly string cheminBatchError = @"" + ConfigurationManager.AppSettings["CheminBatchError"];
        private readonly bool isFeatureLockScreen= bool.Parse(ConfigurationManager.AppSettings["isFeatureLockScreen"]);
        private Window window;
        private int exitCode = 0;
        private IO.Swagger.Api.AuthorizationsApi AuthorizationsApi;
        private IO.Swagger.Api.ServerEventsApi ServerEventsApi;


        public MainWindow()
        {          
            InitializeComponent();
            _logWriter = new LogWriter();
            _logWriter.LogWrite("Début exécution");
            _globalKeyboardHook = new GlobalKeyboardHook();

            AuthorizationsApi = new IO.Swagger.Api.AuthorizationsApi();
            ServerEventsApi = new IO.Swagger.Api.ServerEventsApi();
        }
        
        private void executerSucces_Click(object sender, RoutedEventArgs e)
        {
            IO.Swagger.Model.AuthorizationModel authorizationResult = null;

            _logWriter.LogWrite($"exécution Batch : {cheminBatchSucces}");
            string output = UptimeHelper.processBatch(cheminBatchSucces, "FRPARWEBDEV24", _logWriter, ref exitCode);
            _logWriter.LogWrite($"exit code Batch : {exitCode}");

            if (exitCode != 0)
            {

            }

            if (exitCode == 0)
            {
                var uptime = UptimeHelper.GetUptime(output);

                DateTime LastBootTime = DateTime.Now.AddDays(-uptime);

                //TODO
                //string ServerIpAddress = IpAddressHelper.CcToIp(ServerName);

                _logWriter.LogWrite($"date dernier upTime : {uptime} days ago");

                //gestion des exceptions pour apres
                authorizationResult = AuthorizationsApi.AuthorizationsPostAuthorization("10.21.207.0", LastBootTime);

                IO.Swagger.Model.ServerEvent event_ = null;

                if (authorizationResult.StatusCode == "OK")
                {
                    event_ = new IO.Swagger.Model.ServerEvent()
                    {
                        Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_1
                        // Insert the correct values
                    };

                    // restart

                    // blocage de l'interface 

                }
                else
                {
                    // Specifié la raison du rejet  Event.demandeRejete + DAte du rejet
                    event_ = new IO.Swagger.Model.ServerEvent()
                    {
                        Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_3
                        // Insert the correct values
                    };
                }

                

                

                // 10 d'attentes appconfig

                // ping + log api

                try
                {
                    
                    ServerEventsApi.ServerEventsPostServerEvent(event_);
                }
                catch (Exception ex)
                {

                    throw;
                }


#if DEBUG

                // Log

                // blocage screen

                // commande de redemarrage
#endif
            }
        }
    
        private void executerError_Click(object sender, RoutedEventArgs e)
        {
            _logWriter.LogWrite($"exécution Batch : {cheminBatchError}");
            string output = UptimeHelper.processBatch(cheminBatchError, "", _logWriter, ref exitCode);
            _logWriter.LogWrite($"exit code Batch : {exitCode}");

            if (exitCode == -1)
            {
                if (!isFeatureLockScreen)
                {
                    window = new Window
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
#if !DEBUG
                        //Plein écran pour tout cacher, à personnaliser
                        WindowState = WindowState.Maximized,
                        WindowStyle = WindowStyle.None
#endif
                    };
                    // Hooks into all keys.
                    _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
#if !DEBUG
                    //Désactiver le vérouillage après 15 secondes pour pouvoir quitter (à être remplacé par un autre logique après)
                    Task.Delay(new TimeSpan(0, 0, 15)).ContinueWith(o => 
                    { _globalKeyboardHook.KeyboardPressed -= OnKeyPressed; 
                        if (window != null)
                            window.Close();
                    });
#endif
                    _logWriter.LogWrite($"Affichage plein écran + bloquer Hotkeys");
                    window.Show();
                }
                else
                {
                    _logWriter.LogWrite($"Vérouillage système");
                    LockWorkStation();
                }
            }
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            e.Handled = true;
        }
        [DllImport("user32")]
        public static extern void LockWorkStation();
    }
}