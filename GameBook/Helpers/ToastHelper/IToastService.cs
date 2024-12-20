using Microsoft.AspNetCore.Html;

namespace GameBook.Helpers.ToastHelper
{
    public interface IToastService
    {
        void Success (string message);
        void Danger (string message);
        void Warning (string message);
        IHtmlContent Display();
    }
}
