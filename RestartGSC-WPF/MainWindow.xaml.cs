using RestartGSC_WPF;
using RestartGSC_WPF.Helpers;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace ExecutionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly LogWriter _logWriter;
        private readonly string cheminBatchSucces = @"" + ConfigurationManager.AppSettings["CheminBatchSucces"];
        private readonly string cheminBatchError = @"" + ConfigurationManager.AppSettings["CheminBatchError"];
        private readonly bool isFeatureLockScreen= bool.Parse(ConfigurationManager.AppSettings["isFeatureLockScreen"]);
        private int exitCode = 0;
        private IO.Swagger.Api.AuthorizationsApi AuthorizationsApi;
        private IO.Swagger.Api.ServerEventsApi ServerEventsApi;


        public MainWindow()
        {          
            InitializeComponent();
            _logWriter = new LogWriter();
            _logWriter.LogWrite("Début exécution");

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
#if !DEBUG
                authorizationResult = AuthorizationsApi.AuthorizationsPostAuthorization("10.21.207.0", LastBootTime);
#else
                authorizationResult = new IO.Swagger.Model.AuthorizationModel();
                authorizationResult.StatusCode = "OK";
#endif
                IO.Swagger.Model.ServerEvent event_ = null;

                if (authorizationResult.StatusCode == "OK")
                {
                    event_ = new IO.Swagger.Model.ServerEvent()
                    {
                        Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_1
                        // Insert the correct values
                    };

                    // blocage de l'interface 
                    var _bloquerWindow = new bloquerWindow(_logWriter);
                    _bloquerWindow.Show();
                    this.Close();
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
                try
                {
#if !DEBUG
                    ServerEventsApi.ServerEventsPostServerEvent(event_);
#endif
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
                   
                }
                else
                {
                    _logWriter.LogWrite($"Vérouillage système");
                    LockWorkStation();
                }
            }
        }
     
        [DllImport("user32")]
        public static extern void LockWorkStation();
    }
}