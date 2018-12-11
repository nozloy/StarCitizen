using System;
using System.Windows.Forms;
// Создаем шаблон приложения, где подключаем модули
namespace MyGame
{
    /// <summary>
    /// Управляющий код, создание, прорисовка ии запуск формы.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 1024;
            form.Height = 768;
            form.KeyPreview = true;
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }


    }
}

