#nullable disable warnings
using Godot;

namespace Halcyon.Entities.ActorCommands
{
    /// <summary> A data object containing a command for an actor. </summary>
    public abstract class ActorCommand
    {
        /// <summary> The direction associated with the command. </summary>
        public Vector2 Direction { get; init; } = Vector2.Zero;

        /// <summary> The global position targeted by the command. </summary>
        public Vector2 TargetPosition { get; init; } = Vector2.Zero;

        /// <summary> The entity targeted by the command. </summary>
        public IEntity TargetEntity { get; init; }
    }


    /// <summary> A command telling the actor to stand around and look pretty. </summary>
    public class IdleCommand : ActorCommand
    {
        /// <summary> A command telling the actor to stand around and look pretty. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the actor to walk. </summary>
    public class WalkCommand : ActorCommand
    {
        /// <summary> A command telling the actor to walk. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public WalkCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the actor to sprint. </summary>
    public class SprintCommand : ActorCommand
    {
        /// <summary> A command telling the actor to sprint. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public SprintCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the actor to interact with a nearby entity. </summary>
    public class InteractCommand : ActorCommand
    {
        /// <summary> A command telling the actor to interact with a nearby entity. </summary>
        /// <param name="targetEntity"> The entity targeted by the command. </param>
        public InteractCommand(IEntity targetEntity)
        {
            TargetEntity = targetEntity;
        }
    }
}
