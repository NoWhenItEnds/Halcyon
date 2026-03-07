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
        [Export] public ClampedProperty Value { get; private set; } = new ClampedProperty("stamina_value", 0, 100);


        /// <summary> A reference to the entity's vigor property. </summary>
        private Property? _vigorProperty = null;


        /// <summary> The persistent data for an entity's physical stamina. </summary>
        public StaminaComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                _vigorProperty = stats.Vigor;
                _vigorProperty.ValueChanged += OnVigorChange;
                OnVigorChange(_vigorProperty.CurrentValue);
            }
            else
            {
                throw new ArgumentException($"'{GetType()}' requires '{typeof(StatComponent)}' to be present.");
            }
        }


        /// <inheritdoc/>
        public override void Cleanup()
        {
            if (_vigorProperty != null)
            {
                _vigorProperty.ValueChanged -= OnVigorChange;
                _vigorProperty = null;
            }
        }


        /// <summary> Update the maximum stamina value if vigor changes. </summary>
        /// <param name="newValue"> The new vigor value. </param>
        private void OnVigorChange(Single newValue)
        {
            Value.MaxValue = newValue + 3;
        }
    }
}
