using Godot;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> The main game world's manager singleton. The program's entrypoint. </summary>
    public partial class GameManager : SingletonNode<GameManager>
    {
        /// <inheritdoc/>
        public override void _Ready()
        {
            GD.Print("Hello, World!");
        }
    }
}
