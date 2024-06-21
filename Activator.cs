using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace LeagueSharpScript.Activator
{
    class Activator
    {
        private readonly Obj_AI_Hero _player;

        public Activator()
        {
            _player = ObjectManager.Player;
        }

        public void Update()
        {
            if (!ScriptSettings.Activator) return;

            // Get the target
            var target = TargetSelector.GetTarget();

            // Check if the target is valid
            if (!target.IsValidTarget()) return;

            // Use summoner spells
            UseSummonerSpells(target);
        }

        private void UseSummonerSpells(Obj_AI_Hero target)
        {
            // Use the following summoner spells:
            // - Smite
            // - Exhaust
            // - Heal
            // - Barrier

            if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner1) && _player.Spellbook.GetSpell(SpellSlot.Summoner1).Name == "SummonerSmite")
            {
                // Use Smite
                _player.Spellbook.CastSpell(SpellSlot.Summoner1, target);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner2) && _player.Spellbook.GetSpell(SpellSlot.Summoner2).Name == "SummonerExhaust")
            {
                // Use Exhaust
                _player.Spellbook.CastSpell(SpellSlot.Summoner2, target);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner1) && _player.Spellbook.GetSpell(SpellSlot.Summoner1).Name == "SummonerHeal")
            {
                // Use Heal
                _player.Spellbook.CastSpell(SpellSlot.Summoner1);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner2) && _player.Spellbook.GetSpell(SpellSlot.Summoner2).Name == "SummonerBarrier")
            {
                // Use Barrier
                _player.Spellbook.CastSpell(SpellSlot.Summoner2);
            }
        }
    }
}