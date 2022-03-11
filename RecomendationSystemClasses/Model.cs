namespace RecomendationSystemClasses
{
    // базовый общий класс модели
    public class Model
    {
        // уникальный идентификатор объекта
        public int Id { get; protected set; }

        //пустой конструктор объекта, без аргументов
        public Model()
        {

        }

        //конструктор для ситуации, если пользователь передает ID
        public Model(int id)
        {
            Id = id;
        }
    }
}