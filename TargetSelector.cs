using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.TargetSelector
{
    class TargetSelector
    {
        public static Obj_AI_Hero GetTarget()
        {
            return ObjectManager.GetEnemyHeroes()
                .Where(h => h.IsValidTarget() && h.Distance(ObjectManager.Player) < 600)
                .OrderBy(h => h.Health)
                .FirstOrDefault();
        }
    }
}