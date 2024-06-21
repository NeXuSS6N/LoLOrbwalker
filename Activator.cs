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

            // Obtenir la cible
            var target = TargetSelector.GetTarget();

            // Check si la cible est valide
            if (!target.IsValidTarget()) return;

            UseSummonerSpells(target);
        }

        private void UseSummonerSpells(Obj_AI_Hero target)
        {

            if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner1) && _player.Spellbook.GetSpell(SpellSlot.Summoner1).Name == "SummonerSmite")
            {
                //Smite
                _player.Spellbook.CastSpell(SpellSlot.Summoner1, target);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner2) && _player.Spellbook.GetSpell(SpellSlot.Summoner2).Name == "SummonerExhaust")
            {
                //Exhaust
                _player.Spellbook.CastSpell(SpellSlot.Summoner2, target);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner1) && _player.Spellbook.GetSpell(SpellSlot.Summoner1).Name == "SummonerHeal")
            {
                //Heal
                _player.Spellbook.CastSpell(SpellSlot.Summoner1);
            }
            else if (_player.Spellbook.CanUseSpell(SpellSlot.Summoner2) && _player.Spellbook.GetSpell(SpellSlot.Summoner2).Name == "SummonerBarrier")
            {
                //Barrier
                _player.Spellbook.CastSpell(SpellSlot.Summoner2);
            }
        }
    }
}