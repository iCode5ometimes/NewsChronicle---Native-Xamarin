using System.Collections.Generic;
using NewsChronicle.Data.Interfaces;

namespace NewsChronicle.Data.ViewModel
{
    public class AppSettingsViewModel : BaseViewModel
    {
        #region Fields and Properties

        private Dictionary<string, string> _optionDict = new Dictionary<string, string>()
        {
            {"English",    "en" },
            {"Deustch",    "de" },
            {"Dutch",      "nl" },
            {"French",     "fr" },
            {"Hebrew",     "he" },
            {"Italian",    "it" },
            {"Norwegian",  "no" },
            {"Portuguese", "pt" },
            {"Romanian",   "ro" },
            {"Russian",    "ru" },
            {"Spanish",    "es" },
        };
        public Dictionary<string, string> OptionDict => _optionDict;

        #endregion

        #region Constructor(s) (and Dependencies)

        public AppSettingsViewModel(IAppLanguageSetting appLanguageSetting) : base(appLanguageSetting)
        {
        }

        #endregion
    }
}
