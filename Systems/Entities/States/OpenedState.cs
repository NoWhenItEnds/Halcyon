using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is open; whatever that means for its specific situation. </summary>
    public class OpenedState : EntityState
    {
        /// <summary> The entity is open; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public OpenedState(Entity entity) : base(entity) { }


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
