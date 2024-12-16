using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IncomeLaba;
using System.Globalization;

namespace IncomeTest
{
    [TestClass]
    public class TestForCorrectStringProcessing
    {
        [TestMethod]
        public void TestStringEnteredCorrectly()
        {
            string expected = "Дохды компании:\n Дата: 2023.05.01\n Источник: Сдача гаража в аренду\n Сумма: 35000 р\n Тип операции: Перевод на сбер\n";
            string str = "\"Доходы компании\" 2023.05.01 \"Сдача гаража в аренду\" 35000 \"Перевод на сбер\"";

            string actual = FactoryIncome.fromStr(str).LineOutput();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_IncomeCreatedCorrectly()
        {
            string str = "\"Доходы\" 2023.05.01 \"Сдача гаража в аренду\" 35000";

            var expectedobj = new Income()
            {
                Type = "Доходы",
                Date = DateTime.ParseExact("2023.05.01", "yyyy.MM.dd", CultureInfo.InvariantCulture),
                Source = "Сдача гаража в аренду",
                Amount = 35000
            };

            var actualobj = FactoryIncome.fromStr(str);

            Assert.ReferenceEquals(expectedobj, actualobj);
        }

        [TestMethod]
        public void Test_CompanyIncomeCreatedCorrectly()
        {
            string str = "\"Доходы компании\" 2023.05.01 \"Сдача гаража в аренду\" 35000 \"Перевод на сбер\"";

            var expectedobj = new CompanyIncome()
            {
                Type = "Доходы компании",
                Date = DateTime.ParseExact("2023.05.01", "yyyy.MM.dd", null),
                Source = "Сдача гаража в аренду",
                Amount = 35000,
                TypeOfOperation = "Перевод на сбер"
            };

            var actualobj = FactoryIncome.fromStr(str);

            Assert.ReferenceEquals(expectedobj, actualobj);
        }

        [TestMethod]
        public void Test_PersonalIncomeCreatedCorrectly()
        {
            string str = "\"Доходы физ.лица\" 2023.05.01 \"Сдача гаража в аренду\" 35000 \"Иванов И.И.\"";

            var expectedobj = new PersonalIncome()
            {
                Type = "Доходы физ.лица",
                Date = DateTime.ParseExact("2023.05.01", "yyyy.MM.dd", null),
                Source = "Сдача гаража в аренду",
                Amount = 35000,
                SenderName = "Иванов И.И."
            };

            var actualobj = FactoryIncome.fromStr(str);

            Assert.ReferenceEquals(expectedobj, actualobj);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), "Строка не распознана как действительное значение DateTime.")]
        public void TestNotCorrectDateFormatV1()
        {
            string str = "\"Доходы компании\" 223 \"Сдача гаража в аренду\" 35000";
            FactoryIncome.fromStr(str);
        }

        [TestMethod]
        public void TestNotCorrectDateFormatV2()
        {
            string expected = "Строка не распознана как действительное значение DateTime.";
            string str = "\"Доходы компании\" 223 \"Сдача гаража в аренду\" 35000";
            Exception exception = null;

            try { FactoryIncome.fromStr(str); }
            catch (Exception ex) { exception = ex; }

            Assert.AreEqual(expected, exception.Message);
        }

        [TestMethod]
        public void TestEmptyString()
        {
            string str = "    ";

            var exception = Assert.ThrowsException<Exception>(() => FactoryIncome.fromStr(str));

            Assert.AreEqual("Пустая строка", exception.Message);
        }

        [TestMethod]
        public void Test_UnknownTypeOfIncome()
        {
            string expected = "Неизвестный тип дохода: Доходы ИП";
            string str = "\"Доходы ИП\" 2024.10.10 \"Продажа мышек WLMouse\" 235000";

            var exception = Assert.ThrowsException<Exception>(() => FactoryIncome.fromStr(str));

            Assert.AreEqual(expected, exception.Message);
        }

        [TestMethod]
        public void Test_InvalidStringFormat()
        {
            string expected = "Входная строка имела неверный формат.";
            string str = "\"Доходы компании\" 2024.10.10 Продажа мышек WLMouse 235000 \"Перевод на сбер\"";

            var exception = Assert.ThrowsException<FormatException>(() => FactoryIncome.fromStr(str));

            Assert.AreEqual(expected, exception.Message);
        }
    }
}
