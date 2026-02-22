#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A machine to control the various states an entity can exist within and move between. </summary>
    public abstract class EntityStateMachine
    {
        /// <summary> The current state of the state machine. </summary>
        public EntityState CurrentState { get; protected set; }


        /// <summary> All the possible states the state machine can transition to. </summary>
        protected readonly HashSet<EntityState> STATES = new HashSet<EntityState>();


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
