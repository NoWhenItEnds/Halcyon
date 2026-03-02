using Godot;
using Halcyon.Animations;
using Halcyon.Entities.Data;
using Halcyon.Entities.EntityCommands;
using Halcyon.Utilities;
using System;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is walking across the ground. </summary>
    public class WalkingState : EntityState
    {
        /// <inheritdoc/>
        protected override AnimationKind _animationKind { get; } = AnimationKind.WALKING;


        /// <summary> How fast the walking movement speed is. </summary>
        private readonly Single MOVE_SPEED = 500f;


        /// <summary> The entity is walking across the ground. </summary>
        /// <param name="entity"> A reference to the entity. </param>
        public WalkingState(Entity entity) : base(entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            if (ENTITY.TryGetData<ActorData>(out ActorData? data) && data != null)
            {
                Single speed = data.SpeedStat.CurrentValue * MOVE_SPEED;
                ENTITY.Velocity = command.Direction * speed;

                // Handle animation.
                ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
                ENTITY.LayeredSprite.Play(command.Direction.ToDirection());
            }
            else
            {
                throw new ArgumentNullException($"Despite being a '{ENTITY.GetType()}', the entity doesn't possess '{typeof(ActorData)}' data, which this state, {GetType()}, requires.");
            }
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            ENTITY.Velocity *= (Single)delta;
            Boolean isCollision = ENTITY.MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = ENTITY.GetLastSlideCollision();
            }
        }


        /// <inheritdoc/>
        public override void Stop(EntityCommand command) { }
    }
}
