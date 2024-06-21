using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.TargetSelector
{
    class TargetSelector
    {
        public static Obj_AI_Hero GetTarget()
        {
            // Implement target selector logic here
            // Return the closest enemy hero to the player that is within attack range and has the lowest health
            return ObjectManager.GetEnemyHeroes()
                .Where(h => h.IsValidTarget() && h.Distance(ObjectManager.Player) < 600)
                .OrderBy(h => h.Health)
                .FirstOrDefault();
        }
    }
}