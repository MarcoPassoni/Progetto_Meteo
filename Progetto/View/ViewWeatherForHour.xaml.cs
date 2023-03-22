namespace Progetto.View;
using ModelView;
public partial class ViewWeatherForHour : ContentPage
{
	public ViewWeatherForHour(ModelViewDetails modelView)
	{
		InitializeComponent();

        BindingContext = modelView;
    }
}