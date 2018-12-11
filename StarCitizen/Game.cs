using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MyGame
{
/// <summary>
/// Созданиие массивов с графическими элементами, вывод и обновление с и спользованием таймера
/// </summary>
    static class Game
    {
        public static ImageObject[] _singleobjects;
        private static List<Hash> _hashes = new List<Hash>();
        public static ImageObject[] _enemy;
        public static Random rnd = new Random();
        private static Timer _timer = new Timer { Interval = 50 };

        /// <summary>
        /// Загрузка объектов в массивы для последующего вывода через Draw()
        /// </summary>
        public static void Load()
        {
            _singleobjects = new ImageObject[1];
            _enemy = new ImageObject[10];

            for (int i = 0; i < _enemy.Length; i++)                 
            _enemy[i] = new Enemy(ImageObject.Enemy, Width + rnd.Next(0,280), i * rnd.Next(120, 130) + rnd.Next(100, 200));

            _singleobjects[0] = new Background(ImageObject.BattleField, 0, 0);
        }
        private static Tank _tank = new Tank(ImageObject.Tank, 100, 100);

        public static void Update()
        {
            foreach (ImageObject obj in _singleobjects) obj.Update();

            foreach (Hash h in _hashes) h.Update();
            for (var i = 0; i < _enemy.Length; i++)
            {
                if (_enemy[i] == null) continue;
                _enemy[i].Update();
                for (int j = 0; j < _hashes.Count; j++)
                    if (_enemy[i] != null && _hashes[j].Collision(_enemy[i]))
                    {
                        _enemy[i] = null;
                        _hashes.RemoveAt(j);
                        j--;
                        Console.WriteLine("Enemy down");
                    } else

                    if (_hashes[j].Xcord_hash() > Width+135)
                    {
                        _hashes.RemoveAt(j);
                        j--;
                        Console.WriteLine("Hash deleted");
                    }
                if (_enemy[i] == null || !_tank.Collision(_enemy[i])) continue;
                _tank.EnergyLow(rnd.Next(1, 10));
                Console.WriteLine("Tank penetration");
                _enemy[i] = null;
                if (_tank.Energy <= 0) _tank.Die();
            }
            _tank?.Update();
            foreach (Hash h in _hashes) h?.Update();
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


        public static void Init(Form form)
        {
        
            // Графическое устройство для вывода графики            
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            // Создаем объект (поверхность рисования) и связываем его с формой
            // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом, чтобы рисовать в буфере
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            //Таймер
            //Timer timer = new Timer { Interval = 50 };
            _timer.Start();
            _timer.Tick += Timer_Tick;
            Load();
            form.KeyDown += Form_KeyDown;
            Tank.MessageDie += Finish;
        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _hashes.Add(new Hash(ImageObject.Hash, _tank.Xcord_tank() + 200, _tank.Ycord_tank() + 55));
            if (e.KeyCode == Keys.Up) _tank.Up();
            if (e.KeyCode == Keys.Down) _tank.Down();
        }


        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (ImageObject obj in _singleobjects) obj?.Draw();
            foreach (ImageObject obj in _enemy) obj?.Draw();
            _tank?.Draw();
            foreach (Hash h in _hashes) h?.Draw();
            Buffer.Render();
        }

        public static void Finish()
        {
            _timer.Stop();
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, Width/4, Height/3);
            Buffer.Render();
        }
    }

}
