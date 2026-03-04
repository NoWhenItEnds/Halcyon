using Godot;

namespace Halcyon.Entities.EntityCommands
{
    /// <summary> A data object containing a command for an entity. </summary>
    public abstract class EntityCommand
    {
        /// <summary> The entity that is performing the command. </summary>
        public readonly Entity ActingEntity;

        /// <summary> The direction associated with the command. </summary>
        public readonly Vector2 Direction = Vector2.Zero;

        /// <summary> The global position targeted by the command. </summary>
        public readonly Vector2 TargetPosition = Vector2.Zero;  // TODO - Implement.

        /// <summary> The entity targeted by the command. </summary>
        public readonly Entity? TargetEntity = null;


        /// <summary> A data object containing a command for an entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        protected EntityCommand(Entity actingEntity)
        {
            ActingEntity = actingEntity;    // TODO - Just use direction instead? See how AI will work.
            Direction = actingEntity.Velocity.Normalized(); // By default, the direction will be the current direction of the acting entity.
        }


        /// <summary> A data object containing a command for an entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="direction"> The direction associated with the command. </param>
        protected EntityCommand(Entity actingEntity, Vector2 direction) : this(actingEntity)
        {
            Direction = direction;
        }


        /// <summary> A data object containing a command for an entity. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        protected EntityCommand(Entity actingEntity, Entity targetEntity) : this(actingEntity)
        {
            TargetEntity = targetEntity;
        }
    }


    /// <summary> A command telling the entity to stand around and look pretty. </summary>
    public class IdleCommand : EntityCommand
    {
        /// <summary> A command telling the entity to stand around and look pretty. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Entity actingEntity, Vector2 direction) : base(actingEntity, direction) { }

        /// <summary> A command telling the entity to stand around and look pretty. </summary>
        /// <param name="actingEntity"> The entity that is performing the command. </param>
        public IdleCommand(Entity actingEntity) : base(actingEntity) { }
    }


    /// <summary> A command telling the entity to walk. </summary>
    /// <param name="actingEntity"> The entity that is performing the command. </param>
    /// <param name="direction"> The direction vector associated with the command. </param>
    public class WalkCommand(Entity actingEntity, Vector2 direction) : EntityCommand(actingEntity, direction) { }


    /// <summary> A command telling the entity to sprint. </summary>
    /// <param name="actingEntity"> The entity that is performing the command. </param>
    /// <param name="direction"> The direction vector associated with the command. </param>
    public class SprintCommand(Entity actingEntity, Vector2 direction) : EntityCommand(actingEntity, direction) { }


    /// <summary> A command telling the entity to inspect a nearby entity. </summary>
    /// <param name="actingEntity"> The entity that is performing the command. </param>
    /// <param name="targetEntity"> The entity targeted by the command. </param>
    public class ExamineCommand(Entity actingEntity, Entity targetEntity) : EntityCommand(actingEntity, targetEntity) { }


    /// <summary> A general command telling the entity to 'use' a nearby entity. </summary>
    /// <param name="actingEntity"> The entity that is performing the command. </param>
    /// <param name="targetEntity"> The entity targeted by the command. </param>
    public class UseCommand(Entity actingEntity, Entity targetEntity) : EntityCommand(actingEntity, targetEntity) { }


    /// <summary> A command telling the entity to devour, whether by eating or drinking, a nearby entity. </summary>
    /// <param name="actingEntity"> The entity that is performing the command. </param>
    /// <param name="targetEntity"> The entity targeted by the command. </param>
    public class ConsumeCommand(Entity actingEntity, Entity targetEntity) : EntityCommand(actingEntity, targetEntity) { }
}
