using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;

namespace Halcyon.Entities.Data
{
    /// <summary> The persistent data for an entity. </summary>
    public abstract partial class EntityData : Resource, IEquatable<EntityData>
    {
        /// <summary> The entity's personal name. </summary>
        [ExportGroup("General")]
        [Export] public EntityName Name { get; set; } = EntityName.Random(EntityGender.NONE);

        /// <summary> The current gender / sex of the entity. </summary>
        [Export] public EntityGender Gender { get; set; } = EntityGender.NONE;

        /// <summary> The identifying race / type of the entity. </summary>
        /// <example> human / door / chest </example>
        [Export] public String EntityKind { get; set; } = String.Empty;


        /// <summary> The dimensions of the collision shape, in pixels. </summary>
        /// <remarks> For a circle, this is the radius. </remarks>
        [ExportGroup("Collision")]
        [Export] public Vector2 CollisionSize { get; set; } = Vector2.Zero; // TODO - Have based upon entity size?

        /// <summary> The collision's shape. </summary>
        [Export] public Shape CollisionShape { get; set; } = Shape.NONE;    // TODO - Implement collision shape changes. With Tool-dynamic updates.


        /// <summary> The components that define an entity's advanced functionality. </summary>
        [ExportGroup("Components")]
        [Export] public Godot.Collections.Array<DataComponent> Components
        {
            get => new Godot.Collections.Array<DataComponent>(_components);
            set
            {
                // TODO - Implement set / onchange.
            }
        }

        /// <summary> The components that define an entity's advanced functionality. </summary>
        private HashSet<DataComponent> _components = new HashSet<DataComponent>();


        /// <summary> The persistent data for an entity. </summary>
        public EntityData() { }


        /// <summary> Initialise the data at runtime, constructing the correct state machine and assigning it to the entity. </summary>
        /// <param name="entity"> The entity node this data represents. </param>
        public void Initialise(Entity entity)
        {
            // Need to give Godot time to catch up on initial run.
            CallDeferred(nameof(InitialiseLogic), [entity]);
        }


        /// <summary> The actual logic of the initialisation. We need to wrap this as it needs to be called as a deferred function to allow Godot to update. </summary>
        /// <param name="entity"> The entity node this data represents. </param>
        protected void InitialiseLogic(Entity entity)
        {
            entity.StateMachine = ParseEntityKind(entity);
        }


        /// <summary> Attempt to parse the entity kind into the correct state machine. Will return an exception if this isn't possible. </summary>
        /// <param name="entity"> A reference to the entity the state machine represents. </param>
        /// <returns> The parsed state machine. </returns>
        /// <exception cref="ArgumentNullException"/>
        protected abstract EntityStateMachine ParseEntityKind(Entity entity);


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Name);


        /// <inheritdoc/>
        public Boolean Equals(EntityData? other) => other != null ? Name.Equals(other.Name) : false;
    }
}
