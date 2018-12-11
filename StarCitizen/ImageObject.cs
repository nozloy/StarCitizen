using System;
using System.Drawing;

namespace MyGame
{
/// <summary>
/// Родительский класс графических объектов с абстрактными методами вывода и обновления изображения 
/// </summary>
    abstract class ImageObject : ICollision
    {
        public delegate void Message();
        protected Image Img;
        protected float X;
        protected float Y;
        // присваиваем переменным файлы изображений
        public static Image Tank = Image.FromFile("Tank.png");
        public static Image BattleField = Image.FromFile("battlefield.jpg");
        public static Image Enemy = Image.FromFile("Enemy.png");
        public static Image Hash = Image.FromFile("Hash.png");
        public static Image Explosion = Image.FromFile("Explosion.png");
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(Rect);

        protected ImageObject(Image img, float x, float y)
        {
            Img = img;
            X = x;
            Y = y;
        }

        public Rectangle Rect => new Rectangle((int)X, (int)Y, 50, 50);
        //делаем методы абстрактными
        public abstract void Draw();
        public abstract void Update();
    }
}
