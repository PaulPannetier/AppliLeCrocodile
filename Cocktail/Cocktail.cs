
namespace AppliLeCrocodile
{   
    public struct Cocktail
    {
        public string nameID { get; set; }
        public string imagePath { get; set; }
        public string[] ingredientsNameID { get; set; }
        public Cocktail(string nameID, string imagePath, string[] ingredientsNameID)
        {
            this.nameID = nameID;
            this.imagePath = imagePath;
            this.ingredientsNameID = ingredientsNameID;
        }
    }

    public struct CocktailsSave
    {
        public Cocktail[] cocktails {  get; set; }

        public CocktailsSave(Cocktail[] cocktails)
        {
            this.cocktails = cocktails;
        }
    }
}
