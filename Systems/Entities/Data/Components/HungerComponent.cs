using System;
using Godot;
using Halcyon.Managers;
using Halcyon.Utilities;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
    [GlobalClass]
    [Tool]
    public partial class HungerComponent : DataComponent
    {
        /// <summary> How 'satisfied', whatever that means for a given entity, the entity is. </summary>
        [Export] public ClampedProperty Value { get; private set; } = new ClampedProperty("hunger_value", 0, 100);

        /// <summary> How skilled the entity is at resisting hunger 'damage'. </summary>
        /// <remarks> This is based upon the entity's vigor attribute, and shouldn't apply to regaining hunger. </remarks>
        [Export] public Property Defence { get; private set; } = new Property("hunger_defence", 0);


        /// <summary> A reference to the entity's vigor property. </summary>
        private Property? _vigorProperty = null;


        /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
        public HungerComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TimeChanged += OnTimeChanged;
            }

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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TimeChanged -= OnTimeChanged;
            }

            if (_vigorProperty != null)
            {
                _vigorProperty.ValueChanged -= OnVigorChange;
                _vigorProperty = null;
            }
        }


        /// <summary> Update the current hunger value when the game time advances. </summary>
        /// <param name="currentTime"> The current game time. </param>
        private void OnTimeChanged(DateTime currentTime)
        {
            Int32 successes = GameManager.Instance.DiceRandom.StandardTest((Int32)Defence.CurrentValue);
            switch (DiceRandom.EvaluateResult(successes))
            {
                case DiceResultType.SUCCESS:
                    Value.BaseValue -= 1f;
                    break;
                case DiceResultType.FAILURE:
                    Value.BaseValue -= 2f;
                    break;
            }
        }


        /// <summary> Update the hunger defence value if vigor changes. </summary>
        /// <param name="newValue"> The new vigor value. </param>
        private void OnVigorChange(Single newValue)
        {
            Defence.BaseValue = newValue + 5;
        }
    }
}
