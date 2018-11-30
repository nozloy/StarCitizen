using System;
using System.Drawing;

namespace MyGame
{

    class ImageObject
    {
        protected System.Drawing.Image Img;
        protected Single X;
        protected Single Y;
        public static Image Tank = Image.FromFile("Tank.png");
        public static Image BattleField = Image.FromFile("battlefield.jpg");
        public static Image Hash = Image.FromFile("hash.png");


        public ImageObject(Image img, Single x, Single y)
        {
            Img = img;
            X = x;
            Y = y;
        }

    }
}
