using HandHeldbB.PageModel;

namespace HandHeldbB.Page;

public partial class OverViewPage : ContentPage
{
	public OverViewPage()
	{
		InitializeComponent();
        BindingContext = new OverViewPageModel();
    }
}