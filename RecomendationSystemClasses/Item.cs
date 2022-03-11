namespace RecomendationSystemClasses
{
    //продукт в базе
    public class Item : Model
    {
        //название продукта
        public string Name { get; private set; }

        //фотография продукта
        public string Photo { get; private set; }

        //описание продукта
        public string Description { get; private set; }

        //тип продукта
        public int TypeId { get; private set; }

        //конструктор со всеми основными аргументами
        public Item(int Id, string Name, string Photo, string Description, int TypeId) : base(Id)
        {
            this.Name = Name;
            this.Photo = Photo;
            this.Description = Description;
            this.TypeId = TypeId;
        }

        //конструктор в случае, если передают массив объектов
        public Item(object[] items)
        {
            //преобразуем объекты и приравниваем
            Id = (int)items[0];
            Name = (string)items[1];
            Photo = (string)items[2];
            TypeId = (int)items[4];

            //если описание есть, то приравниваем его
            if (items[3] != null)
            {
                Description = (string)items[3];
            }
        }
    }
}