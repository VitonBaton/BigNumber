using System;
using System.Collections.Generic;
using System.Linq;

namespace BigNumberOperations
{
    public class BigNumber : IComparable, IComparable<BigNumber>, IEquatable<BigNumber>
    {

        /* ------------------------------- Constants --------------------------------- */

        public static readonly BigNumber Zero = 0;

        public static readonly BigNumber One = 1;


        /* -------------------------------- Fields ----------------------------------- */

        private readonly List<int> number;

        private bool sign = true;

        private int b;


        /* ------------------------------ Properties --------------------------------- */

        public int Length => number.Count;

        public int this[int index] => number[index];


        /* -------------------------- Public constructors ---------------------------- */

        public BigNumber(string numberInString, int b)
        {
            if (numberInString.Length == 0)
            {
                throw new ArgumentNullException(numberInString, "Cannot create number from empty string");
            }

            number = new List<int>();
            if (numberInString[0] == '-')
            {
                sign = false;
                numberInString = numberInString.TrimStart('-');
            }
            foreach (var c in numberInString)
            {
                number.Add(CharToInt(c));
            }
            number.Reverse();
            this.b = b;
        }

        public BigNumber(string numberInString) : this(numberInString, 10) {}

        public BigNumber(int value)
        {
            number = new List<int>();
            if (value == 0)
            {
                number.Add(0);
            }
            else
            {
                if (value < 0)
                {
                    sign = false;
                }
                value = Math.Abs(value);
                while (value > 0)
                {
                    number.Add(value % 10);
                    value /= 10;
                }
            }
            b = 10;
        }

        public BigNumber()
        {
            number = new List<int>();
            b = 10;
        }


        /* ----------------- Convertations to any number systems --------------------- */

        private static char IntToChar(int c)
        {
            if (c is >= 0 and <= 9)
            {
                return (char)(c + '0');
            }

            return (char)(c + 'A' - 10);
        }

        private static int CharToInt(char c)
        {
            if (c is >= '0' and <= '9')
            {
                return c - '0';
            }

            return c - 'A' + 10;
        }

        private int NextNumber(int final)
        {
            var temp = 0;
            for (var i = number.Count - 1; i >= 0; i--)
            {
                temp = temp * b + number[i];
                number[i] = temp / final;
                temp %= final;
            }
            return temp;
        }

        public BigNumber ConvertTo(int final)
        {
            BigNumber ret = new()
            {
                b = final
            };
            var temp = Copy();
            do
            {
                ret.number.Add(temp.NextNumber(final));
            } while (!temp.EqualsToZero());
            ret.sign = sign;
            return ret;
        }


        /* ------------------------- Public operators -------------------------------- */

        public static implicit operator BigNumber(int value) => new (value);

        public static implicit operator BigNumber(string value) => new(value);

        public static BigNumber operator + (BigNumber first, BigNumber second)
        {
            return first.Add(second);
        }

        public static BigNumber operator -(BigNumber first, BigNumber second)
        {
            return first.Sub(second);
        }

        public static BigNumber operator *(BigNumber first, BigNumber second)
        {
            return first.Mul(second);
        }

        public static BigNumber operator /(BigNumber first, BigNumber second)
        {
            return first.Div(second);
        }

        public static BigNumber operator %(BigNumber first, BigNumber second)
        {
            return first.FindRemainder(second);
        }

        public static BigNumber operator ++(BigNumber first)
        {
            return first.Inc();
        }

        public static BigNumber operator --(BigNumber first)
        {
            return first.Dec();
        }

        public static BigNumber operator <<(BigNumber first, int value)
        {
            return first.ShiftL(value);
        }

        public static BigNumber operator >>(BigNumber first, int value)
        {
            return first.ShiftR(value);
        }

        public static BigNumber operator ^(BigNumber first, BigNumber second)
        {
            return first.Xor(second);
        }

        public static bool operator ==(BigNumber first, BigNumber second)
        {
            return first is not null && first.CompareTo(second) == 0;
        }

        public static bool operator !=(BigNumber first, BigNumber second)
        {
            return first is not null && first.CompareTo(second) != 0;
        }

        public static bool operator <(BigNumber first, BigNumber second)
        {
            return first.CompareTo(second) == -1;
        }

        public static bool operator >(BigNumber first, BigNumber second)
        {
            return first.CompareTo(second) == 1;
        }

        public static bool operator <=(BigNumber first, BigNumber second)
        {
            return first.CompareTo(second) != 1;
        }

        public static bool operator >=(BigNumber first, BigNumber second)
        {
            return first.CompareTo(second) != -1;
        }


        /*------------------------- Basic math operations ---------------------------- */

        public BigNumber Add(BigNumber other)
        {

            switch (sign)
            {
                case true when !other.sign:
                    other.sign = true;
                    return Sub(other);
                case false when other.sign:
                    sign = true;
                    return other.Sub(this);
            }

            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            BigNumber addend;
            BigNumber ret;
            if (Abs(this) >= Abs(tempOther))
            {
                ret = Copy();
                addend = tempOther.Copy();
            }
            else
            {
                ret = tempOther.Copy();
                addend = Copy();
            }
            if (!addend.EqualsToZero())
            {
                addend.number.TrimEnd(0);
            }
            var c = 0;
            var size = ret.number.Count;

            for (var i = addend.number.Count; i < size; i++)
            {
                addend.number.Add(0);
            }
            for (var i = 0; i < size; i++)
            {
                var sum = ret.number[i] + addend.number[i] + c;
                c = sum / b;
                ret.number[i] = sum % b;
            }
            if (c > 0)
            {
                ret.number.Add(c);
            }
            else
            {
                ret.number[size - 1] += c;
            }
            return ret;
        }

        public BigNumber Sub(BigNumber other)
        {
            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            BigNumber ret;
            BigNumber deductible;
            bool newSign;

            switch (sign)
            {
                case true when !other.sign:
                    other.sign = true;
                    return Add(other);
                case false when other.sign:
                    other.sign = false;
                    return other.Add(this);
            }


            if (Abs(this) >= Abs(tempOther))
            {
                ret = Copy();
                deductible = tempOther.Copy();
                newSign = sign;
            }
            else
            {
                ret = tempOther.Copy();
                deductible = Copy();
                newSign = !sign;
            }

            var c = 0;
            for (var i = 0; i < ret.number.Count; i++)
            {
                int sum;
                if (i < deductible.number.Count)
                    sum = ret.number[i] - deductible.number[i] - c;
                else
                    sum = ret.number[i] - c;
                c = (sum >= 0) ? 0 : 1;
                ret.number[i] = ((sum + b) % b);
            }
            if (!ret.EqualsToZero())
                ret.number.TrimEnd(0);
            ret.sign = newSign;
            return ret;
        }

        public BigNumber Mul(BigNumber other)
        {
            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            BigNumber ret = new() {b = b};

            var length = number.Count + tempOther.number.Count;
            for (var i = 0; i < length; i++)
            {
                ret.number.Add(0);
            }
            for (var i = 0; i < tempOther.number.Count; i++)
            {
                var c = 0;
                for (var j = 0; j < number.Count; j++)
                {
                    var temp = ret.number[i + j] + number[j] * tempOther.number[i] + c;
                    ret.number[i + j] = temp % b;
                    c = temp / b;
                }
                ret.number[i + number.Count] = c;
            }
            if (!ret.EqualsToZero())
                ret.number.TrimEnd(0);
            else
            {
                ret = Zero.Copy();
            }
            ret.sign = !(sign ^ other.sign);
            return ret;
        }

        public BigNumber ShiftL(int value)
        {
            var ret = Copy();
            ret.number.Reverse();
            for (var i = 0; i < value; i++)
                ret.number.Add(0);
            ret.number.Reverse();
            return ret;
        }

        public BigNumber ShiftR(int value)
        {
            var ret = Copy();
            ret.number.RemoveRange(0, value);
            return ret;
        }

        public BigNumber Xor(BigNumber other)
        {
            bool wasConverted = false;
            BigNumber tempThis;
            if (this.b != 2)
            {
                tempThis = this.ConvertTo(2);
                wasConverted = true;
            }
            else
            {
                tempThis = this.Copy();
            }

            var tempOther = tempThis.b != other.b ? other.ConvertTo(tempThis.b) : other.Copy();
            BigNumber ret;
            if (tempThis.Length > tempOther.Length)
            {
                ret = tempThis.Copy();
            }
            else
            {
                ret = tempOther.Copy();
                tempOther = tempThis.Copy();
            }
            var size = tempOther.Length;
            for (var i = 0; i < size; i++)
            {
                ret.number[i] ^= tempOther[i];
            }

            if (!ret.EqualsToZero())
            {
                ret.number.TrimEnd(0);
            }

            if (wasConverted)
            {
                ret = ret.ConvertTo(this.b);
            }
            return ret;
        }

        public BigNumber Dec()
        {
            return this.Sub(One);
        }

        public BigNumber Inc()
        {
            return this.Add(One);
        }

        public BigNumber Div(BigNumber other)
        {
            if (other.EqualsToZero())
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }

            var tempThis = Copy();
            var tempOther = tempThis.b != other.b ? other.ConvertTo(b) : other.Copy();

            if (tempOther.number.Count < 2)
            {
                tempOther = other.ConvertTo(2);
                tempThis = ConvertTo(2);
            }

            if (tempOther.EqualsToOne())
            {
                return Copy();
            }

            var ret = new BigNumber
            {
                b = tempThis.b
            };
            var length = tempThis.number.Count - tempOther.number.Count;
            for (var i = 0; i <= length; i++)
            {
                ret.number.Add(0);
            }
            var k = tempOther.number.Count;
            // normalizing
            var d = tempThis.b / (tempOther.number[^1] + 1);
            // u = u*d
            var u = tempThis.Mul2(new BigNumber(d.ToString(), tempThis.b));
            // v = v*d
            var v = tempOther.Mul(new BigNumber(d.ToString(), tempThis.b));
            for (var i = tempThis.number.Count; i >= k; i--)
            {
                // trialQ
                var trialQ = Math.Min((tempThis.b * u.number[i] + u.number[i - 1]) / v.number[k - 1], tempThis.b - 1);
                // fixing
                while ((trialQ * (v.number[k - 1] * tempThis.b + v.number[k - 2])) > (tempThis.b * tempThis.b * u.number[i] + tempThis.b * u.number[i - 1] + u.number[i - 2]))
                    trialQ -= 1;
                // corrective addiction
                u = u.Sub2(v.ShiftL(i - k).Mul(new BigNumber(IntToChar(trialQ).ToString(), b)));

                if (!u.sign)
                {
                    u = v.ShiftL(i - k).Add(u);
                    trialQ -= 1;
                }

                ret.number[i - k] = trialQ;
            }
            if (!ret.EqualsToZero())
                ret.number.TrimEnd(0);
            ret.sign = ret.sign = !(sign ^ other.sign);
            return ret.ConvertTo(b);
        }

        public BigNumber FindRemainder(BigNumber other)
        {
            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            var temp = Div(tempOther).Mul(tempOther);
            var ret = Sub(temp);

            return ret.EqualsToZero() ? 0 : ret;
        }

        public static BigNumber Abs (BigNumber number)
        {
            var ret = number.Copy();
            ret.sign = true;
            return ret;
        }

        public BigNumber Pow(BigNumber power)
        {
            if (!power.sign)
            {
                throw new ArgumentException("Power must be positive or zero", nameof(power));
            }

            var binaryM = power.ConvertTo(2);

            List<BigNumber> binaryDegrees = new()
            {
                Copy()
            };

            for (var i = 1; i < binaryM.Length; i++)
            {
                binaryDegrees.Add(binaryDegrees[i - 1] * binaryDegrees[i - 1]);
            }
            BigNumber ret = 1;
            for (var i = 0; i < binaryM.Length; i++)
            {
                if (binaryM[i] == 1)
                {
                    ret *= binaryDegrees[i];
                }
            }
            return ret;
        }

        public static BigNumber Random(BigNumber min, BigNumber max)
        {
            Random rand = new();
            var tempMin = !min.sign ? Zero.Copy() : min.Copy();

            var size = rand.Next(tempMin.number.Count, max.number.Count);
            BigNumber ret;
            do
            {
                ret = new BigNumber()
                { 
                    b = min.b
                };

                for (var i = 0; i < size; i++)
                {
                    ret.number.Add(rand.Next(0, min.b));
                }
                if (!ret.EqualsToZero())
                    ret.number.TrimEnd(0);
            } while (ret > max || ret < tempMin);

            return ret;
        }

        /*-------------- Helping private math operations for division ---------------- */

        private BigNumber Mul2(BigNumber other)
        {
            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();
            var ret = new BigNumber {b = b};

            var length = number.Count + tempOther.number.Count;
            for (var i = 0; i < length; i++)
            {
                ret.number.Add(0);
            }
            for (var i = 0; i < tempOther.number.Count; i++)
            {
                var c = 0;
                for (var j = 0; j < number.Count; j++)
                {
                    var temp = ret.number[i + j] + number[j] * tempOther.number[i] + c;
                    ret.number[i + j] = temp % b;
                    c = temp / b;
                }
                ret.number[i + number.Count] = c;
            }
            return ret;
        }

        private BigNumber Sub2(BigNumber other)
        {
            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            BigNumber ret;
            BigNumber deductible;
            var newSign = true;
            if (this >= tempOther)
            {
                ret = Copy();
                deductible = tempOther.Copy();
            }
            else
            {
                ret = tempOther.Copy();
                ret.number.TrimEnd(0);
                deductible = Copy();
                newSign = false;
            }
            var c = 0;
            for (var i = 0; i < ret.number.Count; i++)
            {
                int sum;
                if (i < deductible.number.Count)
                    sum = ret.number[i] - deductible.number[i] - c;
                else
                    sum = ret.number[i] - c;
                c = sum >= 0 ? 0 : 1;
                ret.number[i] = ((sum + b) % b);
            }
            ret.sign = newSign;
            return ret;
        }

        /*------------------------ Additional math operations ------------------------ */

        public bool MillerRabinTest(int T)
        {
            var decN = Dec();
            var two = b > 2 ? new BigNumber("2", b) : new BigNumber("10", b);
            var s = 0;
            var r = decN.Copy();
            while (r.IsDivisibleByTwo())
            {
                r /= two;
                s++;
            }
            for (var t = 1; t <= T; t++)
            {
                var u = Random(two, decN.Dec());
                var v = u.DegreeRemainder(r, this);
                if (v.EqualsToOne() || v.Equals(decN)) continue;
                var key = true;
                for (var i = 1; i <= s - 1 && key; i++)
                {
                    v = v.Mul(v).FindRemainder(this);
                    if (v.EqualsToOne())
                    {
                        return false;
                    }

                    if (v.Equals(decN))
                    {
                        key = false;
                    }
                }
                if (key)
                    return false;
            }
            return true;
        }

        public bool IsDivisibleByTwo()
        {
            if (b % 2 == 0)
                return (number[0] % 2) == 0;
            var sum = number.Sum();
            return (sum % 2) == 0;
        }

        public BigNumber DegreeRemainder(BigNumber power, BigNumber m)
        {
            var binaryM = power.ConvertTo(2);

            List<BigNumber> binaryDegrees = new()
            {
                this % m
            };

            for (var i = 1; i < binaryM.Length; i++)
            {
                binaryDegrees.Add((binaryDegrees[i - 1] * binaryDegrees[i - 1]) % m);
            }
            BigNumber ret = 1;
            for (var i = 0; i < binaryM.Length; i++)
            {
                if (binaryM[i] != 1) continue;
                ret *= binaryDegrees[i];
                ret %= m;
            }
            return ret;
        }

        public static List<(BigNumber, BigNumber)> Factorization(BigNumber n)
        {
            List<(BigNumber, BigNumber)> ret = new();
            BigNumber div = 2;
            while (n > One)
            {
                if ((n % div).Equals(Zero))
                {
                    var temp = (div, new BigNumber(1));
                    while ((n % div).Equals(Zero))
                    {
                        temp.Item2 *= div;
                        n /= div;
                    }
                    ret.Add(temp);
                }
                div++;
            }
            return ret;
        }

        public static BigNumber EilerFunc(BigNumber n)
        {
            var factorization = Factorization(n);
            return factorization.Aggregate<(BigNumber, BigNumber), BigNumber>(1, (current, val)
                => current * (val.Item2 - val.Item2 / val.Item1));
        }

        public static BigNumber EilerFunc(BigNumber first, BigNumber second)
        {
            return first.Dec() * second.Dec();
        }

        public static (BigNumber, BigNumber) IdentityBezu(BigNumber a, BigNumber b)
        {
            BigNumber temp;
            var key = false;
            if (a < b)
            {
                (a, b) = (b, a);
                key = true;
            }

            List<BigNumber> coefficient = new();

            while ((temp = a % b) > 0)
            {
                coefficient.Add(a / b);
                a = b;
                b = temp;
            }
            var ret = (new BigNumber(0), new BigNumber(1));
            for (var i = coefficient.Count - 1; i >= 0; i--)
            {
                temp = ret.Item1;
                ret.Item1 = ret.Item2;
                ret.Item2 = temp - ret.Item2 * coefficient[i];
            }

            if (key)
            {
                (ret.Item1, ret.Item2) = (ret.Item2, ret.Item1);
            }

            return ret;
        }

        public static BigNumber NodEuclidean(BigNumber a, BigNumber b)
        {
            BigNumber temp;
            if (a < b)
            {
                (a, b) = (b, a);
            }
            while (!(temp = a % b).EqualsToZero())
            {
                a = b.Copy();
                b = temp.Copy();
            }
            return b;
        }

        public static BigNumber InverseBezu(BigNumber n, BigNumber m)
        {
            n %= m;

            if (NodEuclidean(m, n) != 1)
            {
                return -1;
            }

            var temp = IdentityBezu(m, n);
            return (temp.Item2 + m) % m;
        }

        /* ------------------------------ Compare methods ---------------------------- */

        public bool EqualsToZero()
        {
            var ret = true;
            var i = 0;
            while (ret && i < number.Count)
            {
                ret = number[i] == 0;
                i++;
            }
            return ret;
        }

        public bool EqualsToOne()
        {
            return (number.Count == 1 && number[0] == 1);
        }

        public bool Equals(BigNumber other)
        {
            if (other is null)
            {
                throw new NullReferenceException("Parameter cannot be null");
            }

            if (sign != other.sign)
            {
                return false;
            }

            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            var ret = true;
            if (number.Count == tempOther.number.Count)
            {
                for (var i = 0; i < number.Count && ret; i++)
                {
                    ret = number[i] == tempOther.number[i];
                }
            }
            else
            {
                return false;
            }

            return ret;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            var temp = obj as BigNumber;

            return temp != null && Equals(temp);
        }

        public int CompareTo (BigNumber other)
        {
            if (other is null)
            {
                return 1;
            }

            switch (this.sign)
            {
                case true when !other.sign:
                    return 1;
                case false when other.sign:
                    return -1;
            }

            var returnSign = (this.sign) ? 1 : -1;


            var tempOther = b != other.b ? other.ConvertTo(b) : other.Copy();

            var tempThis = Copy();
            if (!tempThis.EqualsToZero())
                tempThis.number.TrimEnd(0);
            else
                tempThis = Zero.Copy();
            if (!tempOther.EqualsToZero())
                tempOther.number.TrimEnd(0);
            else
                tempOther = Zero.Copy();

            if (tempThis.number.Count < tempOther.number.Count)
            {
                return -1 * returnSign;
            }

            if (tempThis.number.Count > tempOther.number.Count)
            {
                return 1 * returnSign;
            }


            for (var i = tempThis.number.Count - 1; i >= 0; i--)
            {
                if (tempThis.number[i] > tempOther.number[i])
                {
                    return 1 * returnSign;
                }
                if (tempThis.number[i] < tempOther.number[i])
                {
                    return -1 * returnSign;
                }
            }

            return 0;
        }

        public int CompareTo(object obj)
        {
            if (obj is not BigNumber other)
            {
                throw new ArgumentException("Cannot compare objects");
            }
            return CompareTo(other);
        }


        /* ------------------------------- Other methods ----------------------------- */

        public override int GetHashCode()
        {
            return number.Aggregate(0, (current, val) => current ^ val);
        }

        public override string ToString()
        {
            var ret = "";
            if (!sign)
                ret += '-';
            for (var i = number.Count - 1; i >= 0; i--)
            {
                ret += IntToChar(number[i]);
            }
            return ret;
        }

        public BigNumber Copy()
        {
            BigNumber ret = new() {b = b};
            foreach (var d in number)
            {
                ret.number.Add(d);
            }
            ret.sign = sign;
            return ret;
        }
    }

    internal static class ListExtension
    {
        public static void TrimEnd(this List<int> list, int d)
        {
            var count = 0;
            var iterator = list.Count;
            while (list[iterator - 1] == d)
            {
                count++;
                iterator--;
            }
            list.RemoveRange(iterator, count);
        }
    }
}