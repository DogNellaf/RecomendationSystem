using System;
using System.ComponentModel;

namespace RecommendationSystem.Models
{
    // пользователь в системе
    public class User : Model
    {
        #region Private

        private string name;
        private string password;
        private string mobile;
        private string photo;

        #endregion

        #region Properties

        // имя пользователя
        public string Name { get => name; }

        // пароль пользователя
        public string Password { get => password; }

        // мобильный телефон пользователя
        public string Mobile { get => mobile; }

        // путь до аватарки на сервере
        public string Photo { get => photo; }

        #endregion

        // пустой конструктор
        public User()
        {

        }

        // конструктор со всеми основными аргументами
        public User(int id, string name, string password, string mobile, string photo) : base(id)
        {
            this.name = name;
            this.password = password;
            this.mobile = mobile;
            this.photo = photo;
        }

        // конструктор в случае, если передают массив объектов
        public User(object[] items)
        {
            SetProperty(ref Id, items[0]);
            SetProperty(ref name, items[1]);
            SetProperty(ref password, items[2]);
            SetProperty(ref mobile, items[3]);
            SetProperty(ref photo, items[4]);
        }

        // динамический конструктор 
        public User(dynamic objects)
        {
            this.Id = objects["Id"].ToObject<int>();
            this.name = objects["Name"].ToObject<string>();
            this.password = objects["Password"].ToObject<string>();
            this.mobile = objects["Mobile"].ToObject<string>();
            this.photo = objects["Photo"].ToObject<string>();
        }
    }
}
