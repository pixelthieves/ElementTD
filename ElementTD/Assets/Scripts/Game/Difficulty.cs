using System;

namespace Game
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Insane,
        Impossible
    }

    public static class DifficultyExtensions
    {
        public static float GetMultiplyer(this Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 0.5f;
                case Difficulty.Normal:
                    return 0.7f;
                case Difficulty.Hard:
                    return 1f;
                case Difficulty.Insane:
                    return 1.75f;
                case Difficulty.Impossible:
                    return 2.1f;
                default:
                    throw new ArgumentOutOfRangeException("difficulty", difficulty, null);
            }
        }

        public static float GetBonus(this Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return 0.5f;
                case Difficulty.Normal:
                    return 1f;
                case Difficulty.Hard:
                    return 2f;
                case Difficulty.Insane:
                    return 7.5f;
                case Difficulty.Impossible:
                    return 12f;
                default:
                    throw new ArgumentOutOfRangeException("difficulty", difficulty, null);
            }
        }
    }
}