#nullable disable warnings
using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's movement speed. </summary>
    [GlobalClass]
    [Tool]
    public partial class SpeedComponent : DataComponent
    {
        /// <summary> How fast the character moves as a result of their physical attributes. </summary>
        public DerivedStat SpeedStat { get; private set; }


        /// <summary> The persistent data for an entity's movement speed. </summary>
        public SpeedComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                SpeedStat = new DerivedStat(() => 0, () => 5 + stats.Strength.CurrentValue + stats.Dexterity.CurrentValue);
            }
        }
    }
}
