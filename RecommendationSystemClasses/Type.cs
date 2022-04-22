namespace RecommendationSystem.Models
{
    // класс типов продуктов
    public class Type : Model
    {
        // скрытое название типа
        private string name;

        // название типа
        public string Name { get => name; }

        // пустой конструктор
        public Type()
        {

        }

        // конструктор со всеми основными аргументами
        public Type(int Id, string name) : base(Id) => this.name = name;

        // конструктор в случае, если передают массив объектов
        public Type(object[] items)
        {
            //преобразуем объекты и приравниваем
            SetProperty(ref Id, items[0]);
            SetProperty(ref name, items[1]);
        }

        // конструктор для динамического формирования объектов 
        public Type(dynamic objects)
        {
            this.Id = objects["Id"].ToObject<int>();
            this.name = objects["Name"].ToObject<string>();
        }
    }
}