using System;
using Godot;
using Halcyon.Managers;
using Halcyon.Utilities;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
    [GlobalClass]
    [Tool]
    public partial class EntertainmentComponent : DataComponent
    {
        /// <summary> How entertained / satisfied the entity is. </summary>
        [Export] public ClampedProperty Value { get; private set; } = new ClampedProperty("entertainment_value", 0, 100);

        /// <summary> How skilled the entity is at resisting entertainment 'damage'. </summary>
        /// <remarks> This is based upon the entity's intellect attribute, and shouldn't apply to regaining entertainment. </remarks>
        [Export] public Property Defence { get; private set; } = new Property("entertainment_defence", 0);


        /// <summary> A reference to the entity's intellect property. </summary>
        private ClampedProperty? _intellectProperty = null;


        /// <summary> The persistent data for an entity's entertainment / satisfaction. </summary>
        public EntertainmentComponent() : base() { }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TimeChanged += OnTimeChanged;
            }

            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                _intellectProperty = stats.Intellect;
                _intellectProperty.ValueChanged += OnIntellectChange;
                OnIntellectChange(_intellectProperty.CurrentValue);
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

            if (_intellectProperty != null)
            {
                _intellectProperty.ValueChanged -= OnIntellectChange;
                _intellectProperty = null;
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


        /// <summary> Update the entertainment defence value if intellect changes. </summary>
        /// <param name="newValue"> The new intellect value. </param>
        private void OnIntellectChange(Single newValue)
        {
            if(_intellectProperty != null)
            {
                // Defence is inverse to intellect. The more intelligent you are, the more stimulation you need.
                Defence.BaseValue = Math.Clamp(_intellectProperty.MaxValue - newValue,
                    _intellectProperty.MinValue, _intellectProperty.MaxValue);
            }
        }
    }
}
