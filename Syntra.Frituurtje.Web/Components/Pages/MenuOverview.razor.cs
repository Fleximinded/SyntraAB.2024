using Microsoft.AspNetCore.Components;
using Syntra.Frituurtje.Web.Define;
using Syntra.Frituurtje.Contracts.Models;
using BlazorBootstrap;

namespace Syntra.Frituurtje.Web.Components.Pages
{
    public partial class MenuOverview
    {
        [Inject]
        IMenuService Service { get; set; } = default!;
        IEnumerable<MenuTopic>? Topics { get; set; } = null;
        IEnumerable<MenuItem>? MenuItems { get; set; } = null;
        Modal BbModal { get; set; } = default!;
        MenuItem? SelectedItem { get; set; } = null;
        string ItemAction { get; set; } ="";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                Topics = await Service.GetTopics();
                StateHasChanged();
            }
        }
        async Task OnAddItem(MenuTopic owner) {

            SelectedItem = new MenuItem() { Topic = owner };
            await BbModal.ShowAsync();
        }

        async Task OnEditItem(MenuItem item) {

            SelectedItem = item;
            await BbModal.ShowAsync();
        }
        private async Task OnRowClick(GridRowEventArgs<MenuTopic> args)
        {
            MenuItems = args.Item.MenuItems;
        }
        private async Task OnItemRowClick(GridRowEventArgs<MenuItem> args)
        {
        }
        private async Task OnHideModalClick()
        {
            await BbModal.HideAsync();
        }
        public void OnItemEdited()
        {
        }
        public async Task OnSave() {
            if(SelectedItem != null)
            {
                await Service.SetItem(SelectedItem);
                await BbModal.HideAsync();
            }

        }
    }
}
