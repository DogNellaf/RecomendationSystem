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
        private bool edibility;
        private double average_price;
        private bool with_sugar;
        private bool hypoallergenic;
        private bool eco_friendly;
        private bool import;
        private bool antibacterial;
        private bool non_gmo;
        private bool vegan;
        private bool lean;

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

        private bool IsEdibility { get => edibility; }
        private float AveragePrice { get => average_price; }
        private bool IsWithSugar { get => with_sugar; }
        private bool IsHypoallergenic { get => hypoallergenic; }
        private bool EcoFriendly { get => eco_friendly; }
        private bool IsImport { get => import; }
        private bool IsAntibacterial { get => antibacterial; }
        private bool IsNonGMO { get => non_gmo; }
        private bool IsVegan { get => vegan; }
        private bool IsLean { get => lean; }

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
            SetProperty(ref edibility, items[5]);
            SetProperty(ref average_price, items[6]);
            SetProperty(ref with_sugar, items[7]);
            SetProperty(ref hypoallergenic, items[8]);
            SetProperty(ref eco_friendly, items[9]);
            SetProperty(ref import, items[10]);
            SetProperty(ref antibacterial, items[11]);
            SetProperty(ref non_gmo, items[12]);
            SetProperty(ref vegan, items[13]);
            SetProperty(ref lean, items[14]);
        }

        public Item(dynamic objects)
        {
            this.Id = objects["Id"].ToObject<int>();
            this.name = objects["Name"].ToObject<string>();
            this.photo = objects["Photo"].ToObject<string>();
            this.typeId = objects["TypeId"].ToObject<int>();
            this.edibility = objects["Edibility"].ToObject<bool>();
            this.average_price = objects["AveragePrice"].ToObject<bool>();
            this.with_sugar = objects["IsWithSugar"].ToObject<bool>();
            this.edibility = objects["edibility"].ToObject<bool>();
            try
            {
                this.description = objects["Description"].ToObject<string>();
            } catch { }
        }

        public Type GetType(List<Type> types) => types.Where(x => x.Id == this.TypeId).FirstOrDefault();
    }
}