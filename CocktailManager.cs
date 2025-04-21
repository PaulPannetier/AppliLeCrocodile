
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

        public void Start()
        {

        }

        private void LoadIngredients()
        {
            IngredientsSave ingredientsSave = JsonUtility.DeserializeFromSave<IngredientsSave>(ingredientsPath);
            ingredients = ingredientsSave.ingredients;
        }

        private void LoadCocktails()
        {
            CocktailsSave cocktailSave = JsonUtility.DeserializeFromSave<CocktailsSave>(cocktailsPath);
            cocktails = cocktailSave.cocktails;
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
