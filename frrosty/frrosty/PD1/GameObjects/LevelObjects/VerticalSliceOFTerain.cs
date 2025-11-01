using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;

using PD_1_Project_FrostyGame.PD1.General;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects
{
    //this is some shit code but i was to busy to fix it
    public class VerticalSliceOFTerain
    {
        public static int lastFloorHeight;
        public static Type lastFloorType;
        public static int enemyRepeatChance; //move code parts to terain to prevent statics
        public enum FloorType { IceBlock, Lava, Spike, PitFall }
        public int Xcoordinate { get => (int)ListOfTerainObjects.First().Position.X; }
        public List<GameObject> ListOfTerainObjects = new List<GameObject> { };
        public VerticalSliceOFTerain(int spawnPositionX, Type enemytype, bool flag)
        {
            generateCeilingPart(spawnPositionX);
            generateFloorPart(spawnPositionX, flag);
            if (Logic.IsRandomChanceSuccessful(1, 50))
                SpawnBird(spawnPositionX);
        }

        private void SpawnBird(int positionX)
        {
            Vector2 birdLocation = new Vector2(positionX, GameSettings.screenHeight / 2 + Random.Shared.Next(-2, 3) * GameSettings.SizeOfObjects.Y);
            ListOfTerainObjects.Add(Factory.CreateBird(birdLocation));
        }

        private void generateFloorPart(int positionX, bool flag)
        {
            int howHigh = lastFloorHeight;
            if (Logic.IsRandomChanceSuccessful(1, 3))
            {
                switch (Random.Shared.Next(0, 2))
                {
                    case 0:
                        if (lastFloorHeight < 3)
                        {
                            howHigh = ++lastFloorHeight;
                        }
                        break;
                    case 1:
                        if (lastFloorHeight > 1)
                        {
                            howHigh = --lastFloorHeight;
                        }
                        break;
                    default:
                        howHigh = lastFloorHeight;
                        break;
                }
            }
            Vector2 placePosition = new Vector2(positionX, GameSettings.screenHeight - GameSettings.SizeOfObjects.Y * howHigh);
            lastFloorHeight = howHigh;
            for (int i = 0; i < howHigh; i++)
            {
                if (i == 0)
                {
                    if (flag)
                    {
                        ListOfTerainObjects.Add(Factory.CreateFlag(placePosition));
                        lastFloorType = typeof(IceBlockObject);
                    }
                    else
                    {
                        if (lastFloorType == typeof(SpikesObject))
                        {
                            if (Logic.IsRandomChanceSuccessful(enemyRepeatChance, 100))
                            {
                                ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                enemyRepeatChance = (int)(enemyRepeatChance / 1.15f);
                                ListOfTerainObjects.Add(Factory.CreateSpike(placePosition, ListOfTerainObjects));
                                lastFloorType = typeof(SpikesObject);
                            }
                            else
                            {
                                ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                                lastFloorType = typeof(IceBlockObject);
                            }

                        }
                        else if (lastFloorType == typeof(LavaObject))
                        {
                            if (Logic.IsRandomChanceSuccessful(enemyRepeatChance, 100))
                            {
                                ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                enemyRepeatChance = (int)(enemyRepeatChance / 1.15f);
                                ListOfTerainObjects.Add(Factory.CreateLava(placePosition));
                                lastFloorType = typeof(LavaObject);
                            }
                            else
                            {
                                ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                                lastFloorType = typeof(IceBlockObject);
                            }
                        }
                        else if (lastFloorType == null)
                        {
                            if (Logic.IsRandomChanceSuccessful(enemyRepeatChance, 100))
                            {
                                ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                enemyRepeatChance = (int)(enemyRepeatChance / 1.15f);
                                lastFloorType = null;
                                break;
                            }
                            else
                            {
                                ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                                lastFloorType = typeof(IceBlockObject);
                            }
                        }
                        else if (lastFloorType == typeof(IceBlockObject))
                        {
                            enemyRepeatChance = 100;
                            if (Logic.IsRandomChanceSuccessful(1, 7))
                            {
                                bool breakForLoop = false;
                                switch (Random.Shared.Next(0, 3))
                                {
                                    case 0:
                                        ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                        ListOfTerainObjects.Add(Factory.CreateSpike(placePosition, ListOfTerainObjects));
                                        lastFloorType = typeof(SpikesObject);
                                        break;
                                    case 1:
                                        ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                        ListOfTerainObjects.Add(Factory.CreateLava(placePosition));
                                        lastFloorType = typeof(LavaObject);
                                        break;
                                    case 2:
                                        ListOfTerainObjects.Add(Factory.CreateCoin(new Vector2(placePosition.X, placePosition.Y - GameSettings.SizeOfObjects.Y)));
                                        lastFloorType = null;
                                        breakForLoop = true;
                                        break;
                                }
                                if (breakForLoop)
                                    break;
                            }
                            else
                            {
                                ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                                lastFloorType = typeof(IceBlockObject);
                            }
                        }
                    }
                }
                else
                {
                    ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                }
                placePosition = new Vector2(placePosition.X, placePosition.Y + GameSettings.SizeOfObjects.Y);
            }
        }

        private void generateCeilingPart(int positionX)
        {
            Vector2 placePosition = new Vector2(positionX, 0);
            int howHigh = Random.Shared.Next(1, 4);
            for (int i = 0; i < howHigh; i++)
            {
                ListOfTerainObjects.Add(Factory.CreateIceBlock(placePosition, ListOfTerainObjects));
                placePosition = new Vector2(placePosition.X, placePosition.Y + GameSettings.SizeOfObjects.Y);
            }
            if (Logic.IsRandomChanceSuccessful(1, 4))
            {
                ListOfTerainObjects.Add(Factory.CreateSpike(placePosition, ListOfTerainObjects));
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in ListOfTerainObjects)
                obj.Draw(spriteBatch);
        }
        public void Update()
        {
            foreach (GameObject obj in ListOfTerainObjects)
                obj.Update();

        }

    }

}
