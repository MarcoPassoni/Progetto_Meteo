using Progetto.Model;
using Progetto.ModelView;

namespace Progetto.View;

public partial class DefaultPage : ContentPage
{
	public DefaultPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}