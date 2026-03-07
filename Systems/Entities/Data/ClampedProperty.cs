using Godot;
using System;
using System.Linq;

namespace Halcyon.Entities.Data
{
    /// <summary> A property whose values are clamped between a maximum and minimum. </summary>
    [GlobalClass]
    [Tool]
    public partial class ClampedProperty : Property
    {
        /// <summary> The property's minimum value. The actual value will be clamped to this. </summary>
        [Export] public Single MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {

                _minValue = value;
                if (BaseValue < _minValue)
                {
                    BaseValue = _minValue;
                }
            }
        }

        /// <summary> The property's minimum value. The actual value will be clamped to this. </summary>
        protected Single _minValue;


        /// <summary> The property's maximum value. The actual value will be clamped to this. </summary>
        [Export] public Single MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
                if (BaseValue > _maxValue)
                {
                    BaseValue = _maxValue;
                }
            }
        }

        /// <summary> The property's maximum value. The actual value will be clamped to this. </summary>
        protected Single _maxValue;


        /// <summary> An amount, from 0.0 - 1.0 the property is between its minimum and maximum values. </summary>
        public Single Percent => MaxValue != MinValue ? (Single)(CurrentValue - MinValue) / (MaxValue - MinValue) : 0f;


        /// <summary> A property whose values are clamped between a maximum and minimum. </summary>
        public ClampedProperty() { }


        /// <summary> A property whose values are clamped between a maximum and minimum. </summary>
        /// <param name="name"> The data's unique name or identifier. </param>
        /// <param name="minValue"> The function used to calculate the current minimum possible value. </param>
        /// <param name="maxValue"> The function used to calculate the current maximum possible value. </param>
        public ClampedProperty(String name, Single minValue, Single maxValue) : this(name, minValue, maxValue, maxValue) { }


        /// <summary> A property whose values are clamped between a maximum and minimum. </summary>
        /// <param name="name"> The data's unique name or identifier. </param>
        /// <param name="minValue"> The function used to calculate the current minimum possible value. </param>
        /// <param name="maxValue"> The function used to calculate the current maximum possible value. </param>
        /// <param name="initialValue"> The initial value to set the stat to. </param>
        public ClampedProperty(String name, Single minValue, Single maxValue, Single initialValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            CurrentValue = initialValue;
        }


        /// <inheritdoc/>
        protected override void RecalculateCurrentValue()
        {
            Single oldCurrent = CurrentValue;
            CurrentValue = Math.Clamp(BaseValue, MinValue, MaxValue);

            foreach (PropertyModifier modifier in _modifiers.OrderBy(x => x.Priority))
            {
                CurrentValue = modifier.Apply(CurrentValue);
            }

            if (oldCurrent != CurrentValue)
            {
                OnValueChanged(CurrentValue);
            }
        }
    }
}
