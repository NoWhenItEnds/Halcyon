#nullable disable warnings
using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Halcyon.Animations;
using Halcyon.Entities.Data;
using Halcyon.Entities.EntityCommands;
using Halcyon.Entities.States.Machines;

namespace Halcyon.Entities
{
    /// <summary> An interactable entity within the game world. </summary>
    [Tool]
    public partial class Entity : CharacterBody2D, IEquatable<Entity>
    {
        /// <summary> The state machine currently controlling the entity. </summary>
        [ExportGroup("Nodes")]
        [Export] public EntityStateMachine? StateMachine { get; private set; } = null;

        /// <summary> The node that defines the node's collision. </summary>
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
        [Export] public EntityData Data {
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

        /// <inheritdoc/>
        public override void _Ready()
        {
            if(!Engine.IsEditorHint())
            {
                _interactionArea.BodyEntered += OnInteractionAreaEntered;
                _interactionArea.BodyExited += OnInteractionAreaExited;
            }
        }


        /// <summary> When something enters the entity's area of influence, add it to the nearby entities. </summary>
        /// <param name="body"> The node entering the area. </param>
        private void OnInteractionAreaEntered(Node2D body)
        {
            if(body is Entity entity && entity != this)
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


        /// <summary> Set the state machine controlling this entity, replacing any existing one. </summary>
        /// <typeparam name="T"> The kind of state machine to use. </typeparam>
        public void SetStateMachine<T>() where T : EntityStateMachine, new()
        {
            StateMachine?.QueueFree();
            T newMachine = new T();
            AddChild(newMachine);
            StateMachine = newMachine;
        }


        /// <summary> Try to get the state machine of a specific kind. </summary>
        /// <typeparam name="T"> The kind of state machine. </typeparam>
        /// <param name="stateMachine"> The returned state machine instance. </param>
        /// <returns> Whether the state machine was successfully retrieved. </returns>
        public Boolean TryGetStateMachine<T>(out T? stateMachine) where T : EntityStateMachine
        {
            stateMachine = StateMachine as T;
            return stateMachine != null;
        }


        /// <summary> Handle an incoming command. </summary>
        /// <param name="command"> The command this entity needs to act upon. </param>
        public void HandleCommand(EntityCommand command)
        {
            // Ensure that we have data / a state machine set to handle the command.
            ArgumentNullException.ThrowIfNull(Data);
            ArgumentNullException.ThrowIfNull(StateMachine);

            StateMachine.TryTransitionState(command);
        }


        /// <inheritdoc/>
        public override void _PhysicsProcess(Double delta)
        {
            if (!Engine.IsEditorHint())
            {
                if(Data != null)
                {
                    _nameLabel.Text = Data.Name.ToString();
                }
            }
        }


        /// <inheritdoc/>
        public override void _ExitTree()
        {
            if (!Engine.IsEditorHint())
            {
                _interactionArea.BodyEntered -= OnInteractionAreaEntered;
                _interactionArea.BodyExited -= OnInteractionAreaExited;
            }
        }


        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(Data);


        /// <inheritdoc/>
        public Boolean Equals(Entity? other)
        {
            Boolean result = false;
            if(Data != null && other != null && other.Data != null)
            {
                result = Data.Equals(other.Data);
            }
            return result;
        }
    }
}
