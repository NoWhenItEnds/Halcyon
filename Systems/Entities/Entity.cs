#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Entities.Data;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities
{
    /// <summary> An interactable entity within the game world. </summary>
    public partial class Entity : CharacterBody2D, IEquatable<Entity>
    {
        /// <summary> The node that defines the node's collision. </summary>
        [ExportGroup("Nodes")]
        [Export] public CollisionShape2D Collision { get; private set; }

        /// <summary> The sprite representing the entity within the world. </summary>
        [Export] public AnimatedSprite2D Sprite { get; private set; }

        /// <summary> The area around the entity in which it can interact with other entities. </summary>
        [Export] private Area2D _interactionArea;

        /// <summary> A label displaying the entity's name. </summary>
        /// <remarks To help with debugging. </remarks>
        [Export] private Label _nameLabel;


        /// <summary> The persistent data for an entity. </summary>
        [ExportGroup("Settings")]
        [Export] public EntityData Data { get; private set; } = new EntityData();


        /// <summary> A reference to the entity's state machine. </summary>
        public EntityStateMachine StateMachine;


        /// <summary> A set of all the entities that the entity is currently within interactable range of. </summary>
        private HashSet<Entity> _nearbyEntities = new HashSet<Entity>();


        /// <inheritdoc/>
        public override void _Ready()
        {
            StateMachine = new HumanStateMachine(this);
            _interactionArea.BodyEntered += OnInteractionAreaEntered;
            _interactionArea.BodyExited += OnInteractionAreaExited;

            Sprite.Play();
            _nameLabel.Text = Data.Name.ToString();
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            // Update the state machine.
            StateMachine.CurrentState.Update(delta);
        }


        /// <inheritdoc/>
        public override void _ExitTree()
        {
            _interactionArea.BodyEntered -= OnInteractionAreaEntered;
            _interactionArea.BodyExited -= OnInteractionAreaExited;
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


        /// <summary> Get a sorted array of all the nearby entities within range of this entity. </summary>
        /// <returns> A sorted array of all the entities that this entity can currently interact with. </returns>
        public Entity[] GetNearbyEntities() => _nearbyEntities.ToArray();


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Data.Name);


        /// <inheritdoc/>
        public Boolean Equals(Entity? other) => other != null ? Data.Name.Equals(other.Data.Name) : false;
    }
}
