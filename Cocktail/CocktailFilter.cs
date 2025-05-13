
namespace AppliLeCrocodile
{
    internal interface ICocktailFilter
    {
        public bool IsValidCocktail(in Cocktail cocktail);
    }

    internal class PatternCocktailFilter : ICocktailFilter
    {
        private string pattern;

        public PatternCocktailFilter(string pattern)
        {
            this.pattern = pattern;
        }

        public bool IsValidCocktail(in Cocktail cocktail)
        {
            string cocktailName = LanguageManager.Instance.GetText(cocktail.nameID);
            return cocktailName.Contains(pattern, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    internal class NameCocktailFilter : ICocktailFilter
    {
        private string[] ingredientsNameID;

        public NameCocktailFilter(string[] ingredientsNameID)
        {
            this.ingredientsNameID = ingredientsNameID;
        }

        public bool IsValidCocktail(in Cocktail cocktail)
        {
            foreach (string ingredientNameID in ingredientsNameID)
            {
                if(!cocktail.ingredientsNameID.Contains(ingredientNameID))
                    return false;
            }
            return true;
        }
    }
}
