using Godot;

namespace Halcyon.Entities.ActorCommands
{
    /// <summary> A data object containing a command for an actor. </summary>
    public abstract class ActorCommand
    {
        /// <summary> The direction associated with the command. </summary>
        public Vector2 Direction { get; init; } = Vector2.Zero;


        /// <summary> A data object containing a command for an actor. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public ActorCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }


    /// <summary> A command telling the actor to stand around and look pretty. </summary>
    public class IdleCommand : ActorCommand
    {
        /// <summary> A command telling the actor to stand around and look pretty. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Vector2 direction) : base(direction) { }
    }


    /// <summary> A command telling the actor to walk. </summary>
    public class WalkCommand : ActorCommand
    {
        /// <summary> A command telling the actor to walk. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public WalkCommand(Vector2 direction) : base(direction) { }
    }


    /// <summary> A command telling the actor to sprint. </summary>
    public class SprintCommand : ActorCommand
    {
        /// <summary> A command telling the actor to sprint. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public SprintCommand(Vector2 direction) : base(direction) { }
    }
}
