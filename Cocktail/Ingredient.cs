
namespace AppliLeCrocodile
{
    public struct Ingredient
    {
        public string nameID { get; set; }
        public string descriptionID { get; set; }
        public float alcoholVolume { get; set; }
        public bool isLiquid { get; set; }
        public string imagePath { get; set; }

        public Ingredient(string nameID, string descriptionID, float alcoholVolume, bool isLiquid, string imagePath)
        {
            this.nameID = nameID;
            this.descriptionID = descriptionID;
            this.alcoholVolume = alcoholVolume;
            this.isLiquid = isLiquid;
            this.imagePath = imagePath;
        }
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
