using System;
using System.ComponentModel;

namespace RecommendationSystem.Models
{
    // базовый общий класс модели
    public class Model
    {
        // уникальный идентификатор объекта
        public int Id;

        //пустой конструктор объекта, без аргументов
        public Model()
        {

        }

        //конструктор для ситуации, если пользователь передает ID
        public Model(int id)
        {
            Id = id;
        }

        #region Utils

        protected void SetProperty(ref int property, object value)
        {
            if (value is not DBNull)
            {
                property = (int)value;
            }
        }

        protected void SetProperty(ref string property, object value)
        {
            if (value is not DBNull)
            {
                property = (string)value;
            }
        }


        #endregion
    }
}