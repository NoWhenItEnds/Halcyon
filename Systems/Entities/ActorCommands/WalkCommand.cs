using Godot;

namespace Halcyon.Entities.ActorCommands
{
    /// <summary> A command telling the actor to walk. </summary>
    public class WalkCommand : ActorCommand
    {
        /// <summary> A command telling the actor to walk. </summary>
        /// <param name="direction"> The direction vector associated with the command. </param>
        public WalkCommand(Vector2 direction) : base(direction) { }
    }
}
