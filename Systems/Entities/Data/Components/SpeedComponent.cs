#nullable disable warnings
using System;
using Godot;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's movement speed. </summary>
    [GlobalClass]
    [Tool]
    public partial class SpeedComponent : DataComponent
    {
        /// <summary> How fast the character moves as a result of their physical attributes. </summary>
        [Export] public Property Value { get; private set; } = new Property("speed_value", 0);


        /// <summary> A reference to the stats component. </summary>
        private StatComponent? _statComponent = null;


        /// <summary> The persistent data for an entity's movement speed. </summary>
        public SpeedComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                _statComponent = stats;
                _statComponent.Strength.ValueChanged += OnStatsChange;
                _statComponent.Dexterity.ValueChanged += OnStatsChange;
                OnStatsChange(0);   // As we don't care about the value here, it's pulled from the stats instead, we just use 0.
            }
            else
            {
                throw new ArgumentException($"'{GetType()}' requires '{typeof(StatComponent)}' to be present.");
            }
        }


        /// <inheritdoc/>
        public override void Cleanup()
        {
            if (_statComponent != null)
            {
                _statComponent.Strength.ValueChanged -= OnStatsChange;
                _statComponent.Dexterity.ValueChanged -= OnStatsChange;
                _statComponent = null;
            }
        }


        /// <summary> Update the speed value if stats change. </summary>
        /// <param name="newValue"> The new stat value. </param>
        private void OnStatsChange(Single newValue)
        {
            Value.BaseValue = _statComponent.Strength.CurrentValue + _statComponent.Dexterity.CurrentValue + 5;
        }
    }
}
