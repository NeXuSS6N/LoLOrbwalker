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

            // Implement prediction logic here
            foreach (var enemy in ObjectManager.GetEnemyHeroes())
            {
                if (enemy.IsValidTarget())
                {
                    // Predict the enemy's movement
                    var predictionPosition = PredictPosition(enemy);

                    // Check if the prediction is valid
                    if (predictionPosition.IsValid())
                    {
                        // Use the predicted position for orbwalking or comboing
                        Orbwalker.Orbwalk(enemy, predictionPosition);
                    }
                }
            }
        }

        private Vector3 PredictPosition(Obj_AI_Hero enemy)
        {
            // Calculate the enemy's movement speed and direction
            var movementSpeed = enemy.MoveSpeed;
            var movementDirection = (enemy.ServerPosition - enemy.PreviousPosition).Normalized();

            // Predict the enemy's future position based on their movement speed and direction
            var predictionPosition = enemy.ServerPosition + movementDirection * movementSpeed * 0.25f;

            // Check if the predicted position is valid
            if (predictionPosition.IsOnScreen() && predictionPosition.Distance(_player.ServerPosition) < 1500)
            {
                return predictionPosition;
            }

            return Vector3.Zero;
        }
    }
}