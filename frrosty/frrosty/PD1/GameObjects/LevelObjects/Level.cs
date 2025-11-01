using PD_1_Project_FrostyGame.PD1.sprite;
using System;
using System.Collections.Generic;
using PD_1_Project_FrostyGame.PD1.General;

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects
{
    public class Level
    {
        private static int lastChoise = -1;
        private static int beforeLastChoice = -2;
        private static int twobeforeLastChoice = -3;
        private Vector2 placePosition { get; set; }
        public bool IsNearavatar { get; set; }

        public enum LevelSelection { StairCase, Spikes, Walls, Lava, Pits, Birds, Platforms, lavaSpikes, AllSpikes, AllLava, StartLevel, Flag };

        private int[,] _levelLayout; //0 empty, 1 iceblock, 2 spikes 3 lava 4 birds 5 coins 6 flags
        public List<GameObject> ListOfLevelObjects { get; private set; } = new List<GameObject>();

        public Level(LevelSelection choice, Vector2? topLeftPosition = null)
        {
            placePosition = topLeftPosition ?? Vector2.Zero;
            CreateLevel(choice);
        }
        /// <summary>
        /// random level selection
        /// </summary>
        public Level(Vector2 topLeftPosition)
        {
            int enumLenght;
            int randomint;

            placePosition = topLeftPosition;
            List<LevelSelection> randomPool = GetRandomLevelPool();      //get all the levels that are eligable for the random pick
            randomint = Random.Shared.Next(0, randomPool.Count());     //generate a random number in the range of the enum
            ;
            CreateLevel(randomPool[randomint]);

            twobeforeLastChoice = beforeLastChoice;
            beforeLastChoice = lastChoise;
            lastChoise = (int)randomPool[randomint];
        }
        private static List<LevelSelection> GetRandomLevelPool()
        {
            return Enum.GetValues<LevelSelection>()
                .Cast<LevelSelection>()
                .Where(level => level != LevelSelection.StartLevel
                             && level != LevelSelection.Flag
                             && level != (LevelSelection)lastChoise
                             && level != (LevelSelection)beforeLastChoice
                             && level != (LevelSelection)twobeforeLastChoice)
                .ToList();
        }
        public void CreateLevel(LevelSelection choice)
        {
            switch (choice)
            {
                case LevelSelection.StairCase:
                    CreateStaircase();
                    break;
                case LevelSelection.Spikes:
                    CreateBasicSpikes();
                    break;
                case LevelSelection.Walls:
                    CreateWalls();
                    break;
                case LevelSelection.Lava:
                    CreateBasicLava();
                    break;
                case LevelSelection.Pits:
                    CreatePits();
                    break;
                case LevelSelection.Birds:
                    Createbirds();
                    break;
                case LevelSelection.Platforms:
                    CreatePlatforms();
                    break;
                case LevelSelection.StartLevel:
                    CreateFlatSurface();
                    break;
                case LevelSelection.Flag:
                    CreateFlagLevel();
                    break;
                case LevelSelection.lavaSpikes:
                    CreateLavaSpikes();
                    break;
                case LevelSelection.AllLava:
                    CreateAllLava();
                    break;
                case LevelSelection.AllSpikes:
                    CreateAllSpike();
                    break;
                default:
                    CreateFlatSurface();
                    break;
            }

        }

        private void CreateAllSpike()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 0},
                { 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 5, 5, 5, 0, 0, 0, 5, 5, 5, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreateAllLava()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1},
                { 0, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,1, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,0, 0},
                { 1, 1, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreateFlagLevel()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
                { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreatePlatforms()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 5, 0, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0},
                { 0, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                       };
            FillListOfLevelObjects();

        }

        private void Createbirds()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 , 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 5, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 , 4, 5, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 5, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 5, 4, 5, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 5, 4, 5, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 5, 0, 0, 0, 5, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 5, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 , 0, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 , 1, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreatePits()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 2, 1, 2, 1, 2, 2, 1, 2, 1, 2, 0, 0, 0, 0},
                { 0, 0, 2, 0, 2, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0},
                { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0},
                { 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0},
                { 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0},
                { 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0},
                { 1, 1, 0, 5, 0, 1, 0, 5, 0, 1, 0, 5, 0, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreateBasicLava()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1},
                { 1, 3, 3, 3, 3, 3, 3, 3, 3, 3 ,3, 3,3,3,1},
                { 0, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0,5,5,0},
                { 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 5,0,0,0},
                { 0, 0, 0, 0, 5, 5, 0, 4, 0, 5, 5, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0},
                { 0, 0, 0, 0, 0, 0, 5, 5, 5, 0, 0, 0,0,0,0},
                { 0, 1, 3, 3, 3, 3, 1, 1, 1, 3, 3, 3,3,1,0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1} };
            FillListOfLevelObjects();
        }

        private void CreateWalls()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1},
                { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1,1,1,1},
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1,1,1,1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1,1,1,1},
                { 0, 0, 0, 5, 5, 5, 0, 0, 0, 1, 1, 1,1,1,1},
                { 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 1, 1,1,1,1},
                { 0, 0, 0, 1, 1, 0, 0, 5, 0, 0, 0, 0,0,0,0},
                { 0, 0, 1, 1, 1, 0, 0, 5, 0, 0, 0, 0,0,0,0},
                { 0, 0, 1, 1, 1, 0, 0, 5, 0, 0, 0, 0,0,0,0},
                { 0, 1, 1, 1, 1, 0, 0, 5, 0, 0, 0, 0,0,0,0},
                { 0, 1, 1, 1, 1, 1, 0, 0, 5, 0, 0, 0,0,0,0},
                { 1, 1, 1, 1, 1, 1, 0, 0, 0, 5, 0, 0,0,0,1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 5, 0,0,1,1},
                { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0,1,1,1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,1,1,1} };
            FillListOfLevelObjects();
        }

        private void CreateBasicSpikes()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0},
                { 0, 0, 0, 2, 1, 1, 1, 1, 1, 1, 1, 2, 0, 0, 0},
                { 0, 0, 0, 0, 2, 1, 1, 1, 1, 1, 2, 0, 0, 0, 0},
                { 0, 5, 0, 0, 0, 2, 1, 1, 1, 2, 0, 0, 0, 5, 0},
                { 0, 0, 5, 0, 0, 0, 2, 1, 2, 0, 0, 0, 5, 0, 0},
                { 0, 0, 0, 5, 0, 0, 0, 2, 0, 0, 0, 5, 0, 0, 0},
                { 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 5, 0, 0, 0, 5, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 5, 0, 5, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0},
                { 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1} };
            FillListOfLevelObjects();
        }
        private void CreateFlatSurface()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };
            FillListOfLevelObjects();
        }

        private void CreateStaircase()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1,1,1},
                { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 ,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 ,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0 ,0,0,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 ,5,5,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 ,1,1,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0 ,1,1,0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 0, 0 ,1,1,0},
                { 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, 0 ,1,1,0},
                { 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 0, 0 ,1,1,0},
                { 0, 0, 0, 0, 0, 0, 5, 0, 1, 1, 0, 0 ,1,1,0},
                { 0, 0, 0, 0, 5, 5, 0, 0, 1, 1, 0, 0 ,1,1,0},
                { 0, 0, 0, 5, 1, 1, 0, 0, 1, 1, 0, 0 ,1,1,0},
                { 0, 0, 5, 0, 1, 1, 0, 0, 1, 1, 0, 0 ,1,1,0},
                { 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2 ,1,1,1} };
            FillListOfLevelObjects();
        }
        private void CreateLavaSpikes()
        {
            _levelLayout = new int[GameSettings.levelWithd, GameSettings.levelHeight] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0},
                { 0, 0, 0, 0, 0, 5, 5, 5, 5, 5, 5, 5, 5, 5, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 3, 2, 2, 2, 2, 2, 2, 2},
                { 0, 0, 0, 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 0, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1} };
            FillListOfLevelObjects();
        }
        public void FillListOfLevelObjects()
        {
            Sprite sprite = null;
            for (int row = 0; row < GameSettings.levelWithd; row++)
            {

                for (int collum = 0; collum < GameSettings.levelHeight; collum++)
                {
                    switch (_levelLayout[row, collum])
                    {
                        case 1:
                            ListOfLevelObjects.Add(Factory.CreateIceBlock(placePosition, ListOfLevelObjects));
                            break;
                        case 2:
                            ListOfLevelObjects.Add(Factory.CreateSpike(placePosition, ListOfLevelObjects));
                            break;
                        case 3:
                            ListOfLevelObjects.Add(Factory.CreateLava(placePosition));
                            break;
                        case 4:
                            ListOfLevelObjects.Add(Factory.CreateBird(placePosition));
                            break;
                        case 5:
                            ListOfLevelObjects.Add(Factory.CreateCoin(placePosition));
                            break;
                        case 6:
                            ListOfLevelObjects.Add(Factory.CreateFlag(placePosition));
                            break;
                    }
                    placePosition = new Vector2(placePosition.X + GameSettings.SizeOfObjects.X, placePosition.Y);
                }
                placePosition = new Vector2(ListOfLevelObjects.Min(o => o.Position.X), placePosition.Y + GameSettings.SizeOfObjects.Y);
            }
        }
        public void update()
        {
            updateObjects();
            SeeIfNearavatar();
        }
        private void updateObjects()
        {
            foreach (GameObject obj in ListOfLevelObjects)
            {
                if (obj != null)
                    obj.Update();
            }
        }
        public void SeeIfNearavatar()
        {
            if (ListOfLevelObjects.Min(o => o.Position.X) < GameSettings.Startposition.X + (GameSettings.SizeOfObjects.X * 2))
            {
                IsNearavatar = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (GameObject obj in YieldGetAllObjects(this))
            {
                if (obj != null)
                    obj.Draw(spriteBatch);
            }

        }

        public static IEnumerable<GameObject> YieldGetAllObjects(Level level)
        {
            foreach (GameObject obj in level.ListOfLevelObjects)
                yield return obj;
        }

    }
}