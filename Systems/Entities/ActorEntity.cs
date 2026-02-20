#nullable disable warnings
using System;
using Godot;
using Halcyon.Entities.ActorStates;
using Halcyon.Entities.Data;
using Halcyon.Utilities;

namespace Halcyon.Entities
{
    /// <summary> An moving, sentient entity within the game world. </summary>
    public partial class ActorEntity : CharacterBody2D, IEntity
    {
        /// <summary> The node that defines the node's collision. </summary>
        [ExportGroup("Nodes")]
        [Export] private CollisionShape2D _collisionShape;

        /// <summary> The sprite representing the actor within the world. </summary>
        [Export] private AnimatedSprite2D _sprite;


        /// <summary> The persistent data for an actor entity. </summary>
        [ExportGroup("Settings")]
        [Export] public ActorData Data { get; private set; } = new ActorData();


        /// <summary> A reference to the actor's state machine. </summary>
        public ActorStateMachine StateMachine;


        /// <inheritdoc/>
        public override void _Ready()
        {
            StateMachine = new ActorStateMachine(this);
            _sprite.Play();
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            //Velocity *= (Single)delta;
            Boolean isCollision = MoveAndSlide();
            if (isCollision)
            {
                KinematicCollision2D collision = GetLastSlideCollision();
            }
        }


        /// <inheritdoc/>
        public void SetAnimation(String name, Direction direction) => _sprite.Animation = $"{name}_{direction.ToString().ToLower()}";


        /// <inheritdoc/>
        public Vector2 GetLocation() => GlobalPosition;
    }
}
