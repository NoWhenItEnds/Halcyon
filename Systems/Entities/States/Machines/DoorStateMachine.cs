using Godot;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States.Machines
{
    /// <summary> A state machine used for doors of all kinds. </summary>
    public class DoorStateMachine : EntityStateMachine
    {
        /// <summary> A state machine used for doors of all kinds. </summary>
        /// <param name="entity"> A reference to the entity controlled by the machine. </param>
        public DoorStateMachine(Entity entity) : base(entity)
        {
            STATES[typeof(OpenedState)] = new OpenedState(entity)
                .WithTransition<UseCommand, ClosedState>();
        }


        /// <inheritdoc/>
        protected override EntityState BuildDefaultState(Entity entity)
        {
            return new ClosedState(entity)
                .WithTransition<UseCommand, OpenedState>();
        }
    }
}
