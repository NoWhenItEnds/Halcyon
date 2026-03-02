using System;
using Godot;
using Halcyon.Entities.States.Machines;

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


        /// <summary> The state machine currently controlling the entity. </summary>
        public EntityStateMachine? StateMachine { get; protected set; } = null;


        /// <summary> The persistent data for an entity. </summary>
        public EntityData() { }


        /// <summary> Initialise the data a runtime, allowing it to construct the correct state machine from its internal values. </summary>
        /// <param name="entity"> The entity node this data represents. </param>
        public void Initialise(Entity entity)
        {
            StateMachine = ParseEntityKind(entity);
        }


        /// <summary> Initialise a new state machine. </summary>
        /// <typeparam name="T"> The kind of state machine to initialise. </typeparam>
        /// <param name="entity"> A reference to the entity the state machine represents. </param>
        /// <exception cref="ArgumentNullException"/>
        public void SetStateMachine<T>(Entity entity) where T : EntityStateMachine
        {
            StateMachine = (T)(Activator.CreateInstance(typeof(T), [entity]) ??
                throw new ArgumentNullException($"Unable to create state machine of type: '{typeof(T)}'."));
        }


        /// <summary> Try to get the state machine of a specific kind. </summary>
        /// <typeparam name="T"> The kind of state machine. </typeparam>
        /// <param name="stateMachine"> The returned state machine instance. </param>
        /// <returns> Whether the state machine was successfully retrieved. </returns>
        public Boolean TryGetStateMachine<T>(out T? stateMachine) where T : EntityStateMachine
        {
            stateMachine = null;
            if (StateMachine != null && StateMachine is T machine)
            {
                stateMachine = machine;
            }
            return stateMachine != null;
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
