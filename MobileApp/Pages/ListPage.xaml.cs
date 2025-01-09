using MobileApp.PageModels;

namespace MobileApp.Pages;

public partial class ListPage : ContentPage
{
	public ListPage(ListPageModel pageModel)
	{
		InitializeComponent();
        BindingContext = pageModel;
    }
}