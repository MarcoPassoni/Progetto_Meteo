using Progetto.View;

namespace Progetto;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(GoToDetails), typeof(GoToDetails));
    }
}
