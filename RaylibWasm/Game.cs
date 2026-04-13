using Raylib_cs;
using Optional;
using RayGUI;
using System.Numerics;
using System;
using ui;
using System.IO;
using System.Timers;

namespace game
{
    enum GameState
    {
        MENU = 0,
        GAME = 1,
        PAUSE = 2,
        OPTIONS = 3
    }

    enum UpgradeType
    {
        BASIC = 0,
        GRANDMA = 1,
        MACHINE = 2,
        FACTORY = 3
    }

    public class Game
    {
        private static GameState gameState = GameState.MENU;
        private static int SCREEN_WIDTH = Raylib.GetScreenWidth();
        private static int SCREEN_HEIGHT = Raylib.GetScreenHeight();
        private static string baseDir = AppContext.BaseDirectory;
        private static string imagePath = Path.Combine(baseDir, "Resources", "cookie.png");
        private static Texture2D cookieTexture;
        private static Color tint = Color.White;
        private static bool isInitialized = false;
        private static int cookiesCount = 0;
        private static float timerAccumulator = 0f;
        private static int cookiesPerSecond = 0;

        // BASIC
        private static int b_upgradeCount = 0;
        private static int b_upgradeCost = 15;
        
        // GRANDMA
        private static int g_upgradeCount = 0;
        private static int g_upgradeCost = 250;

        // MACHINE
        private static int m_upgradeCount = 0;
        private static int m_upgradeCost = 575;

        // FACTORY
        private static int f_upgradeCount = 0;
        private static int f_upgradeCost = 1000;
        public static void Init()
        {
            cookieTexture = Raylib.LoadTexture(imagePath);
            isInitialized = true;
        }

        private static GameState RenderState()
        {
            switch (gameState)
            {
                case GameState.MENU:
                    Raylib.DrawText($"Cookie Clicker Clone", SCREEN_WIDTH/2 - 175, SCREEN_HEIGHT/2 - 150, 50, Color.RayWhite);
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 - 50 , SCREEN_HEIGHT/2 - 75), new Vector2(200, 75)), "START"))
                    {
                        gameState = GameState.GAME;
                    }
                    
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 - 50, SCREEN_HEIGHT/2 + 25), new Vector2(200, 75)), "OPTIONS"))
                    {
                        gameState = GameState.OPTIONS;
                    }
                    break;

                case GameState.GAME:
                    UpdateState();
                    break;

                case GameState.PAUSE:
                    UpdateState();
                    break;

                case GameState.OPTIONS:
                    break;
                
                default:
                    Console.WriteLine("State Not Implmented in Rendering: ", gameState);
                    break;
            }

            return gameState;
        }

        private static GameState UpdateState()
        {
            switch (gameState)
            {
                case GameState.GAME:
                    timerAccumulator += Raylib.GetFrameTime(); 
                    if (timerAccumulator >= 1.0f) 
                    {
                        cookiesCount += cookiesPerSecond;
                        timerAccumulator -= 1.0f; 
                    }
                    Raylib.DrawText($"{cookiesCount} cookies", SCREEN_WIDTH/2 - 200, SCREEN_HEIGHT/2 - 200, 50, tint);
                    Raylib.DrawText($"CPS: {cookiesPerSecond}", SCREEN_WIDTH/2 - 200, SCREEN_HEIGHT/2 - 150, 30, Color.LightGray);
                    if (ImageButton.Draw(new(new Vector2(SCREEN_WIDTH/2 - 100, SCREEN_HEIGHT/2 - 100), new Vector2(300, 300)), cookieTexture, tint))
                    {
                        cookiesCount++;
                    }

                    string b_upgradeText = $"Buy BASIC ({b_upgradeCost} C)";
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 + 250, SCREEN_HEIGHT/2 - 50), new Vector2(200, 50)), b_upgradeText))
                    {
                        if (cookiesCount >= b_upgradeCost)
                        {
                            cookiesCount -= b_upgradeCost; 
                            b_upgradeCount++;              
                            
                            cookiesPerSecond += 1;       
                            
                            b_upgradeCost = (int)(b_upgradeCost * 1.15f); 
                        }
                    }

                    string g_upgradeText = $"Buy GRANDMA ({g_upgradeCost} C)";
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 + 250, SCREEN_HEIGHT/2 + 60), new Vector2(200, 50)), g_upgradeText))
                    {
                        if (cookiesCount >= g_upgradeCost)
                        {
                            cookiesCount -= g_upgradeCost; 
                            g_upgradeCount++;              
                            
                            cookiesPerSecond += 3;       
                            
                            g_upgradeCost = (int)(g_upgradeCost * 3.15f); 
                        }
                    }

                    string m_upgradeText = $"Buy MACHINE ({m_upgradeCost} C)";
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 + 250, SCREEN_HEIGHT/2 + 120), new Vector2(200, 50)), m_upgradeText))
                    {
                        if (cookiesCount >= m_upgradeCost)
                        {
                            cookiesCount -= m_upgradeCost; 
                            m_upgradeCount++;              
                            
                            cookiesPerSecond += 7;       
                            
                            m_upgradeCost = (int)(m_upgradeCost * 6.15f); 
                        }
                    }

                    string f_upgradeText = $"Buy FACTORY ({f_upgradeCost} C)";
                    if (RayGui.GuiButton(new(new Vector2(SCREEN_WIDTH/2 + 250, SCREEN_HEIGHT/2 + 180), new Vector2(200, 50)), f_upgradeText))
                    {
                        if (cookiesCount >= f_upgradeCost)
                        {
                            cookiesCount -= f_upgradeCost; 
                            f_upgradeCount++;              
                            
                            cookiesPerSecond += 14;       
                            
                            f_upgradeCost = (int)(f_upgradeCost * 10.15f); 
                        }
                    }
                    break;

                case GameState.PAUSE:
                    break;

                default:
                    Console.WriteLine("State Not Implmentedin Updating: ", gameState);
                    break;
            }

            return gameState;
        }

        private static void Draw()
        {
            RenderState();
        }

        private static void Update()
        {
            UpdateState();
        }

        public static void DrawAndUpdate()
        {
            Update();
            Draw();
        }
    }
}