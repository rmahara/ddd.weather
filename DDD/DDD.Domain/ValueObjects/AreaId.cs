namespace DDD.Domain.ValueObjects
{
    public sealed class AreaId : ValueObject<AreaId>
    {
        public AreaId(int value)
        {
            this.Value = value;
        }
        public int Value { get; }

        protected override bool EquelsCore(AreaId other)
        {
            return Value == other.Value;
        }

        public string DisplayValue 
        {
            get 
            {
                return Value.ToString().PadLeft(4, '0');
            }
        }
    }
}
