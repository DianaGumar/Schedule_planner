#pragma checksum "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b604c20037b701917b4c0cb71fbdf1e02f9a791"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Task_TaskEdit), @"mvc.1.0.view", @"/Views/Task/TaskEdit.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9b604c20037b701917b4c0cb71fbdf1e02f9a791", @"/Views/Task/TaskEdit.cshtml")]
    public class Views_Task_TaskEdit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<PlannerLib.Model.User>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<p></p>\r\n<hr class=\"mb-2\">\r\n<h5 class=\"text-info\">all users</h5>\r\n\r\n<div class=\"container text-center m-2\">\r\n    <table class=\"table-striped\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 12 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 15 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 18 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 21 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 24 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Phone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 27 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
               Write(Html.DisplayNameFor(model => model.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n            </tr>\r\n        </thead>\r\n\r\n        <tbody class=\"text-left\">\r\n");
#nullable restore
#line 33 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
             foreach (var user in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 37 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Id));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 40 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 43 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Password));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 46 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 49 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Phone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 52 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
                   Write(Html.DisplayFor(modelItem => user.Role));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                    <td>\r\n                        <a asp-action=\"Edit\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1819, "\"", 1842, 1);
#nullable restore
#line 56 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
WriteAttributeValue("", 1834, user.Id, 1834, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Edit</a>\r\n                        <a asp-action=\"Delete\"");
            BeginWriteAttribute("asp-route-id", " asp-route-id=\"", 1900, "\"", 1923, 1);
#nullable restore
#line 57 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
WriteAttributeValue("", 1915, user.Id, 1915, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Delete</a>\r\n\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 61 "N:\VSTUDIO_PR\Planner\Planner2\Views\Task\TaskEdit.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<PlannerLib.Model.User>> Html { get; private set; }
    }
}
#pragma warning restore 1591