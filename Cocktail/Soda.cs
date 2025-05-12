
namespace AppliLeCrocodile
{
    public struct Soda
    {
        public string nameID { get; set; }
        public string priceID { get; set; }

        public Soda(string nameID, string priceID)
        {
            this.nameID = nameID;
            this.priceID = priceID;
        }

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Soda soda)
                return nameID == soda.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator ==(Soda right, Soda left) => right.nameID == left.nameID;
        public static bool operator !=(Soda right, Soda left) => right.nameID != left.nameID;
    }

    public struct SodaSave
    {
        public Soda[] soda { get; set; }

        public SodaSave(Soda[] soda)
        {
            this.soda = soda;
        }
    }
}