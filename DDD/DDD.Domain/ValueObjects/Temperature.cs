﻿using DDD.Domain.Helpers;

namespace DDD.Domain.ValueObjects
{
    public sealed class Temperature : ValueObject<Temperature>
    {
        public const string UnitName = "℃";
        public const int DecimalPoint = 2;
        
        public Temperature(float value)
        {
            Value = value;
        }
        
        public float Value { get; }

        public string DisplayValue
        {
            get
            {
                return FloatHelper.RoundString(Value, DecimalPoint);
            }
        }

        public string DisplayValueWithUnit
        {
            get
            {
                return FloatHelper.RoundString(Value, DecimalPoint) + UnitName;
            }
        }
        public string DisplayValueWithUnitSpace
        {
            get
            {
                return FloatHelper.RoundString(Value, DecimalPoint) + " " + UnitName;
            }
        }

        protected override bool EquelsCore(Temperature other)
        {
            return Value == other.Value;
        }
    }
}
