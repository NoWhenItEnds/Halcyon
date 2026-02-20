using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities.ActorCommands;
using Halcyon.Utilities;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> The basic data object representing an actor's potential state. </summary>
    public abstract class ActorState : IEquatable<ActorState>
    {
        /// <summary> The prefix of the animations to use for this state. </summary>
        public abstract String AnimationPrefix { get; }


        /// <summary> A map of the commands to the state they transition into. </summary>
        protected readonly Dictionary<Type, Type> TRANSITIONS = new Dictionary<Type, Type>();

        /// <summary> A reference to the actor. </summary>
        protected readonly ActorEntity ENTITY;


        /// <summary> The basic data object representing an actor's potential state. </summary>
        /// <param name="entity"> A reference to the actor. </param>
        public ActorState(ActorEntity entity)
        {
            ENTITY = entity;
        }


        /// <summary> Attempt to get the state that will result from the given triggering event. </summary>
        /// <param name="command"> The command triggering a state change. </param>
        /// <param name="state"> The new resulting state. A null indicates that there isn't one. </param>
        /// <returns> Whether there is state transition from the given event. </returns>
        public Boolean TryGetNextState(ActorCommand command, out Type? state) => TRANSITIONS.TryGetValue(command.GetType(), out state);


        /// <summary> Adds a transition to the state. </summary>
        /// <typeparam name="TCommand"> The command triggering a state change. </typeparam>
        /// <typeparam name="TState"> The state that will be transitioned to. </typeparam>
        /// <returns> The altered state. </returns>
        /// <exception cref="ArgumentException"/>
        public ActorState WithTransition<TCommand, TState>() where TCommand : ActorCommand where TState : ActorState
        {
            if (!TRANSITIONS.TryAdd(typeof(TCommand), typeof(TState)))
            {
                throw new ArgumentException("Unable to add the given transition to the actor's state machine.", typeof(TCommand).ToString());
            }
            return this;
        }


        /// <summary> Initialise the actor state. Called once when the state is created. </summary>
        public virtual void Start() { }


        /// <summary> Update the actor's state. Called on the physics frame. </summary>
        /// <param name="delta"> The time in second since the last physics frame. </param>
        public virtual void Update(Vector2 direction)
        {
            ENTITY.SetAnimation(AnimationPrefix, direction.ToDirection());
        }


        /// <summary> Called just before the state transitions. Does a final cleanup. </summary>
        public virtual void Stop() { }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(GetType());


        /// <inheritdoc/>
        public Boolean Equals(ActorState? other) => other != null ? GetType() == other.GetType() : false;
    }
}
