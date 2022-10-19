using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.ValueObjects
{
    public sealed class Condition : ValueObject<Condition>
    {
        public Condition(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public string DisplayValue 
        {
            get 
            {
                return Value switch
                {
                    1 => "晴れ",
                    2 => "曇り",
                    3 => "雨",
                    _=> "不明"
                };
            }
        }

        protected override bool EquelsCore(Condition other)
        {
            return this.Value == other.Value;
        }
    }
}
