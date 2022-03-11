namespace RecomendationSystemClasses
{
    //пользователь в системе
    public class User : Model
    {
        //имя пользователя
        public string Username { get; private set; }

        //пароль пользователя
        public string Password { get; private set; }

        //мобильный телефон пользователя
        public string Mobile { get; private set; }

        //конструктор со всеми основными аргументами
        public User(int Id, string Username, string Password, string Mobile) : base(Id)
        {
            this.Username = Username;
            this.Password = Password;
            this.Mobile = Mobile;
        }

        //конструктор в случае, если передают массив объектов
        public User(object[] items)
        {
            //преобразуем объекты и приравниваем
            this.Id = (int)items[0];
            this.Username = (string)items[1];
            this.Password = (string)items[2];
            this.Mobile = (string)items[3];
        }
    }
}
