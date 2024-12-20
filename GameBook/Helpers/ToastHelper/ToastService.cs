using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GameBook.Helpers.ToastHelper
{
    public class ToastService : IToastService
    {
        public enum ToastMessageType
        {
            Success,
            Error,
            Warning
        }

        public const string Key = "_temp_data_toast_message";
        private const string SweetAlertTemplate = "Swal.fire({{icon: '{0}', title: '{1}'}})";

        private readonly ITempDataDictionary _tempData;

        public ToastService(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempData = tempDataDictionaryFactory.GetTempData(httpContextAccessor.HttpContext);
        }
        public void Danger(string message)
        {
            Message(ToastMessageType.Error, message);
        }      

        public void Success(string message)
        {
            Message(ToastMessageType.Success, message);
        }

        public void Warning(string message)
        {
            Message(ToastMessageType.Warning, message);
        }

        private void Message(ToastMessageType type, string message)
        {
            if(_tempData.ContainsKey(Key))
                _tempData.Remove(Key);

            var icon = type switch
            {
                ToastMessageType.Success => "success",
                ToastMessageType.Warning => "warning",
                ToastMessageType.Error => "error",
                _ => "info"
            };

            _tempData.Add(Key, string.Format(SweetAlertTemplate, icon, message));
        }

        public IHtmlContent Display()
        {
            if (!_tempData.TryGetValue(Key, out var text))
                return HtmlString.Empty;

            var htmlString = new HtmlString($"<script>{text}</script>");
            if (htmlString.Value == null)
                return HtmlString.Empty;

            _tempData.Remove(Key);

            return htmlString;
        }

    }
}
