using System.Collections.Generic;

namespace BlayerUI.Shared
{
    public class BElement
    {
        public string Tag { get; set; }
        public string InnerText { get; set; }
        public string OnClick { get; set; }
        public List<BElement> InnerElements { get; set; }
    }
}
