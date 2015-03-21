using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace UtilityMvc
{
    public abstract class HtmlBootstrapComponent<T> where T : HtmlBootstrapComponent<T>
    {
        protected TagBuilder Builder { get; set; }

        public T Class(string cssClass)
        {
            Builder.AddCssClass(cssClass);
            return this as T;
        }

        public T Id(string id)
        {
            Builder.GenerateId(id);
            return this as T;
        }

        public T Enable(bool isEnabled)
        {
            if (!isEnabled)
            {
                Builder.MergeAttribute("disabled", "disabled");
            }
            return this as T;
        }

        public T Required(bool isRequired)
        {
            if (isRequired)
            {
                Builder.MergeAttribute("required", "required");
            }
            return this as T;
        }
    }

    public class BootstrapButton : HtmlBootstrapComponent<BootstrapButton>, IHtmlString
    {
        public BootstrapButton()
        {
            Builder = new TagBuilder("input");
            Builder.MergeAttribute("type", "button");
            Builder.AddCssClass("btn");
        }

        public BootstrapButton ButtonType(ButtonType value)
        {
            Builder.AddCssClass(value.GetDescription());
            return this;
        }

        public BootstrapButton ButtonSize(ButtonSize value)
        {
            Builder.AddCssClass(value.GetDescription());
            return this;
        }

        public BootstrapButton Value(string value)
        {
            Builder.MergeAttribute("value", value);
            return this;
        }

        public BootstrapButton DataBind(string bindings)
        {
            Builder.MergeAttribute("data-bind", bindings);
            return this;
        }

        public string ToHtmlString()
        {
            return Builder.ToString(TagRenderMode.SelfClosing);
        }
    }

    public enum ButtonType
    {
        [Description("btn-primary")]
        Primary,
        [Description("btn-default")]
        Default,
        [Description("btn-info")]
        Info,
        [Description("btn-warning")]
        Warning,
        [Description("btn-danger")]
        Danger,
        [Description("btn-success")]
        Success

    }

    public enum ButtonSize
    {
        [Description("btn-sm")]
        Small,
        [Description("btn-lg")]
        Large,
        [Description("btn-xs")]
        ExtraSmall,
    }

    public static class HtmlComponents
    {
        public static BootstrapButton BootstrapButton(this HtmlHelper helper)
        {
            return new BootstrapButton();
        }
    }
}
