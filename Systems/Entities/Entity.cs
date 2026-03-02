#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Animations;
using Halcyon.Entities.Data;

namespace Halcyon.Entities
{
    /// <summary> An interactable entity within the game world. </summary>
    public partial class Entity : CharacterBody2D, IEquatable<Entity>
    {
        /// <summary> The node that defines the node's collision. </summary>
        [ExportGroup("Nodes")]
        [Export] public CollisionShape2D Collision { get; private set; }

        /// <summary> The sprite representing the entity within the world. </summary>
        [Export] public LayeredSprite2D LayeredSprite { get; private set; }

        /// <summary> The area around the entity in which it can interact with other entities. </summary>
        [Export] private Area2D _interactionArea;

        /// <summary> A label displaying the entity's name. </summary>
        /// <remarks To help with debugging. </remarks>
        [Export] private Label _nameLabel;


        /// <summary> The data representing the state of the entity. </summary>
        [ExportGroup("Settings")]
        [Export] public EntityData? Data {
            get => _data;
            set // TODO - Update EVERYTHING on set!
            {
                _data = value;
            }
        }

        /// <summary> The data representing the state of the entity. </summary>
        private EntityData? _data = null;


        /// <summary> A set of all the entities that the entity is currently within interactable range of. </summary>
        private HashSet<Entity> _nearbyEntities = new HashSet<Entity>();


        /// <summary> Get a sorted array of all the nearby entities within range of this entity. </summary>
        /// <returns> A sorted array of all the entities that this entity can currently interact with. </returns>
        public Entity[] GetNearbyEntities() => _nearbyEntities.ToArray();


        public Boolean TryGetData<T>(out T? data) where T : EntityData
        {
            data = null;
            if(Data != null && Data is T d)
            {
                data = d;
            }
            return data != null;
        }


        /// <summary> When something enters the entity's area of influence, add it to the nearby entities. </summary>
        /// <param name="body"> The node entering the area. </param>
        private void OnInteractionAreaEntered(Node2D body)
        {
            if(body is Entity entity)
            {
                _nearbyEntities.Add(entity);
            }
        }


        /// <summary> Remove the leaving node from the nearby entities. </summary>
        /// <param name="body"> A reference to the node leaving the entity's area of influence. </param>
        private void OnInteractionAreaExited(Node2D body)
        {
            if (body is Entity entity)
            {
                _nearbyEntities.Remove(entity);
            }
        }


        /// <inheritdoc/>
        public override void _Ready()
        {
            _interactionArea.BodyEntered += OnInteractionAreaEntered;
            _interactionArea.BodyExited += OnInteractionAreaExited;

            // Initialise the data if this node wasn't spawned in (was hand-placed in the editor).
            // TODO - Only run this if we're not in the editor.
            if (Data != null)
            {
                Data.Initialise(this);
                _nameLabel.Text = Data.Name.ToString();
            }
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            // Update the state machine.
            Data.StateMachine.CurrentState.Update(delta);
        }


        /// <inheritdoc/>
        public override void _ExitTree()
        {
            _interactionArea.BodyEntered -= OnInteractionAreaEntered;
            _interactionArea.BodyExited -= OnInteractionAreaExited;
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Data);


        /// <inheritdoc/>
        public Boolean Equals(Entity? other) => other != null ? Data.Equals(other.Data) : false;
    }
}
