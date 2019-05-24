using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlayerUI.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlayerUI.Client
{
    [Route("/classrender")]
    public class ClassOnlyComponent : ComponentBase
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
        

            builder.OpenElement(seq, "div");

            builder.AddAttribute(++seq,"class", "main");
            builder.OpenElement(seq, "h3");

            builder.AddContent(++seq, title);
            builder.CloseElement();
            // builder.OpenElement(seq, "input");
            // builder.CloseElement();
            // builder.OpenElement(++seq, "button");
            // builder.AddAttribute(7, "onclick",
            // Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.UIMouseEventArgs>(this, onclick));
            // builder.AddContent(++seq, "Click");
            // builder.CloseElement();
            try
            {
                foreach (var uiElement in ui.Elements)
                {
                    RenderElement(builder, seq, uiElement);
                }
            }
            catch (System.Exception e)
            {

                System.Diagnostics.Debug.WriteLine(e);
            }
            builder.CloseElement();




            base.BuildRenderTree(builder);
        }

        private static int RenderElement(RenderTreeBuilder builder, int seq, BElement uiElement)
        {
            builder.OpenElement(seq, uiElement.Tag);
            if(uiElement.InnerText != null)
            {
                builder.AddContent(++seq, uiElement.InnerText);
            }
            if(uiElement.InnerElements != null)
            {
                foreach (var item in uiElement.InnerElements)
                {
                    RenderElement(builder, seq, item);
                }
            }
            builder.CloseElement();
            return seq;
        }

        protected string title = "BuildRenderTree Action";

        public JsonView ui { get; private set; }

        public async Task onclick(UIMouseEventArgs e)
        {
            ui = await client.GetJsonAsync<JsonView>("api/SampleData/View");

            StateHasChanged();

        }
    }
}
