namespace RegionsAPI.Data
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Country Country { get; set; }

        protected bool Equals(Region other)
        {
            return Id == other.Id;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Region)obj);
        }
        public override int GetHashCode()
        {
            return Id;
        }
    }
}
