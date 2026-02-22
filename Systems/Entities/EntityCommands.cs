#nullable disable warnings
using Godot;

namespace Halcyon.Entities.EntityCommands
{
    /// <summary> A data object containing a command for an entity. </summary>
    public abstract class EntityCommand
    {
        /// <summary> The direction associated with the command. </summary>
        public Vector2 Direction { get; init; } = Vector2.Zero;

        /// <summary> The global position targeted by the command. </summary>
        public Vector2 TargetPosition { get; init; } = Vector2.Zero;

        /// <summary> The entity targeted by the command. </summary>
        public Entity TargetEntity { get; init; }
    }


    /// <summary> A command telling the entity to stand around and look pretty. </summary>
    public class IdleCommand : EntityCommand
    {
        /// <summary> A command telling the entity to stand around and look pretty. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the entity to walk. </summary>
    public class WalkCommand : EntityCommand
    {
        /// <summary> A command telling the entity to walk. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public WalkCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the entity to sprint. </summary>
    public class SprintCommand : EntityCommand
    {
        /// <summary> A command telling the entity to sprint. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public SprintCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the entity to inspect a nearby entity. </summary>
    public class ExamineCommand : EntityCommand
    {
        /// <summary> A command telling the entity to inspect a nearby entity. </summary>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public ExamineCommand(Entity targetEntity)
        {
            TargetEntity = targetEntity;
        }
    }


    /// <summary> A general command telling the entity to 'use' a nearby entity. </summary>
    public class UseCommand : EntityCommand
    {
        /// <summary> A general command telling the entity to 'use' a nearby entity. </summary>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public UseCommand(Entity targetEntity)
        {
            TargetEntity = targetEntity;
        }
    }


    /// <summary> A command telling the entity to devour, whether by eating or drinking, a nearby entity. </summary>
    public class ConsumeCommand : EntityCommand
    {
        /// <summary> A command telling the entity to devour, whether by eating or drinking, a nearby entity. </summary>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public ConsumeCommand(Entity targetEntity)
        {
            TargetEntity = targetEntity;
        }
    }
}
