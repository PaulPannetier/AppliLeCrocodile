

using System.Collections.Specialized;

namespace AppliLeCrocodile
{
    public struct Snack
    {
        public string nameID { get; set; }
        public string priceID { get; set; }

        public Snack(string nameID, string priceID)
        {
            this.nameID = nameID;
            this.priceID = priceID;
        }

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Snack snack)
                return nameID == snack.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator ==(Snack right, Snack left) => right.nameID == left.nameID;
        public static bool operator !=(Snack right, Snack left) => right.nameID != left.nameID;
    }

    public struct SnackSave
    {
        public Snack[] snacks { get; set; }

        public SnackSave(Snack[] snacks)
        {
            this.snacks = snacks;
        }
    }
}