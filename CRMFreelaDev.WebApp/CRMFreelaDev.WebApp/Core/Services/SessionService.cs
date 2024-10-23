using Microsoft.JSInterop;

namespace CRMFreelaDev.WebApp.Core.Services;

public class SessionService
{

    private readonly IJSRuntime _jsRuntime;
    private const string SessionTokenKey = "sessionToken";

    public SessionService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetSessionTokenAsync(string token)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", SessionTokenKey, token);
    }

    public async Task<string> GetSessionTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", SessionTokenKey);
    }

    public async Task ClearSessionAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", SessionTokenKey);
    }
}
