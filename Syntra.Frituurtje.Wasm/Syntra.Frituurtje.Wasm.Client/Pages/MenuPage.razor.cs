using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Client.Services;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm.Client.Pages
{
    public partial class MenuPage
    {
        [Inject]
        public IMenuService MenuService { get; set; } = default!;

        IEnumerable<MenuTopic>? Topics { get; set; } = null;
        IEnumerable<MenuItem>? MenuItems { get; set; } = null;
        Modal BbModal { get; set; } = default!;
        MenuItem? SelectedItem { get; set; } = null;
        string ItemAction { get; set; } = "";
       
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                Topics = await MenuService.GetTopics();
                StateHasChanged();
            }
        }


        async Task OnAddItem(MenuTopic owner)
        {

            SelectedItem = new MenuItem() { Topic = owner };
            await BbModal.ShowAsync();
        }

        async Task OnEditItem(MenuItem item)
        {
            if(item != null)
            {
                if(item.Topic == null)
                {
                   
                }
                SelectedItem = item;

                await BbModal.ShowAsync();
            }
        }
        private async Task OnRowClick(GridRowEventArgs<MenuTopic> args)
        {
            MenuItems = args.Item.MenuItems;
            foreach(var item in MenuItems)
            {
                item.Topic = args.Item;
            }   
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
        public async Task OnSave()
        {
            if(SelectedItem != null)
            {
                await MenuService.SetItem(SelectedItem);
                  await BbModal.HideAsync();
            }

        }
    }
}
