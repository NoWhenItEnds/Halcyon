using System;
using System.Collections.Generic;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for doors of all kinds. </summary>
    public partial class DoorStateMachine : EntityStateMachine
    {
        /// <inheritdoc/>
        protected override EntityState BuildDefaultState(Entity entity)
        {
            return new ClosedState(this, entity)
                .WithTransition<UseCommand, OpenedState>();
        }


        /// <inheritdoc/>
        protected override Dictionary<Type, EntityState> BuildStates(Entity entity)
        {
            Dictionary<Type, EntityState> states = new Dictionary<Type, EntityState>();

            states[typeof(OpenedState)] = new OpenedState(this, entity)
                .WithTransition<UseCommand, ClosedState>();

            return states;
        }
    }
}
