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


        /// <summary> A reference to all the actors within the game world and their controller, if it has one. </summary>
        /// <remarks> A null as the value indicates that it doesn't currently have an associated controller. </remarks>
        private readonly Dictionary<ActorEntity, ActorController?> ACTORS = new Dictionary<ActorEntity, ActorController?>();


        public override void _Ready()
        {
            // Create player.
            PlayerActor = _actorPrefab.InstantiateOrNull<ActorEntity>();
            AddChild(PlayerActor);

            Random random = new Random();

            for (Int32 i = 0; i < 100; i++)
            {
                Vector2 position = new Vector2(random.NextSingle() * 1000, random.NextSingle() * 1000);
                TrySpawnActor(position, out _);
            }
        }


        public override void _PhysicsProcess(Double delta)
        {
            foreach (ActorController? controller in ACTORS.Values)
            {
                if(controller != null)
                {
                    //controller.DoTest(delta);
                }
            }
        }



        public Boolean TrySpawnActor(Vector2 position, out ActorEntity? entity)
        {
            entity = _actorPrefab.InstantiateOrNull<ActorEntity>();
            ActorController controller = new ActorController(entity);

            // Attempt to add the entity to the game world.
            Boolean isSuccess = ACTORS.TryAdd(entity, controller);
            if (isSuccess)
            {
                AddChild(entity);
                entity.GlobalPosition = position;
            }

            return isSuccess;
        }
    }
}
