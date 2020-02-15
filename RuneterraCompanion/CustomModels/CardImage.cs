using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace RuneterraCompanion.CustomModels
{
    public class CardImage
    {
        public CardImage()
        {

        }

        public CardImage(string path)
        {
            Path = path;
        }

        public BitmapImage Image { get; private set; }
        public string Path { get; private set; }
        public Uri PathUri => new Uri(Path);
        public double Width { get; private set; }
        public double Height { get; private set; }
        //TODO generated Tooltip

        public void SetBitMapImage()
        {
            Image = ConstructBitMapImage();
        }

        private BitmapImage ConstructBitMapImage()
        {
            BitmapImage image = new BitmapImage();

            image.BeginInit();
            //On.Demand??
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = PathUri;
            image.EndInit();

            return image;
        }
    }

}
