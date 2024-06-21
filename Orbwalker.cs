using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.Orbwalker
{
    class Orbwalker
    {
        private readonly Obj_AI_Hero _player;

        public Orbwalker()
        {
            _player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!ScriptSettings.Orbwalker) return;

            // Implement orbwalker logic here
            var target = TargetSelector.GetTarget();

            if (target.IsValidTarget())
            {
                // Orbwalk to the target
                Orbwalk(target);
            }
        }

        private void Orbwalk(Obj_AI_Hero target)
        {
            // Calculate the distance to the target
            var distance = _player.ServerPosition.Distance(target.ServerPosition);

            // Check if the target is in attack range
            if (distance < 600)
            {
                // Attack the target
                _player.IssueOrder(GameObjectOrder.AttackUnit, target);
            }
            else
            {
                // Move to the target's position
                MoveToTarget(target);
            }
        }

        private void MoveToTarget(Obj_AI_Hero target)
        {
            // Calculate the path to the target
            var path = NavMesh.GetPath(_player.ServerPosition, target.ServerPosition);

            // Check if the path is valid
            if (path.IsValid())
            {
                // Move along the path
                _player.IssueOrder(GameObjectOrder.MoveTo, path.First());
            }
            else
            {
                // Move directly to the target's position
                _player.IssueOrder(GameObjectOrder.MoveTo, target.ServerPosition);
            }
        }
    }
}