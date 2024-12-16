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

        public override void ReadFromString(List<string> parts)
        {
            Type = parts[0].Trim('"');
            Date = DateTime.ParseExact(parts[1], "yyyy.MM.dd", null);
            Source = parts[2].Trim('"');
            Amount = Convert.ToInt32(parts[3]);
        }
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
