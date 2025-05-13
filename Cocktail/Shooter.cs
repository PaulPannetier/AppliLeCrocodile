
namespace AppliLeCrocodile
{
    public struct Shoother
    {
        public string nameID { get; set; }

        public Shoother(string nameID)
        {
            this.nameID = nameID;
        }

        public override string ToString() => nameID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Shoother shoot)
                return nameID == shoot.nameID;

            return false;
        }

        public override int GetHashCode() => nameID.GetHashCode();

        public static bool operator ==(Shoother right, Shoother left) => right.nameID == left.nameID;
        public static bool operator !=(Shoother right, Shoother left) => right.nameID != left.nameID;
    }

    public struct ShootherSave
    {
        public Shoother[] shooters { get; set; }

        public ShootherSave(Shoother[] shooters)
        {
            this.shooters = shooters;
        }
    }
}