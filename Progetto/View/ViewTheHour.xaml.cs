namespace Progetto.View;
using ModelView;
public partial class ViewTheHour : ContentPage
{
	public ViewTheHour(ModelViewDetails modelView)
	{
		InitializeComponent();
        BindingContext = modelView;
    }
}