using Halcyon.Animations;
using Halcyon.Utilities.Extensions;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> A library of all the resources loaded into the game. </summary>
    public partial class ResourceManager : SingletonNode<ResourceManager>
    {
        /// <summary> A library of all the available layered animations for entities. </summary>
        public LayeredAnimation[] LayeredAnimations { get; private set; } = [];


        /// <inheritdoc/>
        public override void _Ready()
        {
            // Load all the resources.
            LayeredAnimations = ResourceExtensions.GetResources<LayeredAnimation>("res://Content/Resources/LayeredAnimations");
            // TODO - Add loading from user dir?
        }
    }
}
