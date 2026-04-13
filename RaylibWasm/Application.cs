using System.Runtime.InteropServices.JavaScript;
using Raylib_cs;
using game;

namespace RaylibWasm
{
    public partial class Application
    {
        /// <summary>
        /// Application entry point
        /// </summary>
        public static void Main()
        {
            Raylib.InitWindow(1000, 550, "Cookie Clicker Clone");
            Game.Init();
            Raylib.SetTargetFPS(60);
        }

        /// <summary>
        /// Updates frame
        /// </summary>
        [JSExport]
        public static void UpdateFrame()
        {
            Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Orange);
                Game.DrawAndUpdate();
            Raylib.EndDrawing();
        }
    }
}
