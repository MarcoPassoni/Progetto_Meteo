using CommunityToolkit.Mvvm.DependencyInjection;
using Progetto.Model;
using Progetto.ModelView;

namespace Progetto.View;

public partial class GoToDetails : ContentPage
{
	public GoToDetails(Locations loc, string data, string min, string max)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);


        BindingContext = new ModelViewDetails(loc, data, min, max);
    }
}