using System;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Класс, отвечающий за отображение примитивных объектов
    /// </summary>
    abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);

        }
        public abstract void Update();

    }
}
