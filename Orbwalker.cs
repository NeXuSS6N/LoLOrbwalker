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

            var target = TargetSelector.GetTarget();

            if (target.IsValidTarget())
            {
                Orbwalk(target);
            }
        }

        private void Orbwalk(Obj_AI_Hero target)
        {
            var distance = _player.ServerPosition.Distance(target.ServerPosition);

            if (distance < 600)
            {
                _player.IssueOrder(GameObjectOrder.AttackUnit, target);
            }
            else
            {
                MoveToTarget(target);
            }
        }

        private void MoveToTarget(Obj_AI_Hero target)
        {
            var path = NavMesh.GetPath(_player.ServerPosition, target.ServerPosition);

            if (path.IsValid())
            {
                _player.IssueOrder(GameObjectOrder.MoveTo, path.First());
            }
            else
            {
                _player.IssueOrder(GameObjectOrder.MoveTo, target.ServerPosition);
            }
        }
    }
}