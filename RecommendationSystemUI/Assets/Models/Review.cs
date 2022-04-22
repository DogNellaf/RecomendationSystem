using System.Collections.Generic;
using System.Linq;

namespace RecommendationSystem.Models
{
    // отзывы в базе
    public class Review : Model
    {

        #region Private

        private string date;
        private int authorId;
        private int rating;
        private string text;
        private int itemId;

        #endregion

        #region Properties

        // дата написания
        public string Date { get => date; }

        // автор отзыва
        public int AuthorId { get => authorId; }

        // оценка продукта
        public int Rating { get => rating; }

        // текст отзыва
        public string Text { get => text; }

        // продукт, к которому относится отзыв
        public int ItemId { get => itemId; }

        #endregion

        // пустой конструктор
        public Review()
        {

        }

        // конструктор со всеми основными аргументами
        public Review(int Id, string date, int authorId, int rating, string text, int itemId)
        {
            this.Id = Id;
            this.date = date;
            this.authorId = authorId; 
            this.rating = rating;
            this.text = text;
            this.itemId = itemId;
        }

        // конструктор в случае, если передают массив объектов
        public Review(object[] items)
        {
            SetProperty(ref Id, items[0]);
            SetProperty(ref date, items[1]);
            SetProperty(ref authorId, items[2]);
            SetProperty(ref rating, items[3]);
            SetProperty(ref text, items[4]);
            SetProperty(ref itemId, items[5]);
        }

        // конструктор для динамической конвертации
        public Review(dynamic objects)
        {
            this.Id = objects["Id"].ToObject<int>();
            this.date = objects["Date"].ToObject<string>();
            this.authorId = objects["AuthorId"].ToObject<int>();
            this.rating = objects["Rating"].ToObject<int>();
            this.text = objects["Text"].ToObject<string>();
            this.itemId = objects["ItemId"].ToObject<int>();
        }

        #region Utils

        // получить предмет по его id
        public Item GetItem(List<Item> items) => items.Where(x => x.Id == this.ItemId).FirstOrDefault();

        // получить пользователя по его id
        public User GetAuthor(List<User> users) => users.Where(x => x.Id == this.AuthorId).FirstOrDefault();

        #endregion
    }
}
