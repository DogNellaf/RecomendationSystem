using System.Collections.Generic;
using System.Linq;

namespace RecommendationSystem.Models
{
    //продукт в базе
    public class Item : Model
    {
        #region Private

        private string name;
        private string photo;
        private string description;
        private int typeId;

        #endregion

        #region Properties

        //название продукта
        public string Name { get => name; }

        //фотография продукта
        public string Photo { get => photo; }

        //описание продукта
        public string Description { get => description; }

        //тип продукта
        public int TypeId { get => typeId; }

        #endregion

        public Item()
        {

        }

        //конструктор со всеми основными аргументами
        public Item(int id, string name, string photo, string description, int typeId) : base(id)
        {
            this.name = name;
            this.photo = photo;
            this.description = description;
            this.typeId = typeId;
        }

        //конструктор в случае, если передают массив объектов
        public Item(object[] items)
        {
            SetProperty(ref Id, items[0]);
            SetProperty(ref name, items[1]);
            SetProperty(ref photo, items[2]);
            SetProperty(ref description, items[3]);
            SetProperty(ref typeId, items[4]);
        }

        public Item(dynamic objects)
        {
            this.Id = objects["Id"].ToObject<int>();
            this.name = objects["Name"].ToObject<string>();
            this.photo = objects["Photo"].ToObject<string>();
            this.typeId = objects["TypeId"].ToObject<int>();
            try
            {
                this.description = objects["Description"].ToObject<string>();
            } catch { }
        }

        public Type GetType(List<Type> types) => types.Where(x => x.Id == this.TypeId).FirstOrDefault();
    }
}