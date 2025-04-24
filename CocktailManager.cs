using System.Collections.Generic;
using System.Text;

namespace AppliLeCrocodile
{
    internal class CocktailManager
    {
        public static CocktailManager Instance { get; private set; }

        public static void Initialize()
        {
            if (Instance == null)
            {
                Instance = new CocktailManager();
            }
        }

        private const string ingredientsPath = "ingredients.json";
        private const string cocktailsPath = "cocktails.json";

        private Cocktail[] cocktails;

        public Ingredient[] ingredients { get; private set; }

        private CocktailManager()
        {
            LoadIngredients();
            LoadCocktails();
        }

        #region Verification

        private void VerifyCocktailAndIngredient()
        {
            List<string> ingredientsIDNotFoundLanguageManager = new List<string>();
            List<string> ingredientsIDNotFoundInIngredients = new List<string>();
            List<string> cockNotFound = new List<string>();

            foreach (Cocktail cock in cocktails)
            {
                string text = LanguageManager.Instance.GetText(cock.nameID);
                if (text == null || text == string.Empty)
                {
                    cockNotFound.Add(cock.nameID);
                }

                foreach (string ingredientID in cock.ingredientsNameID)
                {
                    text = LanguageManager.Instance.GetText(ingredientID);
                    if (text == null || text == string.Empty)
                    {
                        ingredientsIDNotFoundLanguageManager.Add(ingredientID);
                    }

                    Ingredient ingredient = GetIngredientByID(ingredientID);
                    if (ingredient.nameID != ingredientID)
                    {
                        ingredientsIDNotFoundInIngredients.Add(ingredientID);
                    }
                }
            }

            string ToString<T>(List<T> lst)
            {
                if (lst == null || lst.Count == 0)
                    return "[]";

                StringBuilder sb = new StringBuilder("[");
                foreach (T t in lst)
                {
                    sb.Append(t != null ? t.ToString() : "null");
                    sb.Append(", ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append("]");
                return sb.ToString();
            }

            string error1 = ToString(ingredientsIDNotFoundLanguageManager);
            string error2 = ToString(ingredientsIDNotFoundInIngredients);
            string error3 = ToString(cockNotFound);
        }

        #endregion

        public void Start()
        {
            #if DEBUG
            VerifyCocktailAndIngredient();
            #endif
        }

        private void LoadIngredients()
        {
            IngredientsSave ingredientsSave = JsonUtility.DeserializeFromSave<IngredientsSave>(ingredientsPath);
            ingredients = ingredientsSave.ingredients;
        }

        private void LoadCocktails()
        {
            CocktailsSave cocktailSave = JsonUtility.DeserializeFromSave<CocktailsSave>(cocktailsPath);

            int CompareCocktail(Cocktail c1, Cocktail c2)
            {
                return LanguageManager.Instance.GetText(c1.nameID).CompareTo(LanguageManager.Instance.GetText(c2.nameID));
            }

            cocktails = cocktailSave.cocktails;
            Array.Sort(cocktails, CompareCocktail);
        }

        public Ingredient GetIngredientByID(string nameID) => Array.Find(ingredients, (Ingredient i) => i.nameID == nameID);

        public Cocktail[] GetCocktails(CocktailFilter? filter)
        {
            if(!filter.HasValue)
                return (Cocktail[])cocktails.Clone();

            List<Cocktail> res = new List<Cocktail>(cocktails.Length);
            foreach (Cocktail cocktail in cocktails)
            {
                if(filter.Value.IsValidCocktail(cocktail))
                    res.Add(cocktail);
            }

            return res.ToArray();
        }
    }
}
