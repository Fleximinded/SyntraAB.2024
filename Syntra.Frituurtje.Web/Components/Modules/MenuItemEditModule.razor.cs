using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;
using Syntra.Frituurtje.Contracts.Models;
using Syntra.Frituurtje.Web.Define;
using Syntra.Frituurtje.Web.Services;
using System.Security.Cryptography;

namespace Syntra.Frituurtje.Web.Components.Modules
{
    public partial class MenuItemEditModule
    {
        [Parameter]
        public MenuItem? Value { get; set; }
        [Parameter]
        public EventCallback<MenuItem> ValueChanged { get; set; }
        [Inject]
        public IMenuService Service { get; set; } = default!;
        IEnumerable<MenuTopic>? MenuTopics { get; set; }
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
            await ValueChanged.InvokeAsync(Value);
        }
        async Task OnFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if(file != null && Value!=null)
            {
                MenuImage img=new MenuImage();
                img.ImageType=file.ContentType;
                MemoryStream memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                img.Data=memoryStream.ToArray();
                img.Name=file.Name;
                Value.Images.Add(img);
            }
        }
    }
}
