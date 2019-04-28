using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

namespace Sagromin.Helpers
{
    public class DataHelper<TModel>
    {
        private AppWebViewPage<TModel> dataWebViewPage { get; set; }

        public DataHelper(AppWebViewPage<TModel> dataWebViewPage)
        {
            if (dataWebViewPage == null)
            {
                throw new ArgumentNullException("dataWebViewPage");
            }
            this.dataWebViewPage = dataWebViewPage;
        }

        public MvcHtmlString AjaxLink(String actionName, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, null, null, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, null, routeValues, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, controllerName, null, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            return AjaxLink(actionName, null, routeValues, ajaxOptions, htmlAttributes);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, object routeValues, AjaxOptions ajaxOptions)
        {
            return AjaxLink(actionName, controllerName, routeValues, ajaxOptions, null);
        }

        public MvcHtmlString AjaxLink(String actionName, String controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var actionLink = dataWebViewPage.Ajax.ActionLink("#", actionName, controllerName, routeValues, ajaxOptions, htmlAttributes).ToString();
            actionLink = actionLink.Replace("data-ajax=\"true\"", "data-ajax-link=\"true\"");
            return MvcHtmlString.Create(actionLink.Substring(3, actionLink.Length - 9));
        }
        /// <summary>
        /// Si el Request contiene el parámetro UpdateTargetId, entonces genera los atributos necesarion por el Unobstrusive Ajax:
        /// data-ajax="true="
        /// data-update-id="Valor del UpdateTargetId"
        /// </summary>
        /// <returns></returns>
        public MvcHtmlString CreateAutoTargetAjaxAttributes()
        {
            var updateTargetId = dataWebViewPage.Request.Params["UpdateTargetId"];

            if (dataWebViewPage.Request.IsAjaxRequest() && !String.IsNullOrEmpty(updateTargetId))
            {
                var actionLink = dataWebViewPage.Ajax.ActionLink("#", "AAA", "CCC", null, new AjaxOptions() { UpdateTargetId = updateTargetId }).ToString();
                actionLink = actionLink.Replace("href=\"/CCC/AAA\"", "");
                return MvcHtmlString.Create(actionLink.Substring(3, actionLink.Length - 9));
            }
            return MvcHtmlString.Empty;
        }

        /*
         * data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         *  new  data-type="modal-link" data-source-url="@Url.Action("EditAutor", new { ExperienciaExitosaId = Model.ExperienciaExitosaId, AutorId = item.AutorId})"
         */

        //public IHtmlString ModalLink(String action, String onCloseScript = "")
        //{
        //    return ModalLink(action, null, null, onCloseScript);
        //}

        public IHtmlString ModalLink(String action, String controller, String onCloseScript = "")
        {
            return ModalLink(action, controller, null, onCloseScript);
        }

        public IHtmlString ModalLink(String action, object routeValues, String onCloseScript = "")
        {
            return ModalLink(action, null, routeValues, onCloseScript);
        }

        public IHtmlString ModalLink(String action, String controller, object routeValues, String onCloseScript = "")
        {
            var result = "data-type=\"modal-link\" data-source-url=\"" + dataWebViewPage.Url.Action(action, controller, routeValues) + "\"";

            if (!String.IsNullOrEmpty(onCloseScript))
                result += " data-on-close=\"" + onCloseScript + "\"";

            return new HtmlString(result);
        }

        public IHtmlString ToolTip(String text, String position = "top", Boolean isHtml = false)
        {
            var result = "data-toggle=\"tooltip\" data-placement=\"" + position + "\" title=\"" + text + "\"";

            if (isHtml)
                result += " data-html=\"true\"";

            return new HtmlString(result);
        }

        public IHtmlString BitFilter(String blockId = "", String method = "GET")
        {
            return BitFilter(null, null, null, blockId, method);
        }

        public IHtmlString BitFilter(String action, String blockId = "", String method = "GET")
        {
            return BitFilter(action, null, null, blockId, method);
        }

        public IHtmlString BitFilter(String action, String controller, String blockId = "", String method = "GET")
        {
            return BitFilter(action, controller, null, blockId, method);
        }

        public IHtmlString BitFilter(String action, object routeValues, String blockId = "", String method = "GET")
        {
            return BitFilter(action, null, routeValues, blockId, method);
        }

        public IHtmlString BitFilter(String action, String controller, object routeValues, String blockId = "", String method = "GET")
        {
            var result = "data-type=\"bit-filter\" data-bf-source-url=\"" + dataWebViewPage.Url.Action(action, controller, routeValues) + "\"";

            if (method.ToUpper() != "GET")
                result += " data-bf-method=\"" + method.ToUpper() + "\"";

            if (!String.IsNullOrEmpty(blockId))
                result += " data-bf-block-id=\"" + blockId + "\"";

            return new HtmlString(result);
        }

        public IHtmlString ValidationFor<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
        {
            var html = dataWebViewPage.Html;
            var propertyName = html.NameFor(propertyExpression).ToString();
            var metadata = ModelMetadata.FromLambdaExpression(propertyExpression, html.ViewData);
            var attributes = html.GetUnobtrusiveValidationAttributes(propertyName, metadata);
            return new HtmlString(String.Join(" ", attributes.Select(x => String.Format("{0}=\"{1}\"", x.Key, x.Value)).ToArray()));
        }

        public IHtmlString GoogleReCaptcha()
        {
            var siteKey = System.Web.Configuration.WebConfigurationManager.AppSettings["Google.Captcha.SiteKey"];
            var result = "<div class=\"g-recaptcha\" data-sitekey=\"" + siteKey + "\"></div>";
            return new HtmlString(result);
        }

    }
}

