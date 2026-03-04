using System;
using System.Collections.Generic;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A machine to control the various states an entity can exist within and move between. </summary>
    public abstract class EntityStateMachine
    {
        /// <summary> The current state of the state machine. </summary>
        public EntityState CurrentState { get; protected set; }


        /// <summary> The machine's starting state. The state it will also default to if something goes wrong. </summary>
        protected readonly EntityState DEFAULT_STATE;

        /// <summary> All the possible states the state machine can transition to, keyed by type. </summary>
        protected readonly Dictionary<Type, EntityState> STATES = new Dictionary<Type, EntityState>();


        /// <summary> A machine to control the various states an entity can exist within and move between. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        public EntityStateMachine(Entity entity)
        {
            EntityState defaultState = BuildDefaultState(entity);

            DEFAULT_STATE = defaultState;
            STATES[defaultState.GetType()] = defaultState;
            CurrentState = defaultState;
            CurrentState.Start(new IdleCommand(entity));  // Initialise the state machine with an idle command.
        }


        /// <summary> Attempt to transition from one state to another. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        /// <returns> Whether there was a successful transition. </returns>
        /// <exception cref="ArgumentNullException"/>
        public Boolean TryTransitionState(EntityCommand command)
        {
            Boolean isSuccessful = false;
            if (CurrentState.CanTransition() && CurrentState.TryGetNextState(command, out Type? newState) && newState != null)
            {
                CurrentState.Stop(command);
                CurrentState = STATES.TryGetValue(newState, out EntityState? next) ? next
                    : throw new InvalidOperationException($"State {newState} is not registered on {GetType()}.");
                CurrentState.Start(command);

                isSuccessful = true;
            }
            return isSuccessful;
        }


        /// <summary> Build and set the machine's initial / default state. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        /// <returns> The machine's starting state. </returns>
        protected abstract EntityState BuildDefaultState(Entity entity);
    }
}
