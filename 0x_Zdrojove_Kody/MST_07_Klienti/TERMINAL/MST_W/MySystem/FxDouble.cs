using System;
using System.Collections.Generic;
using System.Text;

namespace Fask.MST_W.MySystem
{
    public class FxDouble
    {
        private const int shift = 4;
        private decimal value = 0;

        public FxDouble(double d)
        {
            value = to_fx(d);
        }

        public FxDouble(decimal d)
        {
            value = d;
        }

        public FxDouble(FxDouble fxdouble)
        {
            value = fxdouble.value;
        }

        private static decimal to_fx(double d)
        {
            return System.Convert.ToDecimal(Math.Round(d * Math.Pow(10, shift)));
        }

        private static double to_fp(decimal i)
        {
            return System.Convert.ToDouble((double)i * Math.Pow(10, -shift));
        }

        public static FxDouble operator+ (FxDouble a, FxDouble b)
        {
            return new FxDouble(a.value + b.value);
        }

        public static FxDouble operator- (FxDouble a, FxDouble b)
        {
            return new FxDouble(a.value - b.value);
        }

        public static FxDouble operator* (FxDouble a, FxDouble b)
        {
            return new FxDouble(a.value * b.value / Convert.ToDecimal(Math.Round(Math.Pow(10, shift))));
        }

        public static FxDouble operator/ (FxDouble a, FxDouble b)
        {
            return new FxDouble((a.value * Convert.ToDecimal(Math.Round(Math.Pow(10, shift)))) / b.value);
        }

        public static bool operator <(FxDouble a, FxDouble b)
        {
            return (a.value < b.value);
        }

        public static bool operator >(FxDouble a, FxDouble b)
        {
            return (a.value > b.value);
        }

        public static bool operator ==(FxDouble a, FxDouble b)
        {
            return (a.value == b.value);
        }

        public static bool operator !=(FxDouble a, FxDouble b)
        {
            return (a.value != b.value);
        }

        public static bool operator <=(FxDouble a, FxDouble b)
        {
            return (a.value <= b.value);
        }

        public static bool operator >=(FxDouble a, FxDouble b)
        {
            return (a.value >= b.value);
        }

        public static explicit operator double(FxDouble a)
        {
            return to_fp(a.value);
        }

        public static explicit operator decimal(FxDouble a)
        {
            return Convert.ToDecimal(a.value);
        }

        public override string ToString()
        {
            return ((double)this).ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
