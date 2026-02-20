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
}
