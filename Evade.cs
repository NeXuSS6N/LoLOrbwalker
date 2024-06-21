using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.Evade
{
    class Evade
    {
        private readonly Obj_AI_Hero _player;

        public Evade()
        {
            _player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!ScriptSettings.Evade) return;

            foreach (var enemy in ObjectManager.GetEnemyHeroes())
            {
                if (enemy.IsValidTarget())
                {
                    foreach (var skillshot in SkillshotDetector.DetectedSkillshots)
                    {
                        if (skillshot.SpellData.SpellType == SpellType.Circle || skillshot.SpellData.SpellType == SpellType.Line)
                        {
                            // Calcule la distance entre le joueur et le skillshot
                            var distance = _player.ServerPosition.Distance(skillshot.StartPosition);

                            // Check si le joueur est a range du skillshot
                            if (distance < skillshot.SpellData.Radius + _player.BoundingRadius)
                            {
                                // Calculate le temps avant que le skillshot touche
                                var timeUntilHit = skillshot.SpellData.Delay + distance / skillshot.SpellData.Speed;

                                // Check si le joueur a le temps de dodge
                                if (timeUntilHit > 0 && timeUntilHit < 0.5)
                                {
                                    EvadeSkillshot(skillshot);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EvadeSkillshot(Skillshot skillshot)
        {
            // Calculate la direction du skillshot
            var direction = (skillshot.EndPosition - skillshot.StartPosition).Normalized();

            // calcule la position ou bouger
            var evadePosition = _player.ServerPosition + direction.Perpendicular() * 100;

            if (IsSafePosition(evadePosition))
            {
                Orbwalker.MoveTo(evadePosition);
            }
            else
            {
                // trouve un chemin safe
                var safePosition = FindSafePosition(skillshot);
                if (safePosition.IsValid())
                {
                    Orbwalker.MoveTo(safePosition);
                }
            }
        }

        private bool IsSafePosition(Vector3 position)
        {
            foreach (var minion in ObjectManager.GetMinions(ObjectManager.Player.Position, 1000))
            {
                if (minion.IsValidTarget() && minion.Distance(position) < 100)
                {
                    return false;
                }
            }

            // Check si la position n'est pas dans la range d'une tourelle
            foreach (var tower in ObjectManager.GetTurrets())
            {
                if (tower.IsValid() && tower.Distance(position) < 1000)
                {
                    return false;
                }
            }

            return true;
        }

        private Vector3 FindSafePosition(Skillshot skillshot)
        {
            // trouve une position safe avec navmesh
            var navMesh = NavMesh.GetNavMesh();
            var safePosition = navMesh.GetClosestPath(_player.ServerPosition, skillshot.EndPosition, 100);

            return safePosition;
        }
    }
}