
using CobaSQLite.Models;
namespace CobaSQLite.Pages;

public partial class CategoryPage : ContentPage
{
	public CategoryPage()
	{
        InitializeComponent();
	}

    public void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        App.CategoryRepo.AddNewCategory(name.Text, description.Text);
        statusMessage.Text = App.CategoryRepo.StatusMessage;
    }

    public void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<Category> people = App.CategoryRepo.GetAllCategory();
        categoryList.ItemsSource = people;
    }
}