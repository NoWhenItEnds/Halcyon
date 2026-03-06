using System.Collections.Generic;
using System.Linq;
using Halcyon.Animations;
using Halcyon.Utilities.Extensions;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> A library of all the resources loaded into the game. </summary>
    public partial class ResourceManager : SingletonNode<ResourceManager>
    {
        /// <summary> A library of all the available layered animations for entities. </summary>
        public HashSet<LayeredAnimation> LayeredAnimations { get; private set; } = new HashSet<LayeredAnimation>();


        /// <inheritdoc/>
        public override void _Ready()
        {
            // Load all the resources.
            LayeredAnimations = ResourceExtensions.GetResources<LayeredAnimation>("res://Content/Resources/LayeredAnimations").ToHashSet(); // TODO - TryAdd each individually to check / throw on not unique?
            // TODO - Add loading from user dir?
        }
    }
}
