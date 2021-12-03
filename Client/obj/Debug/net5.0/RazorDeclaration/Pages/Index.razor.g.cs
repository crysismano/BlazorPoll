// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
#nullable restore
#line 13 "D:\Work\Onlab\BlazorPoll\Client\_Imports.razor"
using Microsoft.AspNetCore.SignalR.Client;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase, IAsyncDisposable
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 35 "D:\Work\Onlab\BlazorPoll\Client\Pages\Index.razor"
      
    
    [Parameter] public List<Poll> Polls { get; set; } = new List<Poll>();
    
    private HubConnection hubConnection;
    
    protected override async Task OnInitializedAsync()
    {
        Polls = await PollService.GetPolls();
        hubConnection = new HubConnectionBuilder()
            .WithUrl(NavigationManager.ToAbsoluteUri("/pollhub"))
            .Build();
        await hubConnection.StartAsync();
    }

    private async Task StartPoll(Poll poll)
    {
        await hubConnection.SendAsync("SendPoll", poll);
        //TODO: Navigate to control page
    }

    public async ValueTask DisposeAsync()
    {
        if (hubConnection is not null)
        {
            await hubConnection.DisposeAsync();
        }
    }



#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IPollService PollService { get; set; }
    }
}
#pragma warning restore 1591
