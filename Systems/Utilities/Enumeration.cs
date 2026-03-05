using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Halcyon.Utilities
{
    /// <summary> A base class for creating strongly-typed enumerations with behaviour. </summary>
    /// <remarks> Derive from this class and expose static instances as public fields to define the enumeration values. </remarks>
    public abstract class Enumeration : IComparable
    {
        /// <summary> The human-readable name of this enumeration value. </summary>
        public String Name { get; private set; }


        /// <summary> A base class for creating strongly-typed enumerations with behaviour. </summary>
        /// <param name="name"> A human-readable label for this value. </param>
        protected Enumeration(string name)
        {
            Name = name;
        }


        /// <summary> Returns all statically-defined values of the enumeration type.. </summary>
        /// <typeparam name="T"> The concrete enumeration type to retrieve values for. </typeparam>
        /// <returns> Every public static field declared directly. </returns>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(x => x.GetValue(null))
                .Cast<T>();


        /// <inheritdoc/>
        public override String ToString() => Name;


        /// <inheritdoc/>
        /// <remarks> Two instances are equal when they share the same runtime type and the same <see cref="Name"/>. </remarks>
        public override Boolean Equals(object? obj) => obj is Enumeration other && GetType() == other.GetType() && Name == other.Name;


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType(), Name);


        /// <inheritdoc/>
        public Int32 CompareTo(object? obj) => Name.CompareTo(((Enumeration?)obj)?.Name);


        /// <summary> Returns true when both operands are equal by value. </summary>
        public static Boolean operator ==(Enumeration left, Enumeration right) => left is null ? right is null : left.Equals(right);


        /// <summary> Returns true when the operands differ by value. </summary>
        public static Boolean operator !=(Enumeration left, Enumeration right) => !(left == right);
    }
}
