using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for doors of all kinds. </summary>
    public class DoorStateMachine : EntityStateMachine
    {
        /// <summary> A state machine used for doors of all kinds. </summary>
        /// <param name="entity"> A reference to the entity controlled by the state. </param>
        public DoorStateMachine(Entity entity)
        {
            EntityState closedState = new ClosedState(entity)
                .WithTransition<UseCommand, OpenedState>();

            CurrentState = closedState;

            STATES.Add(closedState);

            STATES.Add(new OpenedState(entity)
                .WithTransition<UseCommand, ClosedState>());
        }
    }
}
