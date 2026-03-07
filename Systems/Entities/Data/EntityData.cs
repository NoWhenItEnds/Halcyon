using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Entities.Data.Components;

namespace Halcyon.Entities.Data
{
    /// <summary> The persistent data for an entity. </summary>
    [GlobalClass]
    [Tool]
    public partial class EntityData : Resource, IEquatable<EntityData>
    {
        /// <summary> The entity's personal name. </summary>
        [ExportGroup("General")]
        [Export] public EntityName Name { get; set; } = EntityName.Random(EntityGender.NONE);

        /// <summary> The current gender / sex of the entity. </summary>
        [Export] public EntityGender Gender { get; set; } = EntityGender.NONE;


        /// <summary> The components that define an entity's advanced functionality. </summary>
        [ExportGroup("Components")]
        [Export] public Godot.Collections.Array<DataComponent> Components
        {
            get => new Godot.Collections.Array<DataComponent>(_components);
            private set
            {
                CleanupComponents();
                _components = value.ToHashSet();
                InitialiseComponents();
                EmitChanged();
            }
        }

        /// <summary> The components that define an entity's advanced functionality. </summary>
        private HashSet<DataComponent> _components = new HashSet<DataComponent>();


        /// <summary> The persistent data for an entity. </summary>
        public EntityData() { }


        public Boolean TryAddComponent(DataComponent component)
        {
            CleanupComponents();
            Boolean result = _components.Add(component);
            if(result)
            {
                InitialiseComponents();
                EmitChanged();
            }
            return result;
        }


        /// <summary> Initialises all components, resolving any cross-component dependencies. </summary>
        public void InitialiseComponents()
        {
            if (!Engine.IsEditorHint())
            {
                // Note: Ordering is important, so components requiring other components should be AFTER their dependency.
                foreach (DataComponent component in _components)
                {
                    component.Initialise(this);
                }
            }
        }


        /// <summary> Cleans up all components, unsubscribing from events and releasing references. </summary>
        public void CleanupComponents()
        {
            foreach (DataComponent component in _components.Reverse())
            {
                component.Cleanup();
            }
        }


        /// <summary> Attempt to get a particular component from the data. </summary>
        /// <typeparam name="T"> The type of component to search for. </typeparam>
        /// <param name="component"> The returned component, or a null if one wasn't found. </param>
        /// <returns> Whether there is a component of the desired type. </returns>
        public Boolean TryGetComponent<T>(out T? component) where T : DataComponent
        {
            component = _components.OfType<T>().FirstOrDefault() ?? null;
            return component != null;
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Name);


        /// <inheritdoc/>
        public Boolean Equals(EntityData? other) => other != null ? Name.Equals(other.Name) : false;
    }
}
