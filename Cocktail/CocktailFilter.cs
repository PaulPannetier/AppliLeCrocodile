
namespace AppliLeCrocodile
{
    internal struct CocktailFilter
    {
        private string[] ingredientsNameID;

        public CocktailFilter(string[] ingredientsNameID)
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
