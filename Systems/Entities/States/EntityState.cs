using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States
{
    /// <summary> The basic data object representing an entity's potential state. </summary>
    public abstract class EntityState : IEquatable<EntityState>
    {
        /// <summary> Whether the state can currently transition. </summary>
        public Func<Boolean> CanTransition { get; protected set; } = () => true;


        /// <summary> A reference to the entity manipulated by the state. </summary>
        protected readonly Entity ENTITY;

        /// <summary> A map of the commands to the state they transition into. </summary>
        protected readonly Dictionary<Type, Type> TRANSITIONS = new Dictionary<Type, Type>();

        /// <summary> A reference to the project's resource library. </summary>
        private readonly Halcyon.Managers.ResourceManager RESOURCE_MANAGER = Halcyon.Managers.ResourceManager.Instance;


        /// <summary> The basic data object representing an entity's potential state. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public EntityState(Entity entity)
        {
            ENTITY = entity;
        }


        /// <summary> Attempt to get the state that will result from the given triggering event. </summary>
        /// <param name="command"> The command triggering a state change. </param>
        /// <param name="state"> The new resulting state. A null indicates that there isn't one. </param>
        /// <returns> Whether there is state transition from the given event. </returns>
        public Boolean TryGetNextState(EntityCommand command, out Type? state) => TRANSITIONS.TryGetValue(command.GetType(), out state);


        /// <summary> Adds a transition to the state. </summary>
        /// <typeparam name="TCommand"> The command triggering a state change. </typeparam>
        /// <typeparam name="TState"> The state that will be transitioned to. </typeparam>
        /// <returns> The altered state. </returns>
        /// <exception cref="ArgumentException"/>
        public EntityState WithTransition<TCommand, TState>() where TCommand : EntityCommand where TState : EntityState
        {
            if (!TRANSITIONS.TryAdd(typeof(TCommand), typeof(TState)))
            {
                throw new ArgumentException("Unable to add the given transition to the entity's state machine.", typeof(TCommand).ToString());
            }
            return this;
        }


        /// <summary> Initialise the entity state. Called once when the state is created. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        public virtual void Start(EntityCommand command) { }


        /// <summary> Update the entity's state. Called on the physics frame. </summary>
        /// <param name="delta"> The time in second since the last physics frame. </param>
        public virtual void Update(Double delta) { }


        /// <summary> Called just before the state transitions. Does a final cleanup. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        public virtual void Stop(EntityCommand command) { ENTITY.LayeredSprite.Stop(); }

        protected Godot.Collections.Array<LayeredAnimation> GetCurrentAnimations()
        {
            // TODO - Implement.
            IEnumerable<LayeredAnimation> animations = RESOURCE_MANAGER.LayeredAnimations.Where(x => x.EntityState == GetType().Name);
            return new Godot.Collections.Array<LayeredAnimation>(animations);
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType());


        /// <inheritdoc/>
        public Boolean Equals(EntityState? other) => other != null ? GetType() == other.GetType() : false;
    }
}
