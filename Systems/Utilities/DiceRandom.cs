using System;

namespace Halcyon.Utilities
{
    /// <summary> An implementation of random simulating a dice pool. </summary>
    public class DiceRandom
    {
        /// <summary> The underlying random class to use for generating random numbers. </summary>
        private readonly Random RANDOM;


        /// <summary> An implementation of random simulating a dice pool. </summary>
        public DiceRandom()
        {
            RANDOM = new Random();
        }


        /// <summary> An implementation of random simulating a dice pool. </summary>
        /// <param name="random"> The underlying random class to use for generating random numbers. </param>
        public DiceRandom(Random random)
        {
            RANDOM = random;
        }


        /// <summary> An implementation of random simulating a dice pool. </summary>
        /// <param name="seed"> The seed to use for the random generator. </param>
        public DiceRandom(Int32 seed)
        {
            RANDOM = new Random(seed);
        }


        /// <summary> Perform a standard test where meeting for exceeding the target number will result in a success. </summary>
        /// <param name="poolSize"> The number of dice in the pool. </param>
        /// <param name="targetNumber"> The minimum number on a ten-sided dice the roll needs to equal or succeed to be considered a success. </param>
        /// <param name="doesExplode"> Whether rerolls are allowed on a ten. </param>
        /// <returns> How many successes were rolled. </returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public Int32 StandardTest(Int32 poolSize, Int32 targetNumber = 8, Boolean doesExplode = true)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(poolSize);
            ArgumentOutOfRangeException.ThrowIfLessThan(targetNumber, 1);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(targetNumber, 10);

            Int32 successes = 0;
            Int32 totalRolls = poolSize;
            for (Int32 i = 0; i < totalRolls; i++)
            {
                Int32 result = RANDOM.Next(1, 11);
                if (result >= targetNumber)
                {
                    successes++;
                }

                if (doesExplode && result == 10)
                {
                    totalRolls++;
                }
            }

            return successes;
        }


        /// <summary> Perform a contested test against another. The target number is the result of the contesting result. </summary>
        /// <param name="poolSize"> The number of dice in the initiator's pool. </param>
        /// <param name="otherPoolSize"> The number of dice in the defender's pool. </param>
        /// <param name="targetNumber"> The minimum number on a ten-sided dice the initiator's roll needs to equal or exceed to be considered a success. </param>
        /// <param name="otherTargetNumber"> The minimum number on a ten-sided dice the defender's roll needs to equal or exceed to be considered a success. </param>
        /// <param name="doesExplode"> Whether the initiator rerolls on a ten. </param>
        /// <returns> How many successes above, or below the defender's successes. A zero indicates a success for the initiator, and a loss for the defender. </returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public Int32 ContestedTest(Int32 poolSize, Int32 otherPoolSize, Int32 targetNumber = 8, Int32 otherTargetNumber = 8, Boolean doesExplode = true)
        {
            Int32 otherSuccesses = StandardTest(otherPoolSize, otherTargetNumber, doesExplode: false);
            return StandardTest(poolSize, targetNumber, doesExplode) - otherSuccesses;
        }


        /// <summary> Evaluate a number of successes into a readable result type. </summary>
        /// <param name="successes"> The number of successes in a dice roll. </param>
        /// <returns> The resolved dice result type. </returns>
        public static DiceResultType EvaluateResult(Int32 successes)
        {
            DiceResultType result = DiceResultType.NONE;
            if(successes > 4)
            {
                result = DiceResultType.EXCEPTIONAL_SUCCESS;
            }
            else if(successes > 0 && successes <= 4)
            {
                result = DiceResultType.SUCCESS;
            }
            else
            {
                result = DiceResultType.FAILURE;
            }
            return result;
        }
    }


    /// <summary> The kinds of results a dice roll can evaluate to. </summary>
    public enum DiceResultType
    {
        NONE,
        FAILURE,
        SUCCESS,
        EXCEPTIONAL_SUCCESS
    }
}
