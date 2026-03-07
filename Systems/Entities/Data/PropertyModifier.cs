using System;

namespace Halcyon.Entities.Data
{
    /// <summary> A generic data class that modifies a given property. In game terms, it represents something, such as an effect or item, altering the original property. </summary>
    public record PropertyModifier
    {
        /// <summary> How the modifier affects the value. </summary>
        public enum PropertyOperation
        {
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE
        }

        /// <summary> The order the modifier should be applied to the property. Lower is applied first. </summary>
        public Int32 Priority { get; init; } = 0;

        /// <summary> What object is applying the modification. </summary>
        /// <remarks> A null indicates that there isn't one. </remarks>
        public Object? Source { get; init; } = null;

        /// <summary> How the modifier is applied to the property. </summary>
        public required PropertyOperation Operation { get; init; }

        /// <summary> The actual value that affects the property. </summary>
        public required Single Value { get; init; }


        /// <summary> Functionally apply the modifier to the given current value. </summary>
        /// <param name="currentValue"> The current value to modify. </param>
        /// <returns> The result of the operation. </returns>
        public Single Apply(Single currentValue)
        {
            Single result = currentValue;

            switch (Operation)
            {
                case PropertyOperation.ADD:
                    result = currentValue + Value;
                    break;
                case PropertyOperation.SUBTRACT:
                    result = currentValue - Value;
                    break;
                case PropertyOperation.MULTIPLY:
                    result = currentValue * Value;
                    break;
                case PropertyOperation.DIVIDE:
                    result = Value != 0 ? currentValue / Value : currentValue;  // Prevent zero division by returning the original value.
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
