using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncomeLaba
{
    public abstract class BasicIncomeType : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public abstract void ReadFromString(List<string> parts);
        public abstract string LineOutput();
    }
    public class Income : BasicIncomeType
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public int Amount { get; set; }

        /// <summary>
        /// Заполняет свойства текущего экземпляра объекта, используя данные из списка строк.
        /// </summary>
        /// <param name="parts">Список строк, содержащий информацию для заполнения свойств.</param>
        /// <remarks>
        /// Ожидается, что список <paramref name="parts"/> содержит как минимум четыре элемента, которые
        /// соответствуют следующим свойствам:
        /// 1. Тип дохода (строка)
        /// 2. Дата дохода (строка в формате "yyyy.MM.dd")
        /// 3. Источник дохода (строка)
        /// 4. Сумма дохода (строка, представляющая целое число)
        /// </remarks>
        /// <exception cref="FormatException">Вызывает исключение, если данные не могут быть правильно 
        /// распознаны или преобразованы, например, при неправильном формате даты или некорректном 
        /// представлении суммы.</exception>
        public override void ReadFromString(List<string> parts)
        {
            Type = parts[0].Trim('"');
            Date = DateTime.ParseExact(parts[1], "yyyy.MM.dd", null);
            Source = parts[2].Trim('"');
            Amount = Convert.ToInt32(parts[3]);
        }
        /// <summary>
        /// Форматирует и выводит информацию об объекте в виде строки.
        /// </summary>
        /// <returns>Строку, содержащую информацию о типе дохода, дате, источнике и сумме.</returns>
        /// <remarks>
        /// Форматированная строка включает следующие элементы:
        /// - Тип дохода
        /// - Дата (в формате "yyyy.MM.dd")
        /// - Источник дохода
        /// - Сумма в рублях, обозначенная символом "р"
        /// </remarks>
        public override string LineOutput()
        {
            return $"{Type}:\n Дата: {Date.ToString("yyyy.MM.dd")}\n Источник: {Source}\n Сумма: {Amount} р\n";
        }
    }
    public class CompanyIncome : BasicIncomeType
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public int Amount { get; set; }
        public string TypeOfOperation { get; set; }

        public override void ReadFromString(List<string> parts)
        {
            Type = parts[0].Trim('"');
            Date = DateTime.ParseExact(parts[1], "yyyy.MM.dd", null);
            Source = parts[2].Trim('"');
            Amount = Convert.ToInt32(parts[3]);
            TypeOfOperation = parts[4].Trim('"');
        }
        public override string LineOutput()
        {
            return $"{Type}:\n Дата: {Date.ToString("yyyy.MM.dd")}\n Источник: {Source}\n Сумма: {Amount} р\n Тип операции: {TypeOfOperation}\n";
        }
    }
    public class PersonalIncome : BasicIncomeType
    {
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public int Amount { get; set; }
        public string SenderName { get; set; }

        public override void ReadFromString(List<string> parts)
        {
            Type = parts[0].Trim('"');
            Date = DateTime.ParseExact(parts[1], "yyyy.MM.dd", null);
            Source = parts[2].Trim('"');
            Amount = Convert.ToInt32(parts[3]);
            SenderName = parts[4].Trim('"');
        }
        public override string LineOutput()
        {
            return $"{Type}:\n Дата: {Date.ToString("yyyy.MM.dd")}\n Источник: {Source}\n Сумма: {Amount} р\n Имя отправителя: {SenderName}\n";
        }
    }
}
