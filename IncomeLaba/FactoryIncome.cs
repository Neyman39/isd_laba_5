using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace IncomeLaba
{
    public class FactoryIncome
    {
        public static string[] StrFromFiles(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            return fileContent.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<string> ParseString(string input)
        {
            return Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                         .Cast<Match>()
                         .Select(m => m.Value)
                         .ToList();
        }

        public static BasicIncomeType fromStr(string str)
        {
            Dictionary<string, BasicIncomeType> strToIncome = new Dictionary<string, BasicIncomeType> {
                { "Доходы", new Income() },
                { "Доходы компании", new CompanyIncome() },
                { "Доходы физ.лица", new PersonalIncome() }
            };

            var parts = ParseString(str);
            if (parts.Count == 0)
            {
                throw new Exception("Пустая строка");
            }
            string firstWord = parts[0].Trim('"');

            if (!strToIncome.ContainsKey(firstWord))
            {
                throw new Exception($"Неизвестный тип дохода: {firstWord}");
            }
            strToIncome[firstWord].ReadFromString(parts);

            return (BasicIncomeType)strToIncome[firstWord].Clone();
        }

        public static List<BasicIncomeType> ListToObjects(string[] lists)
        {
            List<BasicIncomeType> ObjectsList = new List<BasicIncomeType>();
            foreach (string str in lists)
            {
                try
                {
                    ObjectsList.Add(fromStr(str));
                }
                catch (Exception ex)
                {
                    using (StringWriter writer = new StringWriter())
                    {
                        writer.WriteLine($"Ошибка при обработке строки: {ex.Message}");
                        Console.WriteLine(writer.ToString());
                    }
                }
            }
            return ObjectsList;
        }
    }
}
