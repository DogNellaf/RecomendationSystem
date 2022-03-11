using Microsoft.Data.SqlClient;
using RecomendationSystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace RecommendationSystemCore.Helpers
{
    // класс работы с базой данных
    public static class Database
    {
        // строка соединения с базой данных
        private static string connectionString = @"Server=.\SQLEXPRESS;Database=RecomendationSystem;Trusted_Connection=True;TrustServerCertificate=true;";

        // универсальная функция для получения json по объекту Т (любой наследник класса Model)
        internal static string GetJson<T>(string where = "") where T: Model
        {
            // включаем поддержку небезопасных символов (кириллицы)
            JsonSerializerOptions options = new()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            // получает объекты
            List<T> objects = GetObject<T>(where);

            // конвертируем в json строку по настройкам выше
            string json = JsonSerializer.Serialize(objects, options);

            // удаляем лишние символы
            json = json.Replace("\r\n", "");
            json = json.Replace("  ", "");

            // возвращаем результат
            return json;
        }

        //функция получения объектов из базы, где Т - любой наследник класса Model
        private static List<T> GetObject<T>(string where = "") where T : Model
        {
            // создаем пустой список объектов
            List<T> objects = new();

            // проверяем, есть ли условие
            string query = $"SELECT * FROM [RecomendationSystem].[dbo].[{typeof(T).Name}]";
            if (where != "")
            {
                query = $"SELECT * FROM [RecomendationSystem].[dbo].[{typeof(T).Name}] where {where}";
            }

            // кидаем запрос на выборку
            DataTable table = SendSQL(query);

            // проходимся по каждой строчке таблицы-результата
            foreach (DataRow row in table.Rows)
            {
                // в конструктор передаем единственный параметр - все столбцы строки
                var parameters = new object[1];
                parameters[0] = row.ItemArray;

                // создаем новый объект класса Т
                T element = (T)Activator.CreateInstance(typeof(T), parameters);

                // добавляем в список
                objects.Add(element);
            }

            //возвращаем результат
            return objects;
        }

        // функция отправки SQL запроса
        private static DataTable SendSQL(string query)
        {
            // пустая таблица
            DataTable result = new();

            // пытаемся выполнить кол
            try
            {
                // используя соединение, выполняем дальнейшие команды
                using var connection = new SqlConnection(connectionString);

                // создаем SQL команду по тексту
                SqlCommand command = new(query, connection);

                // Создаем считывающий элемент
                SqlDataAdapter adapter = new(command);

                // заполняем таблицу
                adapter.Fill(result);

            }

            // если словили ошибку
            catch (Exception ex)
            {
                // закрываем соединение
                Console.WriteLine(ex.Message);
            }

            // возвращаем результат - таблицу
            return result;
        }
    }
}
