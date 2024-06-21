using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.Combos
{
    class Combos
    {
        private readonly Obj_AI_Hero _player;

        public Combos()
        {
            _player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!ScriptSettings.Combos) return;

            // Implement combo logic here
            var target = TargetSelector.GetTarget();

            if (target.IsValidTarget())
            {
                // Cast spells and abilities in a combo
                CastCombo(target);
            }
        }

        private void CastCombo(Obj_AI_Hero target)
        {
            // Check if the player has the necessary spells and abilities
            if (_player.Spellbook.GetSpell(SpellSlot.Q).IsReady() &&
                _player.Spellbook.GetSpell(SpellSlot.W).IsReady() &&
                _player.Spellbook.GetSpell(SpellSlot.E).IsReady() &&
                _player.Spellbook.GetSpell(SpellSlot.R).IsReady())
            {
                // Cast the combo
                CastQ(target);
                Utility.DelayAction.Add(100, () => CastW(target));
                Utility.DelayAction.Add(200, () => CastE(target));
                Utility.DelayAction.Add(300, () => CastR(target));
            }
        }

        private void CastQ(Obj_AI_Hero target)
        {
            // Cast Q spell
            _player.Spellbook.CastSpell(SpellSlot.Q, target);
        }

        private void CastW(Obj_AI_Hero target)
        {
            // Cast W spell
            _player.Spellbook.CastSpell(SpellSlot.W, target);
        }

        private void CastE(Obj_AI_Hero target)
        {
            // Cast E spell
            _player.Spellbook.CastSpell(SpellSlot.E, target);
        }

        private void CastR(Obj_AI_Hero target)
        {
            // Cast R spell
            _player.Spellbook.CastSpell(SpellSlot.R, target);
        }
    }
}