using Godot;

namespace Halcyon.Entities.ActorCommands
{
    /// <summary> A command telling the actor to stand around and look pretty. </summary>
    public class IdleCommand : ActorCommand
    {
        /// <summary> A command telling the actor to stand around and look pretty. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public IdleCommand(Vector2 direction) : base(direction) { }
    }
}
