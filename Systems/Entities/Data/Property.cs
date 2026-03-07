using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Halcyon.Entities.Data
{
    /// <summary> A data property whose value can be modified. </summary>
    [GlobalClass]
    [Tool]
    public partial class Property : Resource, IEquatable<Property>
    {
        /// <summary> The data's unique name or identifier. </summary>
        [Export] public String Name { get; protected set; } = String.Empty;

        /// <summary> The property's base value before modifiers are applied. </summary>
        [Export]
        public Single BaseValue
        {
            get
            {
                return _baseValue;
            }
            set
            {
                _baseValue = value;
                RecalculateCurrentValue();
            }
        }

        /// <summary> The property's base value before modifiers are applied. </summary>
        protected Single _baseValue;


        /// <summary> The current value of the property after modifiers have been applied. </summary>
        public Single CurrentValue { get; protected set; }

        /// <summary> Emitted when the value is changed. Contains the new current value. </summary>
        public event Action<Single> ValueChanged = delegate { };

        /// <summary> The modifiers currently being applied to the property. </summary>
        protected HashSet<PropertyModifier> _modifiers = new HashSet<PropertyModifier>();


        /// <summary> A data property whose value can be modified. </summary>
        public Property() { }


        /// <summary> A data property whose value can be modified. </summary>
        /// <param name="name"> The data's unique name or identifier. </param>
        /// <param name="value"> The property's base value before modifiers are applied. </param>
        public Property(String name, Single value)
        {
            Name = name;
            _baseValue = value; // Apply directly to base to avoid updating immediately on create.
        }


        /// <summary> Adds a modifier to the property. </summary>
        /// <param name="modifier"> The modifier object to add. </param>
        public void AddModifier(PropertyModifier modifier)
        {
            if (_modifiers.Add(modifier))
            {
                RecalculateCurrentValue();
            }
        }


        /// <summary> Adds an array of modifiers to the property. </summary>
        /// <param name="modifiers"> The modifier array to add. </param>
        public void AddModifier(PropertyModifier[] modifiers)
        {
            Int32 added = 0;
            foreach (PropertyModifier modifier in modifiers)
            {
                added += _modifiers.Add(modifier) ? 1 : 0;
            }

            if (added > 0)
            {
                RecalculateCurrentValue();
            }
        }


        /// <summary> Removes a modifier from the property. </summary>
        /// <param name="modifier"> The modifier object to remove. </param>
        public void RemoveModifier(PropertyModifier modifier)
        {
            if (_modifiers.Remove(modifier))
            {
                RecalculateCurrentValue();
            }
        }


        /// <summary> Removes an array of modifiers from the property. </summary>
        /// <param name="modifiers"> The modifier array to remove. </param>
        public void RemoveModifier(PropertyModifier[] modifiers)
        {
            Int32 removed = 0;
            foreach (PropertyModifier modifier in modifiers)
            {
                removed += _modifiers.Remove(modifier) ? 1 : 0;
            }

            if (removed > 0)
            {
                RecalculateCurrentValue();
            }
        }


        /// <summary> Remove all the modifiers added from a particular source. </summary>
        /// <param name="source"> A reference to the source object. </param>
        public void RemoveModifierSource(Object source)
        {
            PropertyModifier[] filtered = _modifiers.Where(x => x.Source == source).ToArray();
            if (filtered.Length > 0)
            {
                RemoveModifier(filtered);
            }
        }


        /// <summary> Remove all the active modifiers. </summary>
        public void ClearModifiers()
        {
            if (_modifiers.Count > 0)
            {
                _modifiers.Clear();
                RecalculateCurrentValue();
            }
        }


        /// <summary> Calculate the property's current value. </summary>
        protected virtual void RecalculateCurrentValue()
        {
            Single oldCurrent = CurrentValue;
            CurrentValue = BaseValue;

            foreach (PropertyModifier modifier in _modifiers.OrderBy(x => x.Priority))
            {
                CurrentValue = modifier.Apply(CurrentValue);
            }

            if (oldCurrent != CurrentValue)
            {
                OnValueChanged(CurrentValue);
            }
        }


        /// <summary> Invoke an event indicating that the property's current value has changed. </summary>
        /// <param name="value"> The new value. </param>
        protected void OnValueChanged(Single value)
        {
            ValueChanged?.Invoke(value);
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Name);


        /// <inheritdoc/>
        public Boolean Equals(Property? other) => other != null ? Name == other.Name : false;
    }
}
