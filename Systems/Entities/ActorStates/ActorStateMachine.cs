using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.Entities.ActorCommands;

namespace Halcyon.Entities.ActorStates
{
    /// <summary> A state machine for actor states. </summary>
    public class ActorStateMachine
    {
        /// <summary> The current state of the state machine. </summary>
        public ActorState CurrentState { get; private set; }


        /// <summary> All the possible states the state machine can transition to. </summary>
        private readonly HashSet<ActorState> STATES = new HashSet<ActorState>();


        /// <summary> A state machine for actor states. </summary>
        /// <param name="entity"> A reference to the actor controlled by the state. </param>
        public ActorStateMachine(ActorEntity entity)
        {
            // Set the initial state.
            ActorState initialState = new IdlingState(entity)
                .WithTransition<WalkCommand, WalkingState>()
                .WithTransition<SprintCommand, SprintingState>();

            CurrentState = initialState;

            STATES.Add(initialState);

            STATES.Add(new WalkingState(entity)
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<SprintCommand, SprintingState>());

            STATES.Add(new SprintingState(entity)
                .WithTransition<IdleCommand, IdlingState>()
                .WithTransition<WalkCommand, WalkingState>());
        }


        /// <summary> Handle the input sent to the being by it's controller. </summary>
        /// <param name="input"> The input data class to interpret. </param>
        public void HandleCommand(ActorCommand command)
        {
            TryTransitionState(command);
            CurrentState.Update(command.Direction);
        }


        /// <summary> Attempt to transition from one state to another. </summary>
        /// <param name="command"> The command triggering the state change. </param>
        /// <returns> Whether there was a successful transition. </returns>
        /// <exception cref="ArgumentNullException"/>
        private Boolean TryTransitionState(ActorCommand command)
        {
            Boolean isSuccessful = false;
            if (CurrentState.TryGetNextState(command, out Type? newState) && newState != null)
            {
                CurrentState.Stop();
                CurrentState = STATES.FirstOrDefault(x => x.GetType() == newState) ??
                    throw new ArgumentNullException("Next transition state doesn't exist on ActorStateMachine.", newState.GetType().ToString());
                CurrentState.Start();
                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
