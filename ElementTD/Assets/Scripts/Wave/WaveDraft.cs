using System.Collections.Generic;
using Creep;

namespace Game
{
    public class WaveDraft
    {
        private const int ElementLvl1Multiplier = 4;
        private const int ElementLvl2Multiplier = 8;
        private const int ElementLvl3Multiplier = 12;
        private const int ElementLvl4Multiplier = 20;

        public WaveDraft(CreepName name, int health, Treasure treasure, params CreepTraits[] traits)
        {
            Name = name;
            Health = health;
            Treasure = treasure;
            Traits = traits;
        }

        public CreepTraits[] Traits { get; private set; }
        public Treasure Treasure { get; private set; }
        public int Health { get; private set; }
        public CreepName Name { get; private set; }

        public static List<WaveDraft> GetNormal()
        {
            return new List<WaveDraft>
            {
                new WaveDraft(CreepName.Hoothoot, 38, Treasure.FromGold(1), CreepTraits.Normal),
                new WaveDraft(CreepName.Ledian, 44, Treasure.FromGold(1), CreepTraits.Normal),
                new WaveDraft(CreepName.Crobat, 52, Treasure.FromGold(1), CreepTraits.Fast),
                new WaveDraft(CreepName.Lanturn, 60, Treasure.FromGold(1), CreepTraits.Normal),
                new WaveDraft(CreepName.Quagsire, 71, Treasure.FromGold(2), CreepTraits.Normal),
                new WaveDraft(CreepName.Espeon, 82, Treasure.FromGold(2), CreepTraits.Swarm),
                new WaveDraft(CreepName.Forretress, 96, Treasure.FromGold(2), CreepTraits.Normal),
                new WaveDraft(CreepName.Snubbull, 113, Treasure.FromGold(2), CreepTraits.Normal),
                new WaveDraft(CreepName.Corsola, 132, Treasure.FromGold(2), CreepTraits.Resurrect),
                new WaveDraft(CreepName.Miltank, 154, Treasure.FromGold(3), CreepTraits.Normal),
                new WaveDraft(CreepName.Entei, 181, Treasure.FromGold(3), CreepTraits.Normal),
                new WaveDraft(CreepName.Blaziken, 211, Treasure.FromGold(3), CreepTraits.Fast),
                new WaveDraft(CreepName.Wurmple, 253, Treasure.FromGold(3), CreepTraits.Normal),
                new WaveDraft(CreepName.Beautifly, 284, Treasure.FromGold(4), CreepTraits.Normal),
                new WaveDraft(CreepName.Nuzleaf, 338, Treasure.FromGold(4), CreepTraits.Spawn),
                new WaveDraft(CreepName.Pelipper, 395, Treasure.FromGold(5), CreepTraits.Normal),
                new WaveDraft(CreepName.Kirlia, 463, Treasure.FromGold(5), CreepTraits.Normal),
                new WaveDraft(CreepName.Breloom, 541, Treasure.FromGold(6), CreepTraits.Invisible),
                new WaveDraft(CreepName.Shedinja, 633, Treasure.FromGold(6), CreepTraits.Normal),
                new WaveDraft(CreepName.Whismur, 741, Treasure.FromGold(7), CreepTraits.Fast),
                new WaveDraft(CreepName.Loudred, 869, Treasure.FromGold(7), CreepTraits.Normal),
                new WaveDraft(CreepName.Exploud, 1018, Treasure.FromGold(8), CreepTraits.Swarm),
                new WaveDraft(CreepName.Delcatty, 1194, Treasure.FromGold(9), CreepTraits.Normal),
                new WaveDraft(CreepName.Sableye, 1194, Treasure.FromGold(10), CreepTraits.Resurrect),
                new WaveDraft(CreepName.Mawile, 1400, Treasure.FromGold(11), CreepTraits.Normal),
                new WaveDraft(CreepName.Lairon, 1641, Treasure.FromGold(12), CreepTraits.Invisible),
                new WaveDraft(CreepName.Flygon, 1924, Treasure.FromGold(13), CreepTraits.Normal),
                new WaveDraft(CreepName.Whiscash, 2256, Treasure.FromGold(14), CreepTraits.Normal),
                new WaveDraft(CreepName.Claydol, 2645, Treasure.FromGold(16), CreepTraits.Spawn),
                new WaveDraft(CreepName.Lileep, 3102, Treasure.FromGold(17), CreepTraits.Normal),
                new WaveDraft(CreepName.Feebas, 3637, Treasure.FromGold(19), CreepTraits.Normal),
                new WaveDraft(CreepName.Kecleon, 4273, Treasure.FromGold(21), CreepTraits.Fast),
                new WaveDraft(CreepName.Banette, 5021, Treasure.FromGold(23), CreepTraits.Normal),
                new WaveDraft(CreepName.Duskull, 5900, Treasure.FromGold(26), CreepTraits.Swarm),
                new WaveDraft(CreepName.Tropius, 6932, Treasure.FromGold(28), CreepTraits.Normal),
                new WaveDraft(CreepName.Huntail, 8145, Treasure.FromGold(31), CreepTraits.Resurrect),
                new WaveDraft(CreepName.Kyogre, 9570, Treasure.FromGold(34), CreepTraits.Normal),
                new WaveDraft(CreepName.Prinplup, 11245, Treasure.FromGold(37), CreepTraits.Spawn),
                new WaveDraft(CreepName.Wormadam, 13213, Treasure.FromGold(41), CreepTraits.Normal),
                new WaveDraft(CreepName.Bronzong, 15525, Treasure.FromGold(45), CreepTraits.Fast),
                new WaveDraft(CreepName.Yanmega, 18242, Treasure.FromGold(50), CreepTraits.Normal),
                new WaveDraft(CreepName.Glaceon, 21526, Treasure.FromGold(55), CreepTraits.Invisible),
                new WaveDraft(CreepName.Mamoswine, 25381, Treasure.FromGold(60), CreepTraits.Normal),
                new WaveDraft(CreepName.Palkia, 29972, Treasure.FromGold(66), CreepTraits.Fast),
                new WaveDraft(CreepName.Regigigas, 35367, Treasure.FromGold(73), CreepTraits.Fast),
                new WaveDraft(CreepName.Giratina, 41733, Treasure.FromGold(80), CreepTraits.Healing),
                new WaveDraft(CreepName.Manaphy, 54904, Treasure.FromGold(88), CreepTraits.Fast),
                new WaveDraft(CreepName.Darkrai, 49004, Treasure.FromGold(97), CreepTraits.Normal),
                new WaveDraft(CreepName.Snivy, 68569, Treasure.FromGold(107), CreepTraits.Swarm),
                new WaveDraft(CreepName.Tepig, 80911, Treasure.FromGold(117), CreepTraits.Spawn),
                new WaveDraft(CreepName.Emboar, 95475, Treasure.FromGold(129), CreepTraits.Invisible),
                new WaveDraft(CreepName.Watchog, 106876, Treasure.FromGold(142), CreepTraits.Resurrect),
                new WaveDraft(CreepName.Herdier, 119320, Treasure.FromGold(156), CreepTraits.Normal),
                new WaveDraft(CreepName.Liepard, 131764, Treasure.FromGold(172), CreepTraits.Healing),
                new WaveDraft(CreepName.Panpour, 144208, Treasure.FromGold(189), CreepTraits.Invisible),
                new WaveDraft(CreepName.Tirtouga, 156052, Treasure.FromGold(208), CreepTraits.Healing),
                new WaveDraft(CreepName.Zorua, 169096, Treasure.FromGold(229), CreepTraits.Resurrect),
                new WaveDraft(CreepName.Klinklang, 118540, Treasure.FromGold(252), CreepTraits.Spawn),
                new WaveDraft(CreepName.Lampent, 193984, Treasure.FromGold(277), CreepTraits.Fast),
                new WaveDraft(CreepName.Druddigon, 206428, Treasure.FromGold(304), CreepTraits.Normal),
                new WaveDraft(CreepName.Hydreigon, 218872, Treasure.FromGold(0), CreepTraits.Boss)
            };
        }

        public static List<WaveDraft> GetElement()
        {
            return new List<WaveDraft>
            {
                new WaveDraft(CreepName.Flaaffy, ElementLvl1Multiplier, Treasure.FromFire(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Ampharos, ElementLvl2Multiplier, Treasure.FromFire(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Yanma, ElementLvl3Multiplier, Treasure.FromFire(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Bellossom, ElementLvl1Multiplier, Treasure.FromLight(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Politoed, ElementLvl2Multiplier, Treasure.FromLight(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Skiploom, ElementLvl3Multiplier, Treasure.FromLight(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Marill, ElementLvl1Multiplier, Treasure.FromWater(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Azumarill, ElementLvl2Multiplier, Treasure.FromWater(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Wooper, ElementLvl3Multiplier, Treasure.FromWater(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Sudowoodo, ElementLvl1Multiplier, Treasure.FromNature(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Sunkern, ElementLvl2Multiplier, Treasure.FromNature(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Sunflora, ElementLvl3Multiplier, Treasure.FromNature(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Umbreon, ElementLvl1Multiplier, Treasure.FromDarkness(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Aipom, ElementLvl2Multiplier, Treasure.FromDarkness(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Hoppip, ElementLvl3Multiplier, Treasure.FromDarkness(1), CreepTraits.Boss),
                new WaveDraft(CreepName.Electivire, ElementLvl4Multiplier, Treasure.FromSoul(1), CreepTraits.Boss)
            };
        }
    }
}