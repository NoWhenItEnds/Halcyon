using Godot;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is standing idle, waiting for an action. </summary>
    public class IdlingState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.IDLING;


        /// <summary> The entity is standing idle, waiting for an action. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public IdlingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            _entity.Velocity = Vector2.Zero;

            // Handle animation.
            _entity.LayeredSprite.Animations = GetCurrentAnimations();
            _entity.LayeredSprite.Play(command.Direction.ToDirection());
        }
    }
}
