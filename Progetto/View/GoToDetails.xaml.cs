using CommunityToolkit.Mvvm.DependencyInjection;
using Progetto.Model;
using Progetto.ModelView;

namespace Progetto.View;

public partial class GoToDetails : ContentPage
{
	public GoToDetails(Locations loc)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);


        BindingContext = new ModelViewDetails(loc);
    }
}