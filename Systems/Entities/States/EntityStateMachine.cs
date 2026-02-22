using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States
{
    /// <summary> A state machine for entity states. </summary>
    public class EntityStateMachine
    {
        /// <summary> The current state of the state machine. </summary>
        public EntityState CurrentState { get; private set; }


        /// <summary> All the possible states the state machine can transition to. </summary>
        private readonly HashSet<EntityState> STATES = new HashSet<EntityState>();


        /// <summary> A state machine for entity states. </summary>
        /// <param name="entity"> A reference to the entity controlled by the state. </param>
        public EntityStateMachine(Entity entity)
        {
            // Set the initial state.
            EntityState initialState = new IdlingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>();

            CurrentState = initialState;

            STATES.Add(initialState);

            STATES.Add(new WalkingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>());

            STATES.Add(new SprintingState(entity)
                .WithTransition<SprintCommand, SprintingState>()
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>());
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
                CurrentState = STATES.FirstOrDefault(x => x.GetType() == newState) ??
                    throw new ArgumentNullException($"Next transition state doesn't exist on this {GetType()}.", newState.GetType().ToString());
                CurrentState.Start(command);
                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
