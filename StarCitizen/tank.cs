using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    /// <summary>
    /// Класс, отвечающий за изобрражение танка игрока, перегруженные методы отрисовки и обновления от ImageObject
    /// </summary>
    class Tank : ImageObject
    {
        public static event Message MessageDie;
        public Tank(Image img, float x, float y) : base(img, x, y)
        {
            Enemy.RotateFlip(RotateFlipType.Rotate180FlipY); // Тут оно работает - переворачивает изображение врага.

        }
        public int Energy { get; private set; } = 10;

        public void EnergyLow(int n)
        {
            Energy -= n;
        }


        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Img, X, Y);
        }

        public override void Update()
        {
            
        }
        public float Xcord_tank()
        {
           float x = X;
            return x;
        }

        public float Ycord_tank()
        {
            float y = Y;
            return y;
        }

        public void Up()
        {
            if (Y > 0) Y = Y - 20;
        }
        public void Down()
        {
            if (Y < Game.Height) Y = Y + 20;
        }
        public void Die()
        {
            MessageDie?.Invoke();
            Console.WriteLine("Tank Down");
        }

    }

}