using RuneterraCompanion.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RuneterraCompanion.CustomModels
{
    public sealed class Card : CardAttribute
    {
        public CardImage Image { get {
                if (this.image == null)
                {
                    SetImage(Path.Combine(Directory.GetCurrentDirectory(), Constants.cardImgPath,this.cardCode + ".png"));
                }
                return image;
            } }

        public Card()
        {

        }

        public void SetAttributes(CardAttribute attr)
        {
            if(attr != null)
            {
                foreach (var field in attr.GetType().GetProperties())
                {
                    var attrValue = field.GetValue(attr);
                    var newValue = this.GetType().GetProperty(field.Name);
                    if(attrValue != null)
                    {
                        newValue.SetValue(this, attrValue);
                    }
                }
            }
        }

        public void SetImage(string path)
        {
            image = new CardImage(path);
        }

        //public Card(string path)
        //{
        //    SetImage(path);
        //}

        private CardImage image;
    }
}
