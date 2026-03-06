using Godot;
using System;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> A component for an entity's data that defines their capabilities. </summary>
    /// <remarks> Works a little like an entity component system. Huh, hence the name. </remarks>
    public abstract partial class DataComponent : Resource, IEquatable<DataComponent>
    {
        /// <summary> Called to resolve cross-component dependencies and initialise runtime state. </summary>
        /// <param name="data"> The entity data that owns this component. </param>
        public virtual void Initialise(EntityData data) { }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType());


        /// <inheritdoc/>
        public Boolean Equals(DataComponent? other) => other != null ? GetType().Equals(other.GetType()) : false;
    }
}
