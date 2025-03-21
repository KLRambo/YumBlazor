using Microsoft.JSInterop;

namespace YumBlazor.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ToastrSuccess(this IJSRuntime Ijsruntime, string message)
        {
            await Ijsruntime.InvokeVoidAsync("ShowToastr", "success", message);
        }
        public static async Task ToastrError(this IJSRuntime Ijsruntime, string message)
        {
            await Ijsruntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

    }
}
