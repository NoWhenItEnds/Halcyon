using System;
using Godot;
using Halcyon.Managers;
using Warlord.Utilities;

namespace Halcyon.Entities.Data.Components
{
    /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
    [GlobalClass]
    [Tool]
    public partial class HungerComponent : DataComponent
    {
        /// <summary> How 'satisfied', whatever that means for a given entity, the entity is. </summary>
        [Export] public ClampedProperty Value { get; private set; } = new ClampedProperty("hunger_value", 100, 0, 100);

        /// <summary> How skilled the entity is at resisting hunger 'damage'. </summary>
        /// <remarks> This is based upon the entity's vigor attribute, and shouldn't apply to regaining hunger. </remarks>
        [Export] public Property Defence { get; private set; } = new Property("hunger_defence", 0);


        /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
        public HungerComponent() : base()
        {
            if(!Engine.IsEditorHint())
            {
                GameManager.Instance.TimeChanged += OnTimeChanged;  // TODO - How unsubscribe.
            }
        }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                stats.Vigor.ValueChanged += OnVigorChange;
                OnVigorChange(stats.Vigor.CurrentValue);    // TODO - How to unsub.
            }
            else
            {
                throw new ArgumentException($"'{GetType()}' requires '{typeof(StatComponent)}' to be present.");
            }
        }


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


        private void OnVigorChange(Single newValue)
        {
            Defence.BaseValue = newValue + 5;
        }
    }
}
