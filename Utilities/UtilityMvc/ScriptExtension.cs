﻿using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Utility
{
    public static class ViewModel
    {
        public static Script CreateJsViewModel(this HtmlHelper htmlhelper)
        {
            return new Script(htmlhelper.ViewData.Model);
        }

        public class Script : IHtmlString
        {
            private readonly object _model;
            private readonly SortedList<int, string> _registeredScriptIncludes = new SortedList<int, string>();
            private string _viewModelName;

            public Script(object model)
            {
                _model = model;
            }

            private string ContainerName { get; set; }

            public string ToHtmlString()
            {
                return Render().ToHtmlString();
            }

            public Script RegisterScript(string script)
            {
                if (!_registeredScriptIncludes.ContainsValue(script))
                {
                    _registeredScriptIncludes.Add(_registeredScriptIncludes.Count, script);
                }
                return this;
            }

            public Script ViewModelName(string name)
            {
                _viewModelName = name;
                if (!_registeredScriptIncludes.ContainsValue(name))
                {
                    _registeredScriptIncludes.Add(_registeredScriptIncludes.Count, name);
                }
                return this;
            }

            public Script Container(string containerName)
            {
                ContainerName = containerName;
                return this;
            }

            public MvcHtmlString Render()
            {
                var scripts = new StringBuilder();
                if (_registeredScriptIncludes.Count != 0)
                {
                    scripts.Append(RenderScripts());
                }

                if (ContainerName == null)
                {
                    return MvcHtmlString.Empty;
                }

                scripts.AppendLine("<script>");
                scripts.AppendLine("$(function(){");
                scripts.AppendLine("BYF." + _viewModelName + ".init(" + _model.ToJson() + ",$('" + ContainerName + "'))");
                scripts.AppendLine("});");
                scripts.AppendLine("</script>");
                return MvcHtmlString.Create(scripts.ToString());
            }

            private string RenderScripts()
            {
                var scripts = new StringBuilder();
                foreach (var script in _registeredScriptIncludes.Values)
                {
                    scripts.AppendLine("<script src='/Scripts/View/" + script + ".js' type='text/javascript'></script>");
                }
                return scripts.ToString();
            }
        }
    }


    public static class ScriptExtension
    {
        private static readonly SortedList<int, string> RegisteredScriptIncludes = new SortedList<int, string>();

        public static void RegisterScriptInclude(this HtmlHelper htmlhelper, string script)
        {
            if (!RegisteredScriptIncludes.ContainsValue(script))
            {
                RegisteredScriptIncludes.Add(RegisteredScriptIncludes.Count, script);
            }
        }

        public static string RenderScript(this HtmlHelper htmlhelper, string script)
        {
            var scripts = new StringBuilder();
            scripts.AppendLine("<script src='" + script + "' type='text/javascript'></script>");
            return scripts.ToString();
        }

        public static string RenderScripts(this HtmlHelper htmlhelper)
        {
            var scripts = new StringBuilder();
            scripts.AppendLine("<!-- Rendering registered script includes -->");
            foreach (var script in RegisteredScriptIncludes.Values)
            {
                scripts.AppendLine("<script src='" + script + "' type='text/javascript'></script>");
            }
            return scripts.ToString();
        }

        public static string CreateScript(string script)
        {
            var scripts = new StringBuilder();
            scripts.AppendLine("<script src='" + script + "' type='text/javascript'></script>");
            return scripts.ToString();
        }
    }
}