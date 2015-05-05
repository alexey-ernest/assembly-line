using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AssemblyLine.Infrastructure.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString RequiredFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            if (metadata.IsRequired)
            {
                return new MvcHtmlString("required");
            }

            return new MvcHtmlString(string.Empty);
        }
    }
}