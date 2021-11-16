#pragma checksum "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4f48b7154d2de1ac3a3f9d811fdbe3a2ace11575"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorPoll.Client.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using BlazorPoll.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using BlazorPoll.Client.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using BlazorPoll.Client.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using BlazorPoll.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>Questions</h3>");
#nullable restore
#line 6 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
 if (Questions.Count == 0)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(1, "<span>It\'s Empty!</span>");
#nullable restore
#line 9 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(2, "table");
            __builder.AddAttribute(3, "class", "table");
            __builder.AddMarkupContent(4, "<thead><tr><th>Id</th>\r\n                <th>Question</th>\r\n                <th>Single Choice</th></tr></thead>\r\n        ");
            __builder.OpenElement(5, "tbody");
#nullable restore
#line 21 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
             foreach (var q in Questions)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(6, "tr");
            __builder.OpenElement(7, "td");
            __builder.AddContent(8, 
#nullable restore
#line 24 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
                         q.Id

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(9, "\r\n                    ");
            __builder.OpenElement(10, "td");
            __builder.AddContent(11, 
#nullable restore
#line 25 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
                         q.Text

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(12, "\r\n                    ");
            __builder.OpenElement(13, "td");
            __builder.AddContent(14, 
#nullable restore
#line 26 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
                         q.SingleChoice

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 28 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 31 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 33 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
      
    [Parameter]public List<Question> Questions { get; set; } = new List<Question>();
    protected override async Task OnInitializedAsync()
    {
        Questions = await QuestionService.GetQuestions();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IQuestionService QuestionService { get; set; }
    }
}
#pragma warning restore 1591
