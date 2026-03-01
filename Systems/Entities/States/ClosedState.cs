using Godot;
using Halcyon.Animations;
using Halcyon.Entities.EntityCommands;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
    public class ClosedState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.IDLING;


        /// <summary> The entity is closed; whatever that means for its specific situation. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public ClosedState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            _entity.Velocity = Vector2.Zero;
        }
    }
}
