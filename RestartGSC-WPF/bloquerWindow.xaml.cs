using ExecutionWPF;
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

namespace RestartGSC_WPF
{
    /// <summary>
    /// Interaction logic for bloquerWindow.xaml
    /// </summary>
    public partial class bloquerWindow : Window
    {
        private GlobalKeyboardHook _globalKeyboardHook;
        private readonly LogWriter _logWriter;

        public bloquerWindow(LogWriter logWriter)
        {
            InitializeComponent();

            _globalKeyboardHook = new GlobalKeyboardHook();
            _logWriter = logWriter;

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
            Thread.Sleep(1000 * 5); //1000 * 60 * 10

            // ping + log api  "PingHelper Class" if result OK add serverEventapi Call, 
            // if not OK garder l'ecran bloqué et update ecran contacter votre mainteneur + apicall serverEvent
            this.Dispatcher.Invoke(() =>
            {
                this.windowText.Text = "Contactez votre mainteneur!";
            });
           

            // if OK call xmlRPc attendre 5 minutes is ok update ecran + update api + deblocage ecran

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

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            e.Handled = true;
        }
    }
}
