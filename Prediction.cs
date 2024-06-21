using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.Prediction
{
    class Prediction
    {
        private readonly Obj_AI_Hero _player;

        public Prediction()
        {
            _player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!ScriptSettings.Prediction) return;

            foreach (var enemy in ObjectManager.GetEnemyHeroes())
            {
                if (enemy.IsValidTarget())
                {
                    var predictionPosition = PredictPosition(enemy);

                    if (predictionPosition.IsValid())
                    {
                        Orbwalker.Orbwalk(enemy, predictionPosition);
                    }
                }
            }
        }

        private Vector3 PredictPosition(Obj_AI_Hero enemy)
        {
            var movementSpeed = enemy.MoveSpeed;
            var movementDirection = (enemy.ServerPosition - enemy.PreviousPosition).Normalized();

            var predictionPosition = enemy.ServerPosition + movementDirection * movementSpeed * 0.25f;

            if (predictionPosition.IsOnScreen() && predictionPosition.Distance(_player.ServerPosition) < 1500)
            {
                return predictionPosition;
            }

            return Vector3.Zero;
        }
    }
}