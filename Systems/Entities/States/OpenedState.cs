using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is open; whatever that means for its specific situation. </summary>
    public class OpenedState : EntityState
    {
        /// <summary> The entity is open; whatever that means for its specific situation. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public OpenedState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Velocity = Vector2.Zero;

            // Handle animation.
            ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
            ENTITY.LayeredSprite.Play(Direction.NW);    // TODO - More than hard set.
        }
    }
}
