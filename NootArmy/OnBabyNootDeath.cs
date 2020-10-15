using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace NootArmy
{
    class OnBabyNootDeath
    {
        public static void Patch()
        {
            On.SmallNeedleWorm.Scream += SmallNeedleWorm_Scream;
        }

        private static void SmallNeedleWorm_Scream(On.SmallNeedleWorm.orig_Scream orig, SmallNeedleWorm self)
        {
            if (self.hasScreamed)
                return;
            Creature creature = self.ClosestCreature();
            if (creature is Player)
            {
                SendNootArmy();
            }
            orig(self);
        }

        private static void SendNootArmy()
        {
            Game game = new Game();
            //Thank you Lee.
            AbstractRoom adjacentRoom;
            int numOfNoodles = 20;
            Debug.Log("RELEASE THE NOOTS!");
            for (int i = 0; i < game.player.room.exitAndDenIndex.Length; i++)
            {
                if (game.player.room.WhichRoomDoesThisExitLeadTo(game.player.room.exitAndDenIndex[i]) != null)
                {
                    adjacentRoom = game.player.room.WhichRoomDoesThisExitLeadTo(game.player.room.exitAndDenIndex[i]);
                    WorldCoordinate node = adjacentRoom.RandomNodeInRoom();
                    if (adjacentRoom.realizedRoom == null)
                    {
                        adjacentRoom.RealizeRoom(game.player.room.world, game.player.room.world.game);
                    }
                    for (int c = 0; c < numOfNoodles; c++)
                    {
                        Debug.Log("Spawned a Noodle Fly");
                        AbstractCreature abstractNoodle = new AbstractCreature(game.player.room.world, StaticWorld.GetCreatureTemplate(CreatureTemplate.Type.BigNeedleWorm), null, node, adjacentRoom.world.game.GetNewID());
                        adjacentRoom.AddEntity(abstractNoodle);
                        abstractNoodle.RealizeInRoom();
                        abstractNoodle.state.socialMemory.GetOrInitiateRelationship(game.player.abstractCreature.ID).like = -50f;
                        abstractNoodle.state.socialMemory.GetOrInitiateRelationship(game.player.abstractCreature.ID).tempLike = -50f;
                        abstractNoodle.abstractAI.InternalSetDestination(game.player.coord);
                    }
                    break;
                }
            }
        }
    }
}
