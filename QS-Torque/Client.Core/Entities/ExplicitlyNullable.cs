namespace Core.Entities
{
    public class ExplicitlyNullable<T>
    {
        public ExplicitlyNullable(object value)
        {
            if (value!= null)
            {
                this.Value = (T)value;
                _hasValue = true;
            }
            else
            {
                _hasValue = false;
            }
        }

        public bool HasValue()
        {
            return _hasValue;
        }

        public T GetValueOrDefault(T defaultValue)
        {
            if (_hasValue)
            {
                return Value;
            }
            return defaultValue;
        }

        public T Value { get; set; }
        private bool _hasValue;
    }
}
