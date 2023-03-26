using Progetto.Model;
using Progetto.ModelView;
namespace Progetto.View;

public partial class ViewTheDay : ContentPage
{
	public ViewTheDay(ModelViewDetails modelView)
	{
		InitializeComponent();
        BindingContext = modelView;
    }
}