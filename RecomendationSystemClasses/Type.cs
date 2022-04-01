using System;
using System.Text.Json.Serialization;

namespace RecomendationSystemClasses
{
    // класс типов продуктов
    public class Type : Model
    {
        //название типа
        public string Name { get; private set; }

        //описание ипа
        public string Description { get; private set; }

        public Type()
        {

        }

        //конструктор со всеми основными аргументами
        [JsonConstructor]
        public Type(int Id, string Name, string Description) : base(Id)
        {
            this.Name = Name;
            this.Description = Description;
        }

        //конструктор в случае, если передают массив объектов
        public Type(object[] items)
        {
            //преобразуем объекты и приравниваем
            Id = (int)items[0];
            Name = (string)items[1];

            //если описание есть, тоже его приравниваем
            //if (items[2] != DBNull)
            //{
            //    Description = (string)items[2];
            //}
        }
    }
}