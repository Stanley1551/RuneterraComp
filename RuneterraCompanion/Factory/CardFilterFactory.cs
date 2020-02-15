using RuneterraCompanion.CustomModels.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuneterraCompanion.Factory
{
    public static class CardFilterFactory
    {
        public static CardFilterProperty Latest { get; set; }

        //próbáljuk ki
        public static CardFilterProperty ConstructCardFilterProperty(Action<CardFilterProperty> action)
        {
            CardFilterProperty filter = new CardFilterProperty();
            action(filter);

            SetToLatest(filter);
            return filter;
        }

        private static void SetToLatest(CardFilterProperty prop)
        {
            Latest = prop;
        }
    }
}
