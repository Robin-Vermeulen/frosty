
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using PD_1_Project_FrostyGame.PD1.General;

using System.Collections.Generic;


namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects
{
    public class Terain
    {
        private int _terainCounter;
        public List<VerticalSliceOFTerain> ListOfTerainSlices { get; set; } = new List<VerticalSliceOFTerain>();
        public Terain()
        {
            GenerateTerain();
        }

        private void GenerateTerain()
        {
            int positionX = 0;
            do
            {
                if (_terainCounter++ > GameSettings.ammountOflevelsBeforeFlag * GameSettings.levelWithd)
                {
                    ListOfTerainSlices.Add(new VerticalSliceOFTerain(positionX, typeof(IceBlockObject), true));
                    _terainCounter = 0;
                }
                else
                    ListOfTerainSlices.Add(new VerticalSliceOFTerain(positionX, typeof(SpikesObject), false));
                positionX += (int)GameSettings.SizeOfObjects.X;
            }
            while (positionX < GameSettings.screenWitdh * 2);
        }

        public void Update()
        {
            foreach (VerticalSliceOFTerain slice in ListOfTerainSlices)
            {
                slice.Update();
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            foreach (VerticalSliceOFTerain slice in ListOfTerainSlices)
            {
                slice.Draw(spritebatch);
            }
        }

        public void AddSlice(int positionX)
        {

            if (_terainCounter++ > GameSettings.ammountOflevelsBeforeFlag * GameSettings.levelWithd)
            {
                ListOfTerainSlices.Add(new VerticalSliceOFTerain(positionX, typeof(IceBlockObject), true));
                _terainCounter = 0;
            }
            else
                ListOfTerainSlices.Add(new VerticalSliceOFTerain(positionX, typeof(SpikesObject), false));
        }
    }
}
