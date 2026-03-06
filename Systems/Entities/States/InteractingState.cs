using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is currently interacting with another entity. The specifics of the interaction are defined by that targeted entity. </summary>
    public class InteractingState : EntityState
    {
        /// <summary> The entity is currently interacting with another entity. The specifics of the interaction are defined by that targeted entity. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public InteractingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            GD.Print(command.ActingEntity.Data.Name);
        }
    }
}
