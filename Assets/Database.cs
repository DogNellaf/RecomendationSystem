using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public static class Database
{
    private static string connectionString = @"Server=.\SQLEXPRESS;Database=RecomendationSystem;User ID=obama;Password=obama";
    public static List<Type> Types => GetTypes();
    private static List<Type> GetTypes()
    {
        List<Type> types = new List<Type>();
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM dbo.Type";
            var table = new DataTable();
            var command = new SqlCommand(query);
            using (var adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(table);
            }
            connection.Close();

            foreach (DataRow row in table.Rows)
            {
                Type type = new Type(row.ItemArray);
                types.Add(type);
            }
        }
        return types;
    }
}
