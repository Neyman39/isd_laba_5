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
        /// <summary>
        /// Читает содержимое текстового файла и разбивает его на строки.
        /// </summary>
        /// <param name="filePath">Путь к файлу, который нужно прочитать.</param>
        /// <returns>Массив строк, содержащих строки, считанные из файла.</returns>
        /// <exception cref="System.IO.FileNotFoundException">Вызывается, если файл не найден по указанному пути.</exception>
        /// <exception cref="System.IO.IOException">Вызывается, если произошла ошибка ввода-вывода при чтении файла.</exception>
        public static string[] StrFromFiles(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            return fileContent.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Парсит входную строку и разделяет её на подстроки по пробелам,
        /// учитывая при этом строковые значения в кавычках.
        /// </summary>
        /// <param name="input">Входная строка, содержащая значения, разделенные пробелами.</param>
        /// <returns>Список строк, представляющий подстроки, извлеченные из входной строки.</returns>
        /// <example>
        /// Пример использования:
        /// <code>
        /// var result = ParseString(@"Найдите ""строка 1"" и строка 2 и строка 3.");
        /// </code>
        /// Результат будет содержать три элемента: "строка 1", "и", "строка 2", "и", "строка 3."
        /// </example>
        public static List<string> ParseString(string input)
        {
            return Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                         .Cast<Match>()
                         .Select(m => m.Value)
                         .ToList();
        }

        /// <summary>
        /// Преобразует строковое представление типа дохода в соответствующий объект типа <see cref="BasicIncomeType"/>.
        /// </summary>
        /// <param name="str">Строка, представляющая тип дохода. Ожидается, что она содержит первое слово, соответствующее одному из 
        /// следующих типов: "Доходы", "Доходы компании", "Доходы физ.лица".</param>
        /// <returns>
        /// Возвращает объект типа <see cref="BasicIncomeType"/>, который соответствует типу дохода, указанному в строке.
        /// </returns>
        /// <exception cref="Exception">Вызывает исключение, если строка пустая или если тип дохода не найден в словаре.</exception>
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

        /// <summary>
        /// Преобразует массив строковых представлений типов доходов в список объектов типа <see cref="BasicIncomeType"/>.
        /// </summary>
        /// <param name="lists">Массив строковых представлений, каждая из которых представляет тип дохода.</param>
        /// <returns>
        /// Возвращает список объектов типа <see cref="BasicIncomeType"/>, созданных на основе переданных строк.
        /// </returns>
        /// <remarks>
        /// Если возникает ошибка при обработке строки, сообщение об ошибке выводится в консоль, и обработка продолжается для остальных строк.
        /// </remarks>
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
