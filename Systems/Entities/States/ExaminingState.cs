using Godot;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities;

namespace Halcyon.Entities.States
{
    /// <summary> The entity is in the process of examining another entity. </summary>
    public class ExaminingState : EntityState
    {
        /// <summary> The entity is in the process of examining another entity. </summary>
        /// <param name="stateMachine"> A reference to the owning state machine. </param>
        /// <param name="entity"> A reference to the entity. </param>
        public ExaminingState(EntityStateMachine stateMachine, Entity entity) : base(stateMachine, entity) { }


        /// <inheritdoc/>
        public override void Start(EntityCommand command)
        {
            ENTITY.Velocity = Vector2.Zero;

            // Handle animation.
            ENTITY.LayeredSprite.Animations = GetCurrentAnimations();
            ENTITY.LayeredSprite.Play(command.Direction.ToDirection());

            if (command is ExamineCommand examine)
            {
                GD.Print($"{ENTITY.Data.Name} looked at {examine.TargetEntity.Data.Name}");
            }
        }
    }
}
