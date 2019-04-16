using JWOrbitalSimulatorPortable.ViewModels;

namespace OrbitalSimulator.Pages
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : AbstractMVVMPage<StartMenuViewModel>
    {
        // Constructor initialized component,
        // base construtor creates a new instance of StartMenuViewModel to control the page
        public StartMenu() => InitializeComponent();
    }
}
