using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MudBlazor;
namespace WheelOfFortune.Client.Extensions
{
    public static class SnackbarExtension
    {
        public static ISnackbar Report(this ISnackbar snackbar, bool? result, string action)
        {
            if (result == true)
            {
                snackbar.Add("Успешно " + action, Severity.Success);
            }
            else
            {
                snackbar.Add("Ошибка", Severity.Error);
            }
            return snackbar;
        }
    }
}
