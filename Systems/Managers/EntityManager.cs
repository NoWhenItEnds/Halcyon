#nullable disable warnings
using System;
using System.Collections.Generic;
using Godot;
using Halcyon.Entities;
using Halcyon.Entities.Data;
using Halcyon.Entities.Data.Components;
using Halcyon.Entities.States.Machines;
using Halcyon.Utilities.Singletons;

namespace Halcyon.Managers
{
    /// <summary> The manager for all entities within the game world. </summary>
    public partial class EntityManager : SingletonNode2D<EntityManager>
    {
        /// <summary> The prefab used for spawning new actors. </summary>
        [ExportGroup("Resources")]
        [Export] private PackedScene _entityPrefab;


        /// <summary> The entity currently controlled by the player. </summary>
        public Entity PlayerEntity { get; private set; }


        /// <summary> A reference to all the entities within the game world and their controller, if it has one. </summary>
        /// <remarks> A null as the value indicates that it doesn't currently have an associated controller. </remarks>
        private readonly Dictionary<Entity, EntityController?> ENTITIES = new Dictionary<Entity, EntityController?>();


        public override void _Ready()
        {
            // Get hand-placed entities.
            foreach (Node child in GetChildren())
            {
                if(child is Entity entity)
                {
                    ENTITIES.TryAdd(entity, null);  // TODO - How to handle controller?
                }
                else
                {
                    GD.PushWarning($"Non-entity, '{child.Name}' shouldn't be childed to {Name}.");
                }
            }

            // Create player.
            TrySpawnHuman(Vector2.Zero, out Entity? player);
            PlayerEntity = player;

            Random random = new Random();

            for (Int32 i = 0; i < 100; i++)
            {
                Vector2 position = new Vector2(random.NextSingle() * 1000, random.NextSingle() * 1000);
                TrySpawnDoor(position, out _);
            }
        }


        public override void _PhysicsProcess(Double delta)
        {
            foreach (EntityController? controller in ENTITIES.Values)
            {
                if(controller != null)
                {
                    //controller.DoTest(delta);
                }
            }
        }


        public Boolean TrySpawnHuman(Vector2 position, out Entity? entity)
        {
            entity = _entityPrefab.InstantiateOrNull<Entity>();
            EntityController controller = new EntityController(entity);

            // Attempt to add the entity to the game world.
            Boolean isSuccess = ENTITIES.TryAdd(entity, controller);
            if (isSuccess)
            {
                AddChild(entity);
                entity.GlobalPosition = position;
                entity.Data = new EntityData();
                entity.Data.TryAddComponent(new StatComponent());
                entity.Data.TryAddComponent(new SpeedComponent());
                entity.Data.TryAddComponent(new StaminaComponent());
                entity.Data.TryAddComponent(new EntertainmentComponent());
                entity.Data.ResolveComponents();
                entity.SetStateMachine<HumanStateMachine>();
            }

            return isSuccess;
        }


        public Boolean TrySpawnDoor(Vector2 position, out Entity? entity)
        {
            entity = _entityPrefab.InstantiateOrNull<Entity>();

            // Attempt to add the entity to the game world.
            Boolean isSuccess = ENTITIES.TryAdd(entity, null);
            if (isSuccess)
            {
                AddChild(entity);
                entity.GlobalPosition = position;
                entity.Data = new EntityData();
                entity.SetStateMachine<DoorStateMachine>();
            }

            return isSuccess;
        }
    }
}
