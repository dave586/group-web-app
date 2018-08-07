using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using System.Dynamic;
using System.Collections;
using System.Configuration;
using System.Net;

namespace WebApplication1.Helpers
{
    public static class Html
    {
        public static MvcHtmlString DatePickerFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            var format = System.Configuration.ConfigurationManager.AppSettings.Get("DateFormat");
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string propertyName = data.PropertyName;
            TProperty val = default(TProperty);
            if (htmlHelper.ViewData.Model != null)
            {
                val = expression.Compile().Invoke(htmlHelper.ViewData.Model);
            }
            var date = "";
            if (val != null)
            {
                var dt = Convert.ToDateTime(val);
                date = dt.ToString(format);
            }

            var builder = new TagBuilder("input");
            builder.Attributes.Add("name", propertyName);
            builder.Attributes.Add("type", "text");
            builder.Attributes.Add("value", date);

            builder.Attributes.Add("id", propertyName);

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                builder.MergeAttributes(attributes);
            }
            builder.AddCssClass("date-picker");


            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString ErrorFor(this HtmlHelper helper, string errorFor)
        {
            var writer = new StringWriter();
            using (var w = new HtmlTextWriter(writer))
            {
                w.AddAttribute("data-errorfor", errorFor);
                w.AddAttribute(HtmlTextWriterAttribute.Class, "form-group invisible");
                w.RenderBeginTag(HtmlTextWriterTag.Div);

                w.AddAttribute(HtmlTextWriterAttribute.Style, "min-height=25px;");
                w.RenderBeginTag(HtmlTextWriterTag.Div);

                w.AddAttribute(HtmlTextWriterAttribute.Class, "messages");
                w.RenderBeginTag(HtmlTextWriterTag.Span);

                w.AddAttribute(HtmlTextWriterAttribute.Class, "fa fa-plus-circle pull-right fa-lg grey4");
                w.RenderBeginTag(HtmlTextWriterTag.I);
                w.RenderEndTag();
                w.RenderEndTag(); // span
                w.RenderEndTag(); // inner div
                w.RenderEndTag(); // outer div
            }
            return new MvcHtmlString(writer.ToString());
        }

        public static MvcHtmlString FieldIdFor<TModel, TValue>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string inputFieldId = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);
            return MvcHtmlString.Create(inputFieldId);
        }

        
    }
}