using Progetto.ModelView;

namespace Progetto.View;

public partial class ViewTheWeek : ContentPage
{
	public ViewTheWeek(ModelViewDetails modelView)
	{
		InitializeComponent();

		BindingContext = modelView;
	}
}