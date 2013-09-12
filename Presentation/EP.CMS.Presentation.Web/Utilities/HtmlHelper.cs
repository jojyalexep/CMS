using EP.CMS.Domain.CommonModule.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace EP.CMS.Presentation.Web.Helpers
{
    public static class EPHtmlHelper
    {

        public static string CurrentController(this HtmlHelper html)
        {
            ViewContext context = new ViewContext();

            return (string)context.RouteData.Values["Controller"];

        }

        public static string GetLanguage(int id)
        {
            if (id == (int)Language.Arabic)
            {
                return Language.Arabic.ToString();
            }
            else if (id == (int)Language.English)
            {
                return Language.English.ToString();
            }
            return "";
        }

        public static MvcHtmlString Toolbar(this HtmlHelper html, string action)
        {
            return Toolbar(html, action, action);
        }

        public static MvcHtmlString Toolbar(this HtmlHelper html, string action, string text)
        {
            return Toolbar(html, action, text, "EP.submitButton('" + action.ToLower() + "')");
        }

        public static MvcHtmlString Toolbar(this HtmlHelper html, string action, string text, string function)
        {
            var lowerAction = action.ToLower();
            var tag = new TagBuilder("li");
            tag.AddCssClass("button");
            tag.MergeAttribute("id", "toolbar-" + lowerAction);

            var anchorTag = new TagBuilder("a");
            anchorTag.AddCssClass("toolbar");
            anchorTag.MergeAttribute("onclick", function);

            var span = new TagBuilder("span");
            span.AddCssClass("icon-32-" + lowerAction);

            anchorTag.InnerHtml = span.ToString() + text;
            tag.InnerHtml = anchorTag.ToString();
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Toolbar(this AjaxHelper ajax, string action, AjaxOptions ajaxOptions)
        {
            return Toolbar(ajax, action, null, ajaxOptions);
        }

        public static MvcHtmlString Toolbar(this AjaxHelper ajax, string action, object routeValue, AjaxOptions ajaxOptions)
        {
            var controller = ajax.ViewContext.RouteData.GetRequiredString("controller");
            return Toolbar(ajax, action, controller, action, routeValue, ajaxOptions, null);

        }

        public static MvcHtmlString Toolbar(this AjaxHelper ajax, string actionName, string controllerName, string action, object routeValue, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var lowerAction = action.ToLower();
            var tag = new TagBuilder("li");
            tag.AddCssClass("button");
            tag.MergeAttribute("id", "toolbar-" + lowerAction);


            var anchor = ajax.ActionLink("[InnerHtml]", actionName, controllerName, routeValue, ajaxOptions, new { @class = "toolbar" });
            var span = new TagBuilder("span");
            span.AddCssClass("icon-32-" + lowerAction);
            var anchorTag = anchor.ToHtmlString().Replace("[InnerHtml]", span.ToString() + action);
            tag.InnerHtml = anchorTag.ToString();
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));

        }


    }
}