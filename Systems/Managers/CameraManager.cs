#nullable disable warnings
using Godot;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> The singleton for the game world camera. </summary>
    public partial class CameraManager : SingletonNode2D<CameraManager>
    {
        /// <summary> The main camera node. </summary>
        [ExportGroup("Nodes")]
        [Export] private Camera2D _mainCamera;
    }
}
