using System.Collections.Generic;
using Assets.Scripts.utils;
using Assets.Shared.Scripts;
using Vexe.Runtime.Types;

namespace Game
{
    public class Wallet : BetterBehaviour
    {
        private static Dictionary<Element, ElementLimit> elementLimits = new Dictionary<Element, ElementLimit>
        {
            {Element.Gold, new ElementLimit {Depletable = true, Max = int.MaxValue}},
            {Element.Water, new ElementLimit {Depletable = false, Max = 3}},
            {Element.Fire, new ElementLimit {Depletable = false, Max = 3}},
            {Element.Nature, new ElementLimit {Depletable = false, Max = 3}},
            {Element.Light, new ElementLimit {Depletable = false, Max = 3}},
            {Element.Darkness, new ElementLimit {Depletable = false, Max = 3}},
            {Element.Soul, new ElementLimit {Depletable = false, Max = 1}}
        };

        public Treasure Treasure;

        public void Claim(Treasure claim)
        {
            foreach (var type in Utils.getEnumValues<Element>())
            {
                Treasure[type] = claim[type];
                claim[type] = 0;
            }
        }

        public void Purchase(Treasure treasure)
        {
            treasure.ForEach(e =>
            {
                if (elementLimits[e.Key].Depletable)
                {
                    Treasure[e.Key] -= e.Value;
                }
            });
        }

        public int GetGold()
        {
            return Treasure[Element.Gold];
        }

        public void AddGold(int gold)
        {
            Treasure[Element.Gold] = gold;
        }
    }

    public class ElementLimit
    {
        public int Max;
        public bool Depletable;

        public override string ToString()
        {
            return string.Format("Max: {0}, Depletable: {1}", Max, Depletable);
        }
    }
}