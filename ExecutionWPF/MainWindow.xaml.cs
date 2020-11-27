using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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

        public MainWindow()
        {          
            InitializeComponent();
            _logWriter = new LogWriter();
            _logWriter.LogWrite("Début exécution");
            _globalKeyboardHook = new GlobalKeyboardHook();
        }
        
        private void executerSucces_Click(object sender, RoutedEventArgs e)
        {
            _logWriter.LogWrite($"exécution Batch : {cheminBatchSucces}");
            int exitCode = processBatch(cheminBatchSucces);
            _logWriter.LogWrite($"exit code Batch : {exitCode}");

            if (exitCode == 0 /*un deuxieme exitCode*/)
            {
                //Logique après retour positif
                /*
                 * exitCode == DcMaster
                 * ExitCode2 == DcCoordnateur
                 */

                var api = new IO.Swagger.Api.AuthorizationsApi();

                try
                {
                    var result = api.AuthorizationsPostAuthorization("DcMaster1", "DeploymentCoord1", "10.19.15.12", DateTime.Now.AddDays(-12));
                }
                catch (Exception ex )
                {

                    throw;
                }

                // Log

                // blocage screen

                // commande de redemarrage
            }
        }
    
        private void executerError_Click(object sender, RoutedEventArgs e)
        {
            _logWriter.LogWrite($"exécution Batch : {cheminBatchError}");
            int exitCode = processBatch(cheminBatchError);
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
    
        private int processBatch(string cheminBatch)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo(cheminBatch)
            {
                UseShellExecute = false,
                CreateNoWindow = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true
            };
            processInfo.UseShellExecute = false;
            Process process = Process.Start(processInfo);
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            int exitCode = process.ExitCode;

            _logWriter.LogWrite(Environment.NewLine +  
                                "output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output) + Environment.NewLine +
                                 "error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error) + Environment.NewLine +
                                 "ExitCode>> " + exitCode.ToString());
            process.Close();

            return exitCode;
        }

        [DllImport("user32")]
        public static extern void LockWorkStation();
    }
}