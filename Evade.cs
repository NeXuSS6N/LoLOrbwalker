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

            // Implement evade logic here
            foreach (var enemy in ObjectManager.GetEnemyHeroes())
            {
                if (enemy.IsValidTarget())
                {
                    // Check for skillshots and projectiles
                    foreach (var skillshot in SkillshotDetector.DetectedSkillshots)
                    {
                        if (skillshot.SpellData.SpellType == SpellType.Circle || skillshot.SpellData.SpellType == SpellType.Line)
                        {
                            // Calculate the distance between the player and the skillshot
                            var distance = _player.ServerPosition.Distance(skillshot.StartPosition);

                            // Check if the player is in range of the skillshot
                            if (distance < skillshot.SpellData.Radius + _player.BoundingRadius)
                            {
                                // Calculate the time until the skillshot hits
                                var timeUntilHit = skillshot.SpellData.Delay + distance / skillshot.SpellData.Speed;

                                // Check if the player can evade the skillshot
                                if (timeUntilHit > 0 && timeUntilHit < 0.5)
                                {
                                    // Evade the skillshot
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
            // Calculate the direction of the skillshot
            var direction = (skillshot.EndPosition - skillshot.StartPosition).Normalized();

            // Calculate the position to move to
            var evadePosition = _player.ServerPosition + direction.Perpendicular() * 100;

            // Check if the evade position is safe
            if (IsSafePosition(evadePosition))
            {
                // Move to the evade position
                Orbwalker.MoveTo(evadePosition);
            }
            else
            {
                // Find a safe path to move to
                var safePosition = FindSafePosition(skillshot);
                if (safePosition.IsValid())
                {
                    Orbwalker.MoveTo(safePosition);
                }
            }
        }

        private bool IsSafePosition(Vector3 position)
        {
            // Check if the position is not in a minion's attack range
            foreach (var minion in ObjectManager.GetMinions(ObjectManager.Player.Position, 1000))
            {
                if (minion.IsValidTarget() && minion.Distance(position) < 100)
                {
                    return false;
                }
            }

            // Check if the position is not in a tower's attack range
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
            // Find a safe position using a navmesh
            var navMesh = NavMesh.GetNavMesh();
            var safePosition = navMesh.GetClosestPath(_player.ServerPosition, skillshot.EndPosition, 100);

            return safePosition;
        }
    }
}