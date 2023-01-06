using HugeInteger.Library;
using NUnit.Framework;

namespace HugeInteger.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        #region Add Operations
        [Test]
        public void Add_Zero_Positive_Thousand_PositiveResult_0()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ONE_THOUSAND);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num1 + num2).ToString();

            //Assert
            var expected = (long.Parse(num1Str) + long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Add_Zero_Positive_Thousand_PositiveResult_1()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ONE_THOUSAND);

            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num2 + num1).ToString();

            //Assert
            var expected = (long.Parse(num2Str) + long.Parse(num1Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Add_Zero_Zero_ZeroResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ZERO);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num1 + num2).ToString();
            var expected = (long.Parse(num1Str) + long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Add_Same_Positive_PositiveResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);
            var num2 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);

            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num1 + num2).ToString();
            var expected = (long.Parse(num1Str) + long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Subtract Operations
        [Test]
        public void Subtract_Zero_Positive_Thousand_NegativeResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ONE_THOUSAND);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num1 - num2).ToString();
            var expected = (long.Parse(num1Str) - long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Subtract_Zero_Positive_Thousand_PositiveResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ONE_THOUSAND);

            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = (num2 - num1).ToString();

            //Assert
            var expected = (long.Parse(num2Str) - long.Parse(num1Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Subtract_Zero_Zero_ZeroResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ZERO);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = Worker.SubtractTwoNumbers(num1, num2).ToString();
            var expected = (long.Parse(num1Str) - long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Subtract_Same_Positive_ZeroResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);
            var num2 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);

            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = Worker.SubtractTwoNumbers(num1, num2).ToString();
            var expected = (long.Parse(num1Str) - long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }
        #endregion


        #region Multiply Operaitons
        [Test]
        public void Multiply_Zero_Zero_ZeroResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.ZERO);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = Worker.Multiply(num1, num2).ToString();
            var expected = (long.Parse(num1Str) * long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Multiply_Zero_Positive_ZeroResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ZERO);
            var num2 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = Worker.Multiply(num1, num2).ToString();
            var expected = (long.Parse(num1Str) * long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Multiply_Positive_Positive_PositiveResult()
        {
            //Arrenge
            var num1 = Worker.GetNum(PositiveNumbers.ONE_MILION);
            var num2 = Worker.GetNum(PositiveNumbers.NORMAL_RANDOM);
            var num1Str = num1.ToString().Replace(",", "");
            var num2Str = num2.ToString().Replace(",", "");

            //Act
            var res = Worker.Multiply(num1, num2).ToString();
            var expected = (long.Parse(num1Str) * long.Parse(num2Str)).ToString();
            var actual = res.ToString().Replace(",", "");
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}