using Global.UI.Localizations.Texts;
using Global.UI.Overlays.Abstract;

namespace Global.UI.Overlays.Runtime
{
    public class GlobalExceptionController : IGlobalExceptionController
    {
        public GlobalExceptionController(/*IGlobalExceptionView view, */LanguageTextData localization)
        {
            // _view = view;
            _localization = localization;
        }

        private readonly IGlobalExceptionView _view;
        private readonly LanguageTextData _localization;
        
        public void Show()
        {
            var error = _localization.GetText();
            _view.Show(error);
        }
    }
}