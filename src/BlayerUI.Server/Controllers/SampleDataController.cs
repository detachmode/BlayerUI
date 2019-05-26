using BlayerUI.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlayerUI.Server.Controllers
{

    public class ViewModel
    {
        public string SomeText { get; set; } = "hi";
        public bool ShowTime { get; set; }
    }


    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private readonly ViewModel _viewModel;

        public SampleDataController(ViewModel state)
        {
            // var res  = System.IO.File.ReadAllText("./viewjson.json");
            this._viewModel = state;
        }


        [HttpGet("[action]")]
        public string Event(string command)
        {
            switch (command)
            {
                case "ShowTime":
                    _viewModel.ShowTime = !_viewModel.ShowTime;
                    break;
                case "ChangeSomeText":
                    _viewModel.SomeText = command + Guid.NewGuid();
                    break;

            }

            return View();
        }

        [HttpGet("[action]")]
        public string View()
        {
            BElement time = new BBText("");
            if (_viewModel.ShowTime)
            {
                time = new BDiv
                {
                    InnerElements = new List<BElement>
                    {
                        new BBText(DateTime.Now.ToLongTimeString())
                    }
                };
            }

            var ui = new JsonView(
                new BDiv
                {
                    InnerElements = new List<BElement>
                    {
                        new BBHeader("Test Page"),
                        new BBText(_viewModel.SomeText),
                        new BButton
                        {
                            InnerText = "Click Me!",
                            OnClick = "ChangeSomeText"
                        },

                        new BButton
                        {
                            InnerText = "Toogle Show Time",
                            OnClick = "ShowTime"
                        },
                        time
                    }
                }
            );

            return JsonConvert.SerializeObject(ui);
        }
    }
}
