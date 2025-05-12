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
        private const string softsPath = "softs.json";
        private const string beersPath = "beers.json";
        private const string snacksPath = "snacks.json";
        private const string sodaPath = "soda.json";

        private Cocktail[] cocktails;
        private Cocktail[] softs;
        private Ingredient[] fruitJuices;
        private Beer[] beers;
        private Snack[] snacks;
        private Soda[] sodas;

        public Ingredient[] ingredients { get; private set; }

        private CocktailManager()
        {
            LoadIngredients();
            LoadCocktails();
            LoadSofts();
            LoadBeers();
            LoadSnacks();
            LoadSodas();
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
            List<Ingredient> fruitJuiceLst = new List<Ingredient>(ingredients.Length);
            foreach (Ingredient ingredient in ingredients)
            {
                if(ingredient.isFruitJuice)
                    fruitJuiceLst.Add(ingredient);
            }

            int FruitJuiceCompare(Ingredient right, Ingredient left)
            {
                string rightStr = LanguageManager.Instance.GetText(right.nameID);
                string leftStr = LanguageManager.Instance.GetText(left.nameID);
                return rightStr.CompareTo(leftStr);
            }

            fruitJuices = fruitJuiceLst.ToArray();
            Array.Sort(fruitJuices, FruitJuiceCompare);
        }

        private int CompareCocktail(Cocktail right, Cocktail left)
        {
            string rightStr = LanguageManager.Instance.GetText(right.nameID);
            string leftStr = LanguageManager.Instance.GetText(left.nameID);
            return rightStr.CompareTo(leftStr);
        }

        private void LoadCocktails()
        {
            CocktailsSave cocktailSave = JsonUtility.DeserializeFromSave<CocktailsSave>(cocktailsPath);
            cocktails = cocktailSave.cocktails;
            Array.Sort(cocktails, CompareCocktail);
        }

        private void LoadSofts()
        {
            int CompareSoft(Cocktail right, Cocktail left)
            {
                if(right.nameID == "MESANGE" && left.nameID == "MARABOUT")
                    return -1;
                if (right.nameID == "MARABOUT" && left.nameID == "MESANGE")
                    return 1;

                string rightStr = LanguageManager.Instance.GetText(right.nameID);
                string leftStr = LanguageManager.Instance.GetText(left.nameID);
                return rightStr.CompareTo(leftStr);
            }


            CocktailsSave cocktailSave = JsonUtility.DeserializeFromSave<CocktailsSave>(softsPath);
            softs = cocktailSave.cocktails;
            Array.Sort(softs, CompareSoft);
        }

        private void LoadBeers()
        {
            BeerSave beerSave = JsonUtility.DeserializeFromSave<BeerSave>(beersPath);
            beers = beerSave.beers;
        }

        private void LoadSnacks()
        {
            SnackSave snacksSave = JsonUtility.DeserializeFromSave<SnackSave>(snacksPath);
            snacks = snacksSave.snacks;
        }

        private void LoadSodas()
        {
            SodaSave sodaSave = JsonUtility.DeserializeFromSave<SodaSave>(sodaPath);
            sodas = sodaSave.soda;
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

        public Cocktail[] GetSofts(CocktailFilter? filter)
        {
            if (!filter.HasValue)
                return (Cocktail[])softs.Clone();

            List<Cocktail> res = new List<Cocktail>(softs.Length);
            foreach (Cocktail soft in softs)
            {
                if (filter.Value.IsValidCocktail(soft))
                    res.Add(soft);
            }

            return res.ToArray();
        }

        public Ingredient[] GetFruitJuice()
        {
            return (Ingredient[])fruitJuices.Clone();
        }

        public Beer[] GetBeers()
        {
            return (Beer[])beers.Clone();
        }

        public Snack[] GetSnacks()
        {
            return (Snack[])snacks.Clone();
        }

        public Soda[] GetSodas()
        {
            return (Soda[])sodas.Clone();
        }
    }
}
