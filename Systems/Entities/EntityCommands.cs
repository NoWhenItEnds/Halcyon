using Godot;

namespace Halcyon.Entities.EntityCommands
{
    /// <summary> A data object containing a command for an entity. </summary>
    public abstract class EntityCommand
    {
        /// <summary> The entity that is performing the command. </summary>
        public Entity ActingEntity { get; init; }

        /// <summary> The direction associated with the command. </summary>
        public Vector2 Direction { get; init; } = Vector2.Zero;

        /// <summary> The global position targeted by the command. </summary>
        public Vector2 TargetPosition { get; init; } = Vector2.Zero;

        /// <summary> The entity targeted by the command. </summary>
        public Entity? TargetEntity { get; init; } = null;


        /// <summary> A data object containing a command for an entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        public EntityCommand(Entity actingEntity)
        {
            ActingEntity = actingEntity;    // TODO - Just use direction instead? See how AI will work.
            Direction = actingEntity.Velocity.Normalized(); // By default, the direction will be the current direction of the acting entity.
        }
    }


    /// <summary> A command telling the entity to stand around and look pretty. </summary>
    public class IdleCommand : EntityCommand
    {
        /// <summary> A command telling the entity to stand around and look pretty. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Entity actingEntity, Vector2 direction) : base(actingEntity)
        {
            Direction = direction;
        }


        /// <summary> A command telling the entity to stand around and look pretty. </summary>
        public IdleCommand(Entity actingEntity) : base(actingEntity) { }
    }


    /// <summary> A command telling the entity to walk. </summary>
    public class WalkCommand : EntityCommand
    {
        /// <summary> A command telling the entity to walk. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public WalkCommand(Entity actingEntity, Vector2 direction) : base(actingEntity)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the entity to sprint. </summary>
    public class SprintCommand : EntityCommand
    {
        /// <summary> A command telling the entity to sprint. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public SprintCommand(Entity actingEntity, Vector2 direction) : base(actingEntity)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the entity to inspect a nearby entity. </summary>
    public class ExamineCommand : EntityCommand
    {
        /// <summary> A command telling the entity to inspect a nearby entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public ExamineCommand(Entity actingEntity, Entity targetEntity) : base(actingEntity)
        {
            TargetEntity = targetEntity;
        }
    }


    /// <summary> A general command telling the entity to 'use' a nearby entity. </summary>
    public class UseCommand : EntityCommand
    {
        /// <summary> A general command telling the entity to 'use' a nearby entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public UseCommand(Entity actingEntity, Entity targetEntity) : base(actingEntity)
        {
            TargetEntity = targetEntity;
        }
    }


    /// <summary> A command telling the entity to devour, whether by eating or drinking, a nearby entity. </summary>
    public class ConsumeCommand : EntityCommand
    {
        /// <summary> A command telling the entity to devour, whether by eating or drinking, a nearby entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public ConsumeCommand(Entity actingEntity, Entity targetEntity) : base(actingEntity)
        {
            TargetEntity = targetEntity;
        }
    }
}
