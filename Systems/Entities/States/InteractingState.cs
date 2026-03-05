using Godot;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is currently interacting with another entity. The specifics of the interaction are defined by that targeted entity. </summary>
    public class InteractingState : EntityState
    {
        // TODO - A better way of handling animations.


        /// <summary> The entity is currently interacting with another entity. The specifics of the interaction are defined by that targeted entity. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public InteractingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            // Trigger another unit's state change.
            GD.Print(command.ActingEntity.Data.Name);
        }
    }
}
