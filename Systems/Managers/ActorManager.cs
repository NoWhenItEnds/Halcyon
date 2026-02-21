#nullable disable warnings
using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> The manager for all actor entities within the game world. </summary>
    public partial class ActorManager : SingletonNode2D<ActorManager>
    {
        /// <summary> The prefab used for spawning new actor entities. </summary>
        [ExportGroup("Resources")]
        [Export] private PackedScene _actorPrefab;


        public ActorEntity PlayerActor { get; private set; }


        /// <summary> A reference to all the actors within the game world. </summary>
        private readonly HashSet<ActorEntity> ACTORS = new HashSet<ActorEntity>();


        public override void _Ready()
        {
            // Create player.
            if (TrySpawnActor(Vector2.Zero, out var playerEntity) && playerEntity != null)
            {
                PlayerActor = playerEntity;
            }

            Random random = new Random();

            for (Int32 i = 0; i < 100; i++)
            {
                Vector2 position = new Vector2(random.NextSingle() * 1000, random.NextSingle() * 1000);
                TrySpawnActor(position, out _);
            }
        }


        public Boolean TrySpawnActor(Vector2 position, out ActorEntity? entity)
        {
            entity = _actorPrefab.InstantiateOrNull<ActorEntity>();

            // Attempt to add the entity to the game world.
            Boolean isSuccess = ACTORS.Add(entity);
            if (isSuccess)
            {
                AddChild(entity);
                entity.GlobalPosition = position;
            }

            return isSuccess;
        }
    }
}
