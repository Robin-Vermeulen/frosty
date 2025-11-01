
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects.LevelParts;
using PD_1_Project_FrostyGame.PD1.General;
using PD_1_Project_FrostyGame.PD1.sprite;
using System;
using System.Collections.Generic;


namespace PD_1_Project_FrostyGame.PD1.GameObjects.LevelObjects
{
    public class PlatForm
    {
        public List<GameObject> ListOfObjects { get; set; }
        public PlatForm()
        {
            ListOfObjects = MakeRandomPlatfor();
        }
        private List<GameObject> MakeRandomPlatfor()
        {
            List<GameObject> frostyObjects = new List<GameObject>();
            Random r = new Random();
            Sprite sprite;

            int platFormLenght = r.Next(3, 7);
            Vector2 platFormPosition = new Vector2(GameSettings.screenWitdh, r.Next(6, GameSettings.levelHeight - 5) * GameSettings.SizeOfObjects.Y);


            for (int i = 0; i < platFormLenght; i++)
            {
                int BlockChoice = r.Next(0, 2);
                switch (BlockChoice)
                {
                    case 0:
                        sprite = new Sprite(GameSettings.TextureIceblock, platFormPosition);
                        frostyObjects.Add(new IceBlockObject(sprite));
                        break;
                    case 1:
                        sprite = new Sprite(GameSettings.TextureSpikes, platFormPosition);
                        frostyObjects.Add(new SpikesObject(sprite));
                        break;
                }
                platFormPosition = new Vector2(platFormPosition.X + GameSettings.SizeOfObjects.X, platFormPosition.Y);
            }
            return frostyObjects;
        }
        public void Update()
        {
            foreach (GameObject obj in ListOfObjects)
            {
                obj.Update();
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject obj in ListOfObjects)
                obj.Draw(spriteBatch);
        }
    }
}
