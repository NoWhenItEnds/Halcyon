#nullable disable warnings
using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
    [GlobalClass]
    [Tool]
    public partial class EntertainmentComponent : DataComponent
    {
        /// <summary> How entertained / satisfied the entity is. </summary>
        public DerivedStat EntertainmentStat { get; private set; }


        /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
        public EntertainmentComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            EntertainmentStat = new DerivedStat(() => 0, () => 10);
        }
    }
}
