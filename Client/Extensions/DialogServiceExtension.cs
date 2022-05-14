using MudBlazor;

namespace WheelOfFortune.Client.Extensions
{
    public static class DialogServiceExtension
    {
        public static async Task<bool?> Warning(this IDialogService dialogService, string textWarning)
        {
            var result = await dialogService.ShowMessageBox(
                "Осторожно",
                textWarning,
                yesText: "Продолжить!",
                noText: "Отмена");
            return result;
        }
    }
}
