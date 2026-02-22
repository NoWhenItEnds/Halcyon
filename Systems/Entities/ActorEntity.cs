#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Entities.ActorStates;
using Halcyon.Entities.Data;
using Halcyon.Utilities;

namespace Halcyon.Entities
{
    /// <summary> An moving, sentient entity within the game world. </summary>
    public partial class ActorEntity : CharacterBody2D, IEntity, IEquatable<ActorEntity>
    {
        /// <summary> The node that defines the node's collision. </summary>
        [ExportGroup("Nodes")]
        [Export] private CollisionShape2D _collisionShape;

        /// <summary> The sprite representing the actor within the world. </summary>
        [Export] private AnimatedSprite2D _sprite;

        /// <summary> The area around the entity in which it can interact with other entities. </summary>
        [Export] private Area2D _interactionArea;

        /// <summary> A label displaying the actor's name. </summary>
        /// <remarks To help with debugging. </remarks>
        [Export] private Label _nameLabel;


        /// <summary> The persistent data for an actor entity. </summary>
        [ExportGroup("Settings")]
        [Export] public ActorData Data { get; private set; } = new ActorData();


        /// <summary> A reference to the actor's state machine. </summary>
        public ActorStateMachine StateMachine;


        /// <summary> A set of all the entities that the actor is currently within interactable range of. </summary>
        private HashSet<IEntity> _nearbyEntities = new HashSet<IEntity>();


        /// <inheritdoc/>
        public override void _Ready()
        {
            StateMachine = new ActorStateMachine(this);
            _interactionArea.BodyEntered += OnInteractionAreaEntered;
            _interactionArea.BodyExited += OnInteractionAreaExited;

            _sprite.Play();
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


        /// <summary> When something enters the actor's area of influence, add it to the nearby entities. </summary>
        /// <param name="body"> The node entering the area. </param>
        private void OnInteractionAreaEntered(Node2D body)
        {
            if(body is IEntity entity)
            {
                _nearbyEntities.Add(entity);
            }
        }


        /// <summary> Remove the leaving node from the nearby entities. </summary>
        /// <param name="body"> A reference to the node leaving the actor's area of influence. </param>
        private void OnInteractionAreaExited(Node2D body)
        {
            if (body is IEntity entity)
            {
                _nearbyEntities.Remove(entity);
            }
        }


        /// <summary> Get a sorted array of all the nearby entities within range of this actor. </summary>
        /// <returns> A sorted array of all the entities that this actor can currently interact with. </returns>
        public IEntity[] GetNearbyEntities() => _nearbyEntities.ToArray();


        /// <inheritdoc/>
        public void SetAnimation(String name, Direction direction) => _sprite.Animation = $"{name}_{direction.ToString().ToLower()}";


        /// <inheritdoc/>
        public Vector2 GetLocation() => GlobalPosition;


        /// <inheritdoc/>
        public Boolean TryInteractWith(IEntity interactingEntity)
        {
            GD.Print($"{GetHashCode()} interacted with {interactingEntity.GetHashCode()}!");
            return true;
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Data.Name);


        /// <inheritdoc/>
        public Boolean Equals(ActorEntity? other) => other != null ? Equals(other) : false;
    }
}
