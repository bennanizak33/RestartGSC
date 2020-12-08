using ExecutionWPF;
using McDonalds.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using McDonalds.commun.Constants;
using RestartGSC_WPF.Helpers;

namespace RestartGSC_WPF
{
    /// <summary>
    /// Interaction logic for bloquerWindow.xaml
    /// </summary>
    public partial class bloquerWindow : Window
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private readonly LogWriter _logWriter;
        private string ServerIpAddress = string.Empty;
        private int RestaurantId = 0;

        private IO.Swagger.Api.ServerEventsApi ServerEventsApi;


        public bloquerWindow(LogWriter logWriter, string ipAddress, int restaurantId)
        {
            InitializeComponent();

            _globalKeyboardHook = new GlobalKeyboardHook();
            ServerEventsApi = new IO.Swagger.Api.ServerEventsApi();

            _logWriter = logWriter;
            ServerIpAddress = ipAddress;
            RestaurantId = restaurantId;

            // Hooks into all keys.
            _globalKeyboardHook.KeyboardPressed += OnKeyPressed;
            _logWriter.LogWrite($"Affichage plein écran + bloquer Hotkeys");

#if DEBUG
            //Plein écran pour tout cacher, à personnaliser
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
#endif

            Task.Factory.StartNew(executerProcess);
        }

        private void executerProcess()
        {
            // commande restart  .bat

            // 10 minutes d'attentes appconfig
            Thread.Sleep(1000 * AppSettings.ReadSetting<int>(AppSettingsConstant.ServerResponseTimeout, 10));

            if (PingHelper.CheckAddress(ServerIpAddress).Status != System.Net.NetworkInformation.IPStatus.Success)
            {
                ServerEventsApi.ServerEventsPostServerEvent(new IO.Swagger.Model.ServerEvent()
                {
                    Date = DateTime.Now,
                    Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_5,
                    Restaurant = new IO.Swagger.Model.Restaurant() { RestaurantId = RestaurantId },
                    Detail = "Le serveur a repondu"
                });

                this.Dispatcher.Invoke(() =>
                {
                    this.windowText.Text = "Le serveur distant a bien redemarrer";
                });

                //Thread.Sleep(1000 * AppSettings.ReadSetting<int>(AppSettingsConstant.XmlRpcResponseTimeout, 5) * 60);

                //Call XmlRpc

                //TODO

#if DEBUG
                //Désactiver le vérouillage après 15 secondes pour pouvoir quitter (à être remplacé par un autre logique après)
                Task.Delay(new TimeSpan(0, 0, 5)).ContinueWith(o =>
                {
                    _globalKeyboardHook.KeyboardPressed -= OnKeyPressed;
                    // déblocage de l'interface 
                    Application.Current.Dispatcher.Invoke((Action)delegate {
                        var _mainWindow = new MainWindow();
                        _mainWindow.Show();
                        this.Close();
                    });
                });
#endif

            }
            else
            {
                ServerEventsApi.ServerEventsPostServerEvent(new IO.Swagger.Model.ServerEvent()
                {
                    Date = DateTime.Now,
                    Event = IO.Swagger.Model.ServerEvent.EventEnum.NUMBER_6,
                    Restaurant = new IO.Swagger.Model.Restaurant() { RestaurantId = RestaurantId },
                    Detail = "Le serveur n'a pas repondu"
                });

                this.Dispatcher.Invoke(() =>
                {
                    this.windowText.Text += "\n" + "Contactez votre mainteneur!";
                });
            }           

            // if OK call xmlRPc attendre 5 minutes is ok update ecran + update api + deblocage ecran


        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            e.Handled = true;
        }
    }
}
