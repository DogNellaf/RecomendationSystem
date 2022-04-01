using System;
using System.Text.Json.Serialization;

namespace RecomendationSystemClasses
{
    //отзывы в базе
    public class Review : Model
    {
        //дата написания
        public string Date { get; set; }

        //автор отзыва
        public int AuthorId { get; set; }

        //оценка продукта
        public int Rating { get; set; }

        //текст отзыва
        public string Text { get; set; }

        //продукт, к которому относится отзыв
        public int ItemId { get; set; }

        public Review()
        {

        }

        //конструктор со всеми основными аргументами
        [JsonConstructor]
        public Review(int Id, string Date, int AuthorId, int Rating, string Text, int ItemId)
        {
            this.Id = Id;
            this.Date = Date;
            this.AuthorId = AuthorId; 
            this.Rating = Rating;
            this.Text = Text;
            this.ItemId = ItemId;
        }

        //конструктор в случае, если передают массив объектов
        public Review(object[] items)
        {
            //преобразуем объекты и приравниваем
            Id = (int)items[0];
            Date = (string)items[1];
            AuthorId = (int)items[2];
            Rating = (int)items[3];
            Text = (string)items[4];
            ItemId = (int)items[5];
        }
    }
}
