
using System.Diagnostics.CodeAnalysis;

namespace AppliLeCrocodile
{
    public struct Ingredient
    {
        public string nameID { get; set; }
        public string descriptionID { get; set; }
        public float alcoholVolume { get; set; }
        public bool isLiquid { get; set; }
        public bool isFruitJuice { get; set; }
        public string imagePath { get; set; }

        public Ingredient(string nameID, string descriptionID, float alcoholVolume, bool isLiquid, bool isFruitJuice, string imagePath)
        {
            this.nameID = nameID;
            this.descriptionID = descriptionID;
            this.alcoholVolume = alcoholVolume;
            this.isLiquid = isLiquid;
            this.isFruitJuice = isFruitJuice;
            this.imagePath = imagePath;
        }

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj))
                return false;

            if (obj is Ingredient ingredient)
                return nameID == ingredient.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator==(Ingredient right, Ingredient left) => right.nameID == left.nameID;
        public static bool operator!=(Ingredient right, Ingredient left) => right.nameID != left.nameID;
    }

    public struct IngredientsSave
    {
        public Ingredient[] ingredients { get; set; }

        public IngredientsSave(Ingredient[] ingredient)
        {
            this.ingredients = ingredient;
        }
    }
}
