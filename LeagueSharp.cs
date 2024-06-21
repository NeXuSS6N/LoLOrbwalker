using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript
{
    class LeagueSharp
    {
        public static void Main()
        {
            Initialize();

            Evade evade = new Evade();
            Prediction prediction = new Prediction();
            Orbwalker orbwalker = new Orbwalker();
            TargetSelector targetSelector = new TargetSelector();
            Combos combos = new Combos();
            Activator activator = new Activator();

            //event handlers
            Game.OnUpdate += OnUpdate;
            Game.OnWndProc += OnWndProc;

            CreateInterface();
        }

        private static void Initialize()
        {
            // Scripts settings
            ScriptSettings settings = new ScriptSettings();
            settings.AddBool("Evade", "Evade", true);
            settings.AddBool("Prediction", "Prediction", true);
            settings.AddBool("Orbwalker", "Orbwalker", true);
            settings.AddBool("TargetSelector", "TargetSelector", true);
            settings.AddBool("Combos", "Combos", true);
            settings.AddBool("Activator", "Activator", true);
        }

        private static void OnUpdate(EventArgs args)
        {
            evade.Update();
            prediction.Update();
            orbwalker.Update();
            targetSelector.Update();
            combos.Update();
            activator.Update();
        }

        private static void OnWndProc(WndEventArgs args)
        {
            // Handle in-game events
            if (args.Msg == (int)WindowsMessages.WM_KEYDOWN)
            {
                // Handle key presses
                if (args.WParam.ToInt32() == 32) // Space bar
                {
                    // Toggle evade
                    ScriptSettings.Evade = !ScriptSettings.Evade;
                }
                else if (args.WParam.ToInt32() == 13) // Enter key
                {
                    // Toggle orbwalker
                    ScriptSettings.Orbwalker = !ScriptSettings.Orbwalker;
                }
            }
        }

        private static void CreateInterface()
        {
            // Ingame menu avec LeagueSharp
            Menu menu = new Menu("LeagueSharp Script", "LeagueSharp Script", true);

            // Add menu items for each feature
            menu.AddItem(new MenuItem("Evade", "Evade").SetValue(ScriptSettings.Evade));
            menu.AddItem(new MenuItem("Prediction", "Prediction").SetValue(ScriptSettings.Prediction));
            menu.AddItem(new MenuItem("Orbwalker", "Orbwalker").SetValue(ScriptSettings.Orbwalker));
            menu.AddItem(new MenuItem("TargetSelector", "TargetSelector").SetValue(ScriptSettings.TargetSelector));
            menu.AddItem(new MenuItem("Combos", "Combos").SetValue(ScriptSettings.Combos));
            menu.AddItem(new MenuItem("Activator", "Activator").SetValue(ScriptSettings.Activator));

            menu.AddToMainMenu();
        }
    }
}