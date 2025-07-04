using System.Collections.Frozen;

namespace AppliLeCrocodile
{
    internal class SettingsManager
    {
        public static SettingsManager Instance { get; private set; }

        public static void Initialize()
        {
            if(Instance == null)
            {
                Instance = new SettingsManager();
            }
        }

        private const string settingsFilePath = "settings.json";
        private const string constantesFilePath = "constantes.json";

        private SettingsData settingsData;
        private FrozenDictionary<string, string> constantes;

        public bool firstTImeLauch => settingsData.firstTimeLaunch;

        private SettingsManager() 
        {
            if (!JsonUtility.DeserializeFromAppData(settingsFilePath, out settingsData))
            {
                settingsData = new SettingsData(LanguageManager.defaultLanguage, true);
            }

            ConstantesData constantesData = JsonUtility.DeserializeFromSave<ConstantesData>(constantesFilePath);
            Dictionary<string, string> constantesDict = new Dictionary<string, string>(constantesData.dataConstantes.Length);
            foreach (ConstantesData.DataConstante constante in constantesData.dataConstantes)
            {
                constantesDict.Add(constante.key, constante.value);
            }
            constantes = constantesDict.ToFrozenDictionary();
        }

        public void Start()
        {
            ApplicationManager.Instance.onSleepCallback += OnSleep;
            if(!LanguageManager.Instance.availableLanguages.Contains(settingsData.language))
            {
                settingsData.language = LanguageManager.defaultLanguage;
            }
            LanguageManager.Instance.currentLanguage = settingsData.language;
        }

        private void OnSleep()
        {
            settingsData.firstTimeLaunch = false;
            JsonUtility.SerializeToAppData(settingsData, settingsFilePath);
        }

        public bool TryGetConstante(string constanteID, out string constant) => constantes.TryGetValue(constanteID, out constant);

        public struct SettingsData
        {
            public string language { get; set; }
            public bool firstTimeLaunch { get; set; }

            public SettingsData(string language, bool firstTimeLaunch)
            {
                this.language = language;
                this.firstTimeLaunch = firstTimeLaunch;
            }
        }

        public struct ConstantesData
        {
            public DataConstante[] dataConstantes { get; set; }

            public ConstantesData(DataConstante[] dataConstantes)
            {
                this.dataConstantes = dataConstantes;
            }

            public struct DataConstante
            {
                public string key { get; set; }
                public string value { get; set; }

                public DataConstante(string key, string value)
                {
                    this.key = key;
                    this.value = value;
                }
            }
        }
    }
}
