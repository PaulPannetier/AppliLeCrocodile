using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace AppliLeCrocodile
{
    internal class LanguageManager
    {
        public static LanguageManager Instance { get; private set; }

        public static void Initialize()
        {
            if (Instance == null)
            {
                Instance = new LanguageManager();
            }
        }

        public const string defaultLanguage = "Français";
        private const string languageDirectoryPath = "Language";
        private FrozenDictionary<string, string> textData;
        private FrozenDictionary<string, string> defaultTextData;

        public readonly string[] availableLanguages = [
            "Français",
            "English"
        ];

        public string _currentLanguage;
        public string currentLanguage
        {
            get => _currentLanguage;
            set
            {
                if(_currentLanguage == value || !availableLanguages.Contains(value)) 
                    return;

                LoadLanguage(value);
                _currentLanguage = value;
            }
        }

        private LanguageManager()
        {
            LoadDefaultLanguage();
            currentLanguage = defaultLanguage;
        }

        public void Start()
        {

        }

        private Dictionary<string, string> GetLanguageDict(string language)
        {
            string languageFilePath = $"{languageDirectoryPath}/{language}/texts.json";
            LanguageTextsData textsData = JsonUtility.DeserializeFromSave<LanguageTextsData>(languageFilePath);

            if (language != textsData.language)
            {
                Console.WriteLine($"LanguageManager.GetLanguageDict error, read language is : {textsData.language} but given language is {language}");
            }

            Dictionary<string, string> dict = new Dictionary<string, string>(textsData.datas.Length);

            foreach (LanguageTextData textData in textsData.datas)
            {
                dict.Add(textData.key, textData.value);
            }
            return dict;
        }

        private void LoadLanguage(string language)
        {
            Dictionary<string, string> languageDict = GetLanguageDict(language);
            textData = languageDict.ToFrozenDictionary();
        }

        private void LoadDefaultLanguage()
        {
            Dictionary<string, string> languageDict = GetLanguageDict(defaultLanguage);
            defaultTextData = languageDict.ToFrozenDictionary();
        }

        private string Resolve(string text)
        {
            string GetConstanteReplacement(Match match)
            {
                string res = string.Empty;
                SettingsManager.Instance.TryGetConstante(match.Groups[1].ToString(), out res);
                return res;
            }

            const string regexPattern = @"\<([\w\d]+?)\>"; // Ex: "SuperSpell has an impedance of only <superspell_impedance> !"
            string res = text;
            res = Regex.Replace(res, regexPattern, GetConstanteReplacement);
            return res;
        }

        public string GetText(string textID)
        {
            string text = string.Empty;
            if(textData.TryGetValue(textID, out text))
            {
                return Resolve(text);
            }
            Console.WriteLine($"Text with the ID : {textID} and the language {currentLanguage} are not found");

            if (defaultTextData.TryGetValue(textID, out text))
            {
                return Resolve(text);
            }

            Console.WriteLine($"Text with the ID : {textID} and the language {defaultLanguage} are not found");
            return string.Empty;
        }

        #region private struct

        public struct LanguageTextsData
        {
            public string language { get; set; }
            public LanguageTextData[] datas { get; set; }

            public LanguageTextsData(string language, LanguageTextData[] datas) : this()
            {
                this.language = language;
                this.datas = datas;
            }
        }

        public struct LanguageTextData
        {
            public string key { get; set; }
            public string value { get; set; }

            public LanguageTextData(string key, string value)
            {
                this.key = key;
                this.value = value;
            }
        }

        #endregion
    }
}
