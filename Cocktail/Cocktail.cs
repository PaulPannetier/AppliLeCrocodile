
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

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Cocktail cocktail)
                return nameID == cocktail.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator ==(Cocktail right, Cocktail left) => right.nameID == left.nameID;
        public static bool operator !=(Cocktail right, Cocktail left) => right.nameID != left.nameID;
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
