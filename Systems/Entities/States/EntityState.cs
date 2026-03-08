using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities.States
{
    /// <summary> The basic data object representing an entity's potential state. </summary>
    public abstract class EntityState : IEquatable<EntityState>
    {
        /// <summary> The initial command that triggered the state. </summary>
        /// <remarks> This is set by the state machine. A little bit of a double up, but it means that we don't need to keep setting it on every state. </remarks>
        public EntityCommand? TriggeringCommand { get; set; } = null;


        /// <summary> A reference to the state machine that owns this state. </summary>
        protected readonly EntityStateMachine STATE_MACHINE;

        /// <summary> A reference to the entity manipulated by the state. </summary>
        protected readonly Entity ENTITY;

        /// <summary> A map of the commands to the state they transition into. </summary>
        protected readonly Dictionary<Type, Type> TRANSITIONS = new Dictionary<Type, Type>();

        /// <summary> A reference to the project's resource library. </summary>
        private readonly Halcyon.Managers.ResourceManager RESOURCE_MANAGER = Halcyon.Managers.ResourceManager.Instance;


        /// <summary> The basic data object representing an entity's potential state. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public EntityState(EntityStateMachine stateMachine, Entity entity)
        {
            STATE_MACHINE = stateMachine;
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


        /// <summary> Sees if the state can currently transition. </summary>
        public virtual Boolean CanTransition() => true;


        /// <summary> Initialise the entity state. Called once when the state is created. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        public virtual void Start(EntityCommand command) { }


        /// <summary> Update the entity's state. Called on the physics frame. </summary>
        /// <param name="delta"> The time in second since the last physics frame. </param>
        public virtual void Update(Double delta) { }


        /// <summary> Called just before the state transitions. Does a final cleanup. </summary>
        public virtual void Stop()
        {
            ENTITY.LayeredSprite.Stop();    // TODO - Not sure about this.
        }


        /// <summary> Request the state machine to transition to another state. The transition is deferred to avoid reentrant transitions. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        protected void RequestTransition(EntityCommand command) => Godot.Callable.From(() => STATE_MACHINE.TryTransitionState(command)).CallDeferred();


        protected Godot.Collections.Array<LayeredAnimation> GetCurrentAnimations()
        {
            IEnumerable<LayeredAnimation> animations = RESOURCE_MANAGER.LayeredAnimations.Where(x =>
                x.StateMachine == STATE_MACHINE.GetType().Name && x.EntityState == GetType().Name);
            return new Godot.Collections.Array<LayeredAnimation>(animations);
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType());


        /// <inheritdoc/>
        public Boolean Equals(EntityState? other) => other != null ? GetType() == other.GetType() : false;
    }
}
