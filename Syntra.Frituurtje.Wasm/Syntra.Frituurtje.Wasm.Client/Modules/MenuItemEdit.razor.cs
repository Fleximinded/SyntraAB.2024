
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Wasm.Shared;

namespace Syntra.Frituurtje.Wasm.Client.Modules
{
    public partial class MenuItemEdit
    {
        const int MaxFileSize = 1024 * 1024 * 5;
        [Parameter]
        public MenuItem? Value { get; set; }
        [Parameter]
        public EventCallback<MenuItem> ValueChanged { get; set; }
        [Inject]
        public IMenuService Service { get; set; } = default!;
        IEnumerable<MenuTopic>? MenuTopics { get; set; }
        bool FileError { get; set; } = false;
        protected override async void OnAfterRender(bool firstRender)
        {
            if(firstRender)
            {
                MenuTopics = await Service.GetTopics(false);
                StateHasChanged();
            }
        }
        private async Task OnSave()
        {
            if(Value?.Topic==null)
            {
                Value.Topic = MenuTopics?.FirstOrDefault();
            }
            await ValueChanged.InvokeAsync(Value);
        }
        async Task OnFileSelected(InputFileChangeEventArgs e)
        {
            FileError = false;
            var file = e.File;
            if(file != null && Value != null && file.Size < MaxFileSize)
            {

                MenuImage img = new MenuImage();
                img.ImageType = file.ContentType;
                MemoryStream memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                img.Data = memoryStream.ToArray();
                img.Name = file.Name;
                Value.Images.Clear();
                Value.Images.Add(img);
            }
            else
            {
                FileError = true;
            }
        }
        private void OnTopicSelected(ChangeEventArgs e)
        {
            var value = e.Value;
        }
    }
}
