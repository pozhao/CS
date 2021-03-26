using System.Web.Mvc;

namespace CSWeb.App_Code
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString CreateButton(this HtmlHelper helper, string id, string text)
        {
            TagBuilder tag = new TagBuilder("input");
            tag.Attributes.Add("id", id);
            tag.Attributes.Add("value", text);
            tag.Attributes.Add("type", "submit");
            tag.AddCssClass("btn btn-primary");

            return MvcHtmlString.Create(tag.ToString());
        }

    }
}