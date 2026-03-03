using Godot;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
    public class ClosedState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.CLOSED;


        /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public ClosedState(Entity entity) : base(entity) { }


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
