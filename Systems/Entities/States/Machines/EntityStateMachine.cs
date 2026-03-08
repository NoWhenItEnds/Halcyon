#nullable disable warnings
using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A machine to control the various states an entity can exist within and move between. </summary>
    public abstract partial class EntityStateMachine : Node
    {
        /// <summary> The current state of the state machine. </summary>
        public EntityState CurrentState { get; protected set; }


        /// <summary> The entity this machine manipulates. </summary>
        protected Entity _entity { get; private set; }

        /// <summary> The machine's starting state. The state it will also default to if something goes wrong. </summary>
        protected EntityState _defaultState { get; private set; }

        /// <summary> All the possible states the state machine can transition to, keyed by type. </summary>
        protected readonly Dictionary<Type, EntityState> STATES = new Dictionary<Type, EntityState>();


        /// <inheritdoc/>
        public sealed override void _Ready()
        {
            // We only want to build ourselves at runtime.
            if(!Engine.IsEditorHint())
            {
                _entity = GetParent<Entity>() ?? throw new ArgumentNullException($"The state machine, '{GetType()}', doesn't have an entity parent.");

                // Build the states.
                _defaultState = BuildDefaultState(_entity);
                STATES[_defaultState.GetType()] = _defaultState;
                foreach (KeyValuePair<Type, EntityState> state in BuildStates(_entity))
                {
                    if (!STATES.TryAdd(state.Key, state.Value))
                    {
                        GD.PushError($"Unable to add state with the key '{state.Key}' to the state machine for {_entity.GetType()}.");
                    }
                }

                CurrentState = _defaultState;
                CallDeferred(nameof(InitialiseInitialState));
            }
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            if (!Engine.IsEditorHint())
            {
                CurrentState.Update(delta);
            }
        }


        /// <summary> Attempt to transition from one state to another. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        /// <returns> Whether there was a successful transition. </returns>
        /// <exception cref="InvalidOperationException"/>
        public Boolean TryTransitionState(EntityCommand command)
        {
            Boolean isSuccessful = false;
            if (CurrentState.CanTransition() && CurrentState.TryGetNextState(command, out Type? newState) && newState != null)
            {
                CurrentState.Stop();
                CurrentState.TriggeringCommand = null;

                CurrentState = STATES.TryGetValue(newState, out EntityState? next) ? next
                    : throw new InvalidOperationException($"State {newState} is not registered on {GetType()}.");

                CurrentState.TriggeringCommand = command;
                CurrentState.Start(command);

                isSuccessful = true;
            }
            return isSuccessful;
        }


        /// <summary> Build and return the machine's initial / default state. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        /// <returns> The machine's starting state. </returns>
        protected abstract EntityState BuildDefaultState(Entity entity);


        /// <summary> Build all non-default states for this machine. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        /// <returns> A map of the new, non-default states. </returns>
        protected abstract Dictionary<Type, EntityState> BuildStates(Entity entity);


        /// <summary> Request that the machine transitions back to its default state. The transition is deferred to avoid reentrancy. </summary>
        public void RequestDefaultTransition()
        {
            Callable.From(() =>
            {
                if (CurrentState.CanTransition())
                {
                    CurrentState.Stop();
                    CurrentState.TriggeringCommand = null;
                    CurrentState = _defaultState;
                    CurrentState.Start(new IdleCommand(_entity));
                }
            }).CallDeferred();
        }


        /// <summary> Start the machine's initial state with the correct command. </summary>
        /// <remarks We need to have this separate as it needs to be deferred to allow Godot time to initialise the resources this depends upon. </remarks>
        protected virtual void InitialiseInitialState()
        {
            CurrentState.Start(new IdleCommand(_entity));
        }
    }
}
