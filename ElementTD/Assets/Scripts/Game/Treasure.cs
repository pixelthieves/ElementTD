using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Shared.Scripts;

namespace Game
{
    public class Treasure
    {
        private readonly Dictionary<Element, ElementInfo> elements = new Dictionary<Element, ElementInfo>
        {
            {Element.Gold, new ElementInfo {type = Element.Gold, depletable = true, max = int.MaxValue}},
            {Element.Water, new ElementInfo {type = Element.Water, depletable = false, max = 3}},
            {Element.Fire, new ElementInfo {type = Element.Fire, depletable = false, max = 3}},
            {Element.Nature, new ElementInfo {type = Element.Nature, depletable = false, max = 3}},
            {Element.Light, new ElementInfo {type = Element.Light, depletable = false, max = 3}},
            {Element.Darkness, new ElementInfo {type = Element.Darkness, depletable = false, max = 3}},
            {Element.Soul, new ElementInfo {type = Element.Soul, depletable = false, max = 1}}
        };

        public void AddElement(Element element, int count)
        {
            SetElement(element, GetElement(element) + count);
        }

        public void SubtractElement(Element element, int count)
        {
            SetElement(element, GetElement(element) - count);
        }

        private void SetElement(Element element, int value)
        {
            if (elements.ContainsKey(element))
            {
                elements[element].value = value;
            }
        }

        public int GetElement(Element element)
        {
            return elements.ContainsKey(element) ? elements[element].value : 0;
        }

        public void TransferTo(Treasure treasure)
        {
            foreach (var element in elements)
            {
                treasure.AddElement(element.Key, Math.Max(element.Value.max, 0));
            }
        }

        public void TransferFrom(Treasure treasure)
        {
            treasure.TransferTo(this);
        }

        /**
         * Checks whether a treasure is greater or equal then another treasure.
         *
         * @param treasure to be checked
         * @return {@code True} if this treasure is greater or equal to another treasure, {@code false} otherwise
         */

        public bool Includes(Treasure treasure)
        {
            return elements.All(e => e.Value.value >= treasure.GetElement(e.Key));
        }

        public void Purchase(Treasure treasure)
        {
            elements.ForEach(e =>
            {
                if (e.Value.depletable)
                {
                    e.Value.value -= treasure.GetElement(e.Key);
                }
            });
        }

        public void Add(Treasure treasure)
        {
            elements.ForEach(e => e.Value.value += treasure.GetElement(e.Key));
        }

        public static Treasure FromTresure(Treasure treasure)
        {
            var t = new Treasure();
            foreach (var elementInfo in t.elements)
            {
                elementInfo.Value.value = treasure.GetElement(elementInfo.Key);
            }
            return t;
        }

        public static Treasure FromGold(int count)
        {
            var t = new Treasure();
            t.elements[Element.Gold].value = count;
            return t;
        }

        public static Treasure FromWater(int count)
        {
            var t = new Treasure();
            t.elements[Element.Water].value = count;
            return t;
        }

        public static Treasure FromFire(int count)
        {
            var t = new Treasure();
            t.elements[Element.Fire].value = count;
            return t;
        }

        public static Treasure FromNature(int count)
        {
            var t = new Treasure();
            t.elements[Element.Nature].value = count;
            return t;
        }

        public static Treasure FromLight(int count)
        {
            var t = new Treasure();
            t.elements[Element.Light].value = count;
            return t;
        }

        public static Treasure FromDarkness(int count)
        {
            var t = new Treasure();
            t.elements[Element.Darkness].value = count;
            return t;
        }

        public static Treasure FromSoul(int count)
        {
            var t = new Treasure();
            t.elements[Element.Soul].value = count;
            return t;
        }

        private class ElementInfo
        {
            public Element type;
            public int value;
            public int max;
            public bool depletable;
        }
    }
}