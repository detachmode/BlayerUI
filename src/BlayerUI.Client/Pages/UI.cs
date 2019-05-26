using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlayerUI.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlayerUI.Client
{
    [Route("/ui")]
    public class BUI : ComponentBase
    {

        [Inject] public HttpClient client { get; set; }


        protected override async Task OnInitAsync()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += async (a, b) => await onclick(null);
            aTimer.Interval = 2000;
            aTimer.Enabled = true;

        }

        private int counter = 0;
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            System.Diagnostics.Debug.WriteLine(counter++);
            var seq = 0;


            builder.OpenElement(++seq, "div");

            builder.AddAttribute(++seq, "class", "main");
            builder.OpenElement(++seq, "h3");

            builder.AddContent(++seq, title);
            builder.CloseElement();

            try
            {
                foreach (var uiElement in _ui.Elements)
                {
                    RenderElement(builder, seq, uiElement);
                }
            }
            catch (System.Exception e)
            {
                Print(e);
            }
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        private static void Print(object e)
        {
            System.Diagnostics.Debug.WriteLine(e);
        }

        private int RenderElement(RenderTreeBuilder builder, int seq, BElement uiElement)
        {
            builder.OpenElement(++seq, uiElement.Tag);
        
            if (uiElement.OnClick != null)
            {
                builder.AddAttribute(++seq, "class", "btn btn-primary");
                builder.AddAttribute(++seq, "onclick",
                    EventCallback.Factory.Create<UIMouseEventArgs>(this, async (e) =>
                    {
                       await EventHandlerHandler(uiElement.OnClick, e);
                    }));
            }
            
            if (uiElement.InnerText != null)
            {
                builder.AddContent(++seq, uiElement.InnerText);
            }
            if (uiElement.InnerElements != null)
            {
                foreach (var item in uiElement.InnerElements)
                {
                    RenderElement(builder, seq, item);
                }
            }
            builder.CloseElement();
            return seq;
        }

        private async Task EventHandlerHandler(string command, object e)
        {
            _ui = await client.GetJsonAsync<JsonView>(
                $"api/SampleData/event?command={command}");
        }

        protected string title = "BuildRenderTree Action";

        public JsonView _ui { get; private set; }

        public async Task onclick(UIMouseEventArgs e)
        {
            _ui = await client.GetJsonAsync<JsonView>("api/SampleData/View");

            StateHasChanged();
        }
    }
}
