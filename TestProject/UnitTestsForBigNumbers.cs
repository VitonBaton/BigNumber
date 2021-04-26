using System;
using System.Collections.Generic;
using System.Linq;
using BigNumberOperations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTestsForBigNumbers
    {
        [TestMethod]
        public void TestCompareBiggerWithSmaller()
        {
            BigNumberOperations.BigNumber bigger = 4;
            BigNumberOperations.BigNumber smaller = 2;
            var expected = 1;
            var result = bigger.CompareTo(smaller);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareSmallerWithBigger()
        {
            BigNumberOperations.BigNumber bigger = 4;
            BigNumberOperations.BigNumber smaller = 2;
            var expected = -1;
            var result = smaller.CompareTo(bigger);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareEquals()
        {
            BigNumberOperations.BigNumber number1 = 4;
            BigNumberOperations.BigNumber number2 = 4;
            var expected = 0;
            var result = number1.CompareTo(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareEqualsDifferentSigns()
        {
            BigNumberOperations.BigNumber number1 = 4;
            BigNumberOperations.BigNumber number2 = -4;
            var expected = false;
            var result = number1 == number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareSmallerWithBiggerNegative()
        {
            BigNumberOperations.BigNumber bigger = -2;
            BigNumberOperations.BigNumber smaller = -4;
            var expected = -1;
            var result = smaller.CompareTo(bigger);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareBiggerWithSmallerNegative()
        {
            BigNumberOperations.BigNumber bigger = -2;
            BigNumberOperations.BigNumber smaller = -4;
            var expected = 1;
            var result = bigger.CompareTo(smaller);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareBigNumberWithInt()
        {
            BigNumberOperations.BigNumber bigger = -2;
            var smaller = -4;
            var expected = 1;
            var result = bigger.CompareTo(smaller);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareBigNumberWithString()
        {
            BigNumberOperations.BigNumber bigger = -2;
            var smaller = "-4";
            var expected = 1;
            var result = bigger.CompareTo(smaller);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareWithZero()
        {
            BigNumberOperations.BigNumber number1 = 0;
            var expected = true;
            var result = number1 == BigNumber.Zero;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestCompareWhereObjectIsBigNumber()
        {
            BigNumber number1 = 10;
            BigNumber number2 = 8;
            var expected = 1;
            var result = number1.CompareTo((object)number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Cannot compare objects")]
        public void TestCompareBigNumberWithSomething()
        {
            BigNumberOperations.BigNumber bigger = -2;
            double smaller = -4;
            var result = bigger.CompareTo(smaller);
        }

        [TestMethod]
        public void TestEquals()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var number2 = "-2";
            var expected = true;
            var result = number1.Equals(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEqualsWithDifferentLength()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var number2 = "-22";
            var expected = false;
            var result = number1.Equals(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "Parameter cannot be null")]
        public void TestEqualsException()
        {
            BigNumberOperations.BigNumber number1 = -2;
            BigNumber number2 = null;
            var result = number1.Equals(number2);
        }

        [TestMethod]
        public void TestEqualsWithDifferentSigns()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var number2 = "2";
            var expected = false;
            var result = number1.Equals(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEqualsWithEqualNumbers()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var expected = true;
            var result = number1.Equals((object)number1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEqualsWithNull()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var expected = false;
            var result = number1.Equals((object)null);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Cannot divide by zero")]
        public void TestDivisionByZero()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var number2 = "0";
            var result = number1.Div(number2);
        }

        [TestMethod]
        public void TestDivisionByOne()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var expected = -2;
            var result = number1.Div(BigNumber.One);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDivisionWithDifferentBases()
        {
            BigNumber number1 = 23;
            var expected = 4;
            var result = number1.Div(new BigNumber("12", 3));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDivisionPositiveByNegative()
        {
            BigNumberOperations.BigNumber number1 = -2;
            var number2 = "2";
            var expected = -1;
            var result = number1.Div(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDivisionPositiveByPositive()
        {
            BigNumberOperations.BigNumber number1 = 777712;
            var number2 = "665";
            var expected = 777712 / 665;
            var result = number1.Div(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDivisionNegativeByPositive()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = -2;
            var result = number1.Div(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestDivisionNegativeByNegative()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "-2";
            var expected = 2;
            var result = number1.Div(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddictionPositiveAndPositive()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "2";
            var expected = 52;
            var result = number1.Add(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddictionPositiveAndNegative()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "-2";
            var expected = 48;
            var result = number1.Add(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddictionNegativeAndPositive()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "2";
            var expected = -48;
            var result = number1.Add(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddictionNegativeAndNegative()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "-2";
            var expected = -52;
            var result = number1.Add(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSubtractionPositiveAndPositive()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "2";
            var expected = 48;
            var result = number1.Sub(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSubtractionPositiveAndNegative()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "-2";
            var expected = 52;
            var result = number1.Sub(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSubtractionNegativeAndPositive()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "2";
            var expected = -52;
            var result = number1.Sub(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSubtractionNegativeAndNegative()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "-2";
            var expected = -48;
            var result = number1.Sub(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiplicationPositiveByPositive()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "2";
            var expected = 100;
            var result = number1.Mul(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiplicationPositiveByNegative()
        {
            BigNumberOperations.BigNumber number1 = 50;
            var number2 = "-2";
            var expected = -100;
            var result = number1.Mul(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiplicationNegativeByPositive()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "2";
            var expected = -100;
            var result = number1.Mul(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMultiplicationNegativeByNegative()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var number2 = "-2";
            var expected = 100;
            var result = number1.Mul(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRemainderPositiveByPositive()
        {
            BigNumberOperations.BigNumber number1 = 25;
            var number2 = "2";
            var expected = 25 % 2;
            var result = number1.FindRemainder(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRemainderPositiveByNegative()
        {
            BigNumberOperations.BigNumber number1 = 25;
            var number2 = "-2";
            var expected = 25 % -2;
            var result = number1.FindRemainder(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRemainderNegativeByPositive()
        {
            BigNumberOperations.BigNumber number1 = -25;
            var number2 = "2";
            var expected = -25 % 2;
            var result = number1.FindRemainder(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestRemainderNegativeByNegative()
        {
            BigNumberOperations.BigNumber number1 = -25;
            var number2 = "-2";
            var expected = -25 % -2;
            var result = number1.FindRemainder(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Cannot divide by zero")]
        public void TestRemainderDivisionByZero()
        {
            BigNumberOperations.BigNumber number1 = 25;
            var number2 = "0";
            var result = number1.FindRemainder(number2);
        }

        [TestMethod]
        public void TestPow()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = 25;
            var result = number1.Pow(number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Power must be positive or zero")]
        public void TestPowException()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "-2";
            var result = number1.Pow(number2);
        }

        [TestMethod]
        public void TestOperatorAddiction()
        {
            BigNumberOperations.BigNumber number1 = 6;
            var number2 = "4";
            var expected = 10;
            var result = number1 + number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorSubtraction()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = -7;
            var result = number1 - number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorMultiplication()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = -10;
            var result = number1 * number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorDivision()
        {
            BigNumberOperations.BigNumber number1 = 131;
            var number2 = "66";
            var expected = 131 / 66;
            var result = number1 / number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorRemainder()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = -5 % 2;
            var result = number1 % number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorIncrement()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var expected = -4;
            number1++;
            Assert.AreEqual(expected, number1);
        }

        [TestMethod]
        public void TestOperatorDecrement()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var expected = -6;
            number1--;
            Assert.AreEqual(expected, number1);
        }

        [TestMethod]
        public void TestOperatorEqual()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = false;
            var result = number1 == number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorNotEqual()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = true;
            var result = number1 != number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorBiggerThan()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = false;
            var result = number1 > number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorLesserThan()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "2";
            var expected = true;
            var result = number1 < number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorBiggerOrEqual1()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "-5";
            var expected = true;
            var result = number1 >= number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorBiggerOrEqual2()
        {
            BigNumberOperations.BigNumber number1 = -4;
            var number2 = "-5";
            var expected = true;
            var result = number1 >= number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorLesserOrEqual1()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "-5";
            var expected = true;
            var result = number1 <= number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorLesserOrEqual2()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = "-6";
            var expected = false;
            var result = number1 <= number2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorShiftLeft()
        {
            BigNumberOperations.BigNumber number1 = -50;
            var expected = -5000;
            var result = number1 << 2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOperatorShiftRight()
        {
            BigNumberOperations.BigNumber number1 = -5000;
            var expected = -50;
            var result = number1 >> 2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestConversionForLowBase()
        {
            BigNumberOperations.BigNumber number1 = -5;
            var number2 = new BigNumber("-101", 2);
            var expected = true;
            var result = number1.ConvertTo(2).ToString() == number2.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestConversionForBigBase()
        {
            BigNumberOperations.BigNumber number1 = 123;
            var number2 = new BigNumber("7B",16);
            var expected = true;
            var result = number1.ConvertTo(16).ToString() == number2.ToString();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestIndexer()
        {
            BigNumberOperations.BigNumber number1 = -5032;
            var expected = 5;
            Assert.AreEqual(expected, number1[3]);
        }

        [TestMethod]
        public void TestLengthProperty()
        {
            BigNumberOperations.BigNumber number1 = -5032;
            var expected = 4;
            Assert.AreEqual(expected, number1.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Cannot create number from empty string")]
        public void TestConstructorByEmptyString()
        {
            BigNumber test = "";
        }

        [TestMethod]
        public void TestRandom()
        {
            var expected = true;
            var result = BigNumber.Random(0, 10);
            var compare = result >= 0 && result <= 10;
            Assert.AreEqual(expected, compare);
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            BigNumber number1 = 18;
            var expected = 9;
            var result = number1.GetHashCode();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMillerRabinTestNotPrime()
        {
            BigNumber number1 = 18;
            var expected = false;
            var result = number1.MillerRabinTest(15);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMillerRabinTestPrime()
        {
            BigNumber number1 = 17;
            var expected = true;
            var result = number1.MillerRabinTest(15);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMillerRabinTestPrime2()
        {
            BigNumber number1 = 101;
            var expected = true;
            var result = number1.MillerRabinTest(15);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestMillerRabinTestNotPrime2()
        {
            BigNumber number1 = new("1729",10);
            var expected = false;
            var result = number1.ConvertTo(11).MillerRabinTest(1500);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestFactorization()
        {
            BigNumber number1 = 120;
            var expected = new List<(BigNumber, BigNumber)>() {(2, 8), (3, 3), (5, 5)};
            var result = BigNumber.Factorization(number1);
            var compare = expected.SequenceEqual(result);
            Assert.AreEqual(true, compare);
        }

        [TestMethod]
        public void TestEilerFunctionForAnyNumber()
        {
            BigNumber number1 = 120;
            var expected = 32;
            var result = BigNumber.EilerFunc(number1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEilerFunctionForTwoPrimes()
        {
            BigNumber number1 = 13;

            BigNumber number2 = 17;
            var expected = 12 * 16;
            var result = BigNumber.EilerFunc(number1, number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestIdentutyBezuFirstBiggerThanSecond()
        {
            BigNumber number1 = 5;

            BigNumber number2 = 2;
            var expected = (1,-2);
            var result = BigNumber.IdentityBezu(number1,number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestIdentutyBezuFirstLesserThanSecond()
        {
            BigNumber number1 = 2;

            BigNumber number2 = 5;
            var expected = (-2, 1);
            var result = BigNumber.IdentityBezu(number1, number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNodFirstBiggerThanSecond()
        {
            BigNumber number1 = 5;

            BigNumber number2 = 2;
            var expected = 1;
            var result = BigNumber.NodEuclidean(number1, number2);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNodFirstLesserThanSecond()
        {
            BigNumber number1 = 5;

            BigNumber number2 = 2;
            var expected = 1;
            var result = BigNumber.NodEuclidean(number2, number1);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestInverseBezuNumberHasInversed()
        {
            BigNumber module = 5;

            BigNumber number = 2;
            var expected = 3;
            var result = BigNumber.InverseBezu(number, module);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestInverseBezuNumberHasNotInversed()
        {
            BigNumber module = 4;

            BigNumber number = 2;
            var expected = -1;
            var result = BigNumber.InverseBezu(number, module);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestXor()
        {
            BigNumber temp = 193123;
            BigNumber temp2 = 102123;
            var expected = 193123 ^ 102123;
            var result = temp.Xor(temp2);
            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void TestXorOperator()
        {
            BigNumber temp = 193123;
            BigNumber temp2 = 1022123;
            var expected = 193123 ^ 1022123;
            var result = temp.ConvertTo(2) ^ temp2.ConvertTo(11);
            Assert.AreEqual(expected, result.ConvertTo(10));
        }
    }
}
