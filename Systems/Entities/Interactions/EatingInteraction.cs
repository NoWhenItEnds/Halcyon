using System;
using Godot;
using Halcyon.Entities.Data.Components;

namespace Halcyon.Entities.Interactions
{
    /// <summary> An interaction where the actor consumes the target entity. </summary>
    public class EatingInteraction : Interaction
    {
        /// <summary> How long consuming the food takes, in seconds, to complete. </summary>
        // TODO - This should be based upon the thing being consumed.
        private const Single CONSUMPTION_TIME = 3f;

        /// <summary> How long remaining before the interaction is complete. </summary>
        private Single _timeRemaining = 0f;

        /// <summary> The actor's hunger component, resolved during CanBegin. </summary>
        private HungerComponent? _hunger = null;

        /// <summary> The target's edible component, resolved during CanBegin. </summary>
        private EdibleComponent? _edible = null;


        /// <summary> An interaction where the actor consumes the target entity. </summary>
        /// <param name="actor"> The entity doing the eating. </param>
        /// <param name="target"> The entity being eaten. </param>
        public EatingInteraction(Entity actor, Entity target) : base(actor, target) { }


        /// <inheritdoc/>
        public override Boolean CanBegin()
        {
            Boolean actorCanEat = ActingEntity.Data != null && ActingEntity.Data.TryGetComponent<HungerComponent>(out _hunger);
            Boolean targetIsEdible = TargetEntity.Data != null && TargetEntity.Data.TryGetComponent<EdibleComponent>(out _edible);
            return actorCanEat && targetIsEdible;
        }


        /// <inheritdoc/>
        public override void Begin()
        {
            _timeRemaining = CONSUMPTION_TIME;
            GD.Print($"{ActingEntity.Data.Name} starts eating {TargetEntity.Data.Name}!");
        }


        /// <inheritdoc/>
        public override void Update(Double delta)
        {
            _timeRemaining -= (Single)delta;

            if (_timeRemaining <= 0f)
            {
                if (_hunger != null && _edible != null)
                {
                    _hunger.Value.BaseValue += _edible.NutritionValue.CurrentValue;
                }
                IsComplete = true;
            }
        }


        /// <inheritdoc/>
        public override void End()
        {
            GD.Print($"{ActingEntity.Data.Name} finished eating {TargetEntity.Data.Name}!");
        }
    }
}
