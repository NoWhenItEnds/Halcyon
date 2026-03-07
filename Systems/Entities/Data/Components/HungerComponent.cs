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
        public Stat Hunger { get; private set; } = new Stat("hunger", 100, 0, 100);


        /// <summary> The persistent data for how hungry, whatever that means for an entity, an entity is. </summary>
        public HungerComponent() : base()
        {
            if(!Engine.IsEditorHint())
            {
                GameManager.Instance.TimeChanged += OnTimeChanged;  // TODO - How unsubscribe.
            }
        }


        private void OnTimeChanged(DateTime currentTime)
        {
            Int32 successes = GameManager.Instance.DiceRandom.StandardTest(10); // ToDO - Derived stat should go here.
            if (successes > 4)
            {
                // + 0
            }
            else if (successes > 0 && successes <= 4)
            {
                // + 1
            }
            else
            {
                // + 2
            }
        }


        /// <inheritdoc/>
        public override void Initialise(EntityData data)
        {
            if (data.TryGetComponent<StatComponent>(out StatComponent? stats) && stats != null)
            {
                // TODO - Better derived stat that doesn't have max / min, only current.
                Speed = new DerivedStat(() => 0, () => 5 + stats.Strength.CurrentValue + stats.Dexterity.CurrentValue);
            }
            else
            {
                throw new ArgumentException($"'{GetType()}' requires '{typeof(StatComponent)}' to be present.");
            }
        }
    }
}
