

using System.Collections.Specialized;

namespace AppliLeCrocodile
{
    public struct Beer
    {
        public string nameID { get; set; }
        public string imagePath { get; set; }
        public string descriptionID { get; set; }

        public Beer(string nameID, string imagePath, string descriptionID)
        {
            this.nameID = nameID;
            this.imagePath = imagePath;
            this.descriptionID = descriptionID;
        }

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Beer beer)
                return nameID == beer.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator ==(Beer right, Beer left) => right.nameID == left.nameID;
        public static bool operator !=(Beer right, Beer left) => right.nameID != left.nameID;
    }

    public struct BeerSave
    {
        public Beer[] beers { get; set; }

        public BeerSave(Beer[] beers)
        {
            this.beers = beers;
        }
    }
}