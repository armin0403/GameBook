using GameBook.Helpers.ToastHelper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GameBook.Controllers
{
    public class ToastController : Controller
    {
        private readonly ITempDataDictionary _tempData;
        public ToastController(IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempData)
        {
            _tempData = tempData.GetTempData(httpContextAccessor.HttpContext);
        }
        public IActionResult GetMessages()
        {
            if(!_tempData.TryGetValue(ToastService.Key, out var text))
                return Ok(string.Empty);

            var htmlString = new HtmlString($"{text}");
            if (htmlString.Value == null)
                return Ok(string.Empty);

            _tempData.Remove(ToastService.Key);

            return Ok(htmlString.ToString());
        }
    }
}
