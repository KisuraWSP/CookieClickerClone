using Raylib_cs;
using System.Numerics;

namespace ui
{

    /*
        Rectangle exitRect = new Rectangle(Game.width / 2 - 50, 350, 100, 100);
        if (ImageButton.Draw(exitRect, texture, Color.White))
        {
            Environment.Exit(0);
        }
    */
    public class ImageButton
    {
        public static bool Draw(Rectangle bounds, Texture2D texture, Color tint)
        {
            Vector2 mousePos = Raylib.GetMousePosition();
            bool isHovering = Raylib.CheckCollisionPointRec(mousePos, bounds);
            
            Color drawColor = isHovering ? Raylib.ColorBrightness(tint, 0.2f) : tint;

            Raylib.DrawTexturePro(texture, 
                new(0, 0, texture.Width, texture.Height), 
                bounds, new(0, 0), 0.0f, drawColor);

            return isHovering && Raylib.IsMouseButtonPressed(MouseButton.Left);
        }
    }
}