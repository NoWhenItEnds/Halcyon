#nullable disable warnings
using System;
using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's physical stamina. </summary>
    [GlobalClass]
    [Tool]
    public partial class StaminaComponent : DataComponent
    {
        /// <summary> The entity's physical stamina. Drained by performing physically demanding activities. </summary>
        public DerivedStat Stamina { get; private set; }


        /// <summary> The persistent data for an entity's physical stamina. </summary>
        public StaminaComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                Stamina = new DerivedStat(() => 0, () => stats.Vigor.CurrentValue + 3);
            }
            else
            {
                throw new ArgumentException($"'{GetType()}' requires '{typeof(StatComponent)}' to be present.");
            }
        }
    }
}
