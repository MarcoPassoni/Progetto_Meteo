using CommunityToolkit.Mvvm.DependencyInjection;
using Progetto.Model;
using Progetto.ModelView;

namespace Progetto.View;

public partial class GoToDetails : ContentPage
{
	public GoToDetails(ModelViewDetails modelView)
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);


        BindingContext = modelView;
    }
}