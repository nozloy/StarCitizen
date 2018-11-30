using System;
using System.Windows.Forms;
using System.Drawing;
namespace MyGame
{
    static class Game
    {
        public static BaseObject[] _objs;
        public static void Load()
        {
            _objs = new BaseObject[20];
            for (int i = 0; i < _objs.Length; i++)
                for (int k = 0; k < Height; k=k+50)
                    _objs[i] = new Star(new Point(750,i*50), new Point(30, 0), new Size(20, 20));
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            //неведомый метод, убирающий мерцания 
            //form.Invalidate();
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            //Таймер
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
            Load();

        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            for (int i = 0; i < Width; i=i+236)
                for (int k = 0; k < Height; k = k + 236)
                    Buffer.Graphics.DrawImage(ImageObject.BattleField, i, k);
            Buffer.Graphics.DrawImage(ImageObject.Tank, 100, 100);
            Buffer.Graphics.DrawImage(ImageObject.Hash, 500, 150);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }





    }

}
