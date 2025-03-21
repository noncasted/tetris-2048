﻿using System;
using Global.Publisher.Abstract.Languages;

namespace Global.UI.Localizations.Runtime
{
    public class LanguageConverter : ILanguageConverter
    {
        public string ToString(Language language)
        {
            switch (language)
            {
                case Language.Ru:
                    return "Рус";
                case Language.Eng:
                    return "Eng";
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }
        }
    }
}