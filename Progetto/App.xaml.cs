using Progetto.View;

namespace Progetto;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new home();
	}
}
