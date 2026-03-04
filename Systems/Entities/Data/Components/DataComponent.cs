using Godot;
using System;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> A component for an entity's data that defines their capabilities. </summary>
    /// <remarks> Works a little like an entity component system. Huh, hence the name. </remarks>
    public abstract partial class DataComponent : Resource, IEquatable<DataComponent>
    {
        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType());


        /// <inheritdoc/>
        public Boolean Equals(DataComponent? other) => other != null ? GetType().Equals(other.GetType()) : false;
    }
}
