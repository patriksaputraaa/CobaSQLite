using CobaSQLite.Models;

namespace CobaSQLite.Pages;

public partial class CategoryPage : ContentPage
{
    public CategoryPage()
    {
        InitializeComponent();
    }

    // Add a new category
    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(description.Text))
        {
            statusMessage.Text = "Please provide both Name and Description.";
            return;
        }

        await App.CategoryRepo.AddNewCategory(name.Text, description.Text);
        statusMessage.Text = App.CategoryRepo.StatusMessage;

        await RefreshCategories();
    }

    // Retrieve all categories
    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        await RefreshCategories();
    }

    // Update a category
    public async void OnUpdateButtonClicked(object sender, EventArgs args)
    {
        if (sender is Button button && button.CommandParameter is int id)
        {
            if (string.IsNullOrWhiteSpace(name.Text) || string.IsNullOrWhiteSpace(description.Text))
            {
                statusMessage.Text = "Please provide updated Name and Description.";
                return;
            }

            await App.CategoryRepo.UpdateCategory(id, name.Text, description.Text);
            statusMessage.Text = App.CategoryRepo.StatusMessage;

            await RefreshCategories();
        }
    }

    // Delete a category
    public async void OnDeleteButtonClicked(object sender, EventArgs args)
    {
        if (sender is Button button && button.CommandParameter is int id)
        {
            bool confirmDelete = await DisplayAlert(
                "Confirm Delete",
                "Are you sure you want to delete this category?",
                "Yes",
                "No"
            );

            if (confirmDelete)
            {
                await App.CategoryRepo.DeleteCategory(id);
                statusMessage.Text = App.CategoryRepo.StatusMessage;

                await RefreshCategories();
            }
        }
    }

    // Populate the entry fields when a category is selected
    private void OnCategorySelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Category selectedCategory)
        {
            name.Text = selectedCategory.Name;
            description.Text = selectedCategory.Description;

            // Optionally, keep the selected item visible.
            categoryList.SelectedItem = null;
        }
    }

    // Refresh the list of categories
    private async Task RefreshCategories()
    {
        var categories = await App.CategoryRepo.GetAllCategory();
        categoryList.ItemsSource = categories;
    }
}
