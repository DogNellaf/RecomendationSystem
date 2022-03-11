using System;

namespace RecomendationSystemClasses
{
    //отзывы в базе
    public class Review : Model
    {
        //дата написания
        public DateTime Date { get; private set; }

        //автор отзыва
        public int AuthorId { get; private set; }

        //оценка продукта
        public int Rating { get; private set; }

        //текст отзыва
        public string Text { get; private set; }

        //продукт, к которому относится отзыв
        public int ItemId { get; private set; }

        //конструктор со всеми основными аргументами
        public Review(int id, DateTime date, int userId, int rating, string text, int itemId) : base(id)
        {
            this.Date = date;
            this.AuthorId = userId;
            this.Rating = rating;
            this.Text = text;
            this.ItemId = itemId;
        }

        //конструктор в случае, если передают массив объектов
        public Review(object[] items)
        {
            //преобразуем объекты и приравниваем
            Id = (int)items[0];
            Date = (DateTime)items[1];
            AuthorId = (int)items[2];
            Rating = (int)items[3];
            Text = (string)items[4];
            ItemId = (int)items[5];
        }
    }
}
