using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Treasure : IEnumerable<KeyValuePair<Element, int>>
    {
        [SerializeField]
        private Dictionary<Element, int> elements = new Dictionary<Element, int>();

        public Treasure()
        {
        }

        public int this[Element element]
        {
            get { return elements.ContainsKey(element) ? elements[element] : 0; }
            set
            {
                if (value < 0) throw new InvalidOperationException("Value cannot be lesser than zero.");
                elements[element] = value;
                if (value == 0) elements.Remove(element);
            }
        }

        public bool Includes(Treasure treasure)
        {
            return elements.All(e => e.Value >= this[e.Key]);
        }

        public static Treasure FromTresure(Treasure treasure)
        {
            var t = new Treasure();
            foreach (var e in treasure.elements)
            {
                t[e.Key] = treasure[e.Key];
            }
            return t;
        }

        public static Treasure FromGold(int count)
        {
            var t = new Treasure();
            t.elements[Element.Gold] = count;
            return t;
        }

        public static Treasure FromWater(int count)
        {
            var t = new Treasure();
            t.elements[Element.Water] = count;
            return t;
        }

        public static Treasure FromFire(int count)
        {
            var t = new Treasure();
            t.elements[Element.Fire] = count;
            return t;
        }

        public static Treasure FromNature(int count)
        {
            var t = new Treasure();
            t.elements[Element.Nature] = count;
            return t;
        }

        public static Treasure FromLight(int count)
        {
            var t = new Treasure();
            t.elements[Element.Light] = count;
            return t;
        }

        public static Treasure FromDarkness(int count)
        {
            var t = new Treasure();
            t.elements[Element.Darkness] = count;
            return t;
        }

        public static Treasure FromSoul(int count)
        {
            var t = new Treasure();
            t.elements[Element.Soul] = count;
            return t;
        }

        public IEnumerator<KeyValuePair<Element, int>> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}