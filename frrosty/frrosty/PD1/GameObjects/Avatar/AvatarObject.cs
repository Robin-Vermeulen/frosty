
using PD_1_Project_FrostyGame.PD1.GameObjects.Avatar.AvatarParts;
using PD_1_Project_FrostyGame.PD1.General;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PD_1_Project_FrostyGame.PD1.GameObjects.Avatar
{

    public class AvatarObject
    {
        public TopHatObject TopHatObject { get; set; }
        /// <summary>
        /// object instance of the head
        /// </summary>
        public HeadObject HeadObject { get; private set; }
        /// <summary>
        /// list of all snowball instances currently acitve
        /// </summary>
        public List<GameObject> ListOfAllAvatarParts { get; set; } = new List<GameObject>();
        /// <summary>
        /// constructor for a avatar, it creates a head at a startlocation and with a texture, boht of which are specified in gamesettings
        /// </summary>
        public AvatarObject()
        {
            HeadObject = Factory.CreateHead();
            TopHatObject = Factory.CreateTopHat();
            ListOfAllAvatarParts.Add(HeadObject);
            ListOfAllAvatarParts.Add(TopHatObject);
        }
        /// <summary>
        /// draws the head and all the snowballs curently active
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject bodyPart in ListOfAllAvatarParts)
                bodyPart.Draw(spriteBatch);
        }

        /// <summary>
        /// updates head and body,calculates gravety, detection for getting hit, adds snowballs
        /// </summary>
        public void Update()
        {
            if (UserInput.HasPressedKey(Keys.Space) || UserInput.HasPressedLeftMouseButton())
                AddBodyPart();

            RemoveInactiveBodyParts();

            foreach (GameObject body in ListOfAllAvatarParts)
            {
                body.Update();
            }
        }
        /// <summary>
        /// adds a snowball under the head and moves the head up
        /// </summary>
        private void AddBodyPart()
        {
            if (!TopHatObject.IsTouchingPlatform) //the top hat works as a detection system for when the snowman is hitting the ceiling, and thus when it is not allowed to make new parts
            {
                GameSettings.SoundPop.Play(GameSettings.Volume, (float)Random.Shared.Next(-5, 5) / 100, 0.5f); //the pitch is between -0.95 and 1.05 to avoid audio fatigue

                ListOfAllAvatarParts.Add(Factory.CreateSnowball(HeadObject.Position));    //make a new snowbal at the same position of the head

                TopHatObject.Position = new Vector2(TopHatObject.Position.X, TopHatObject.Position.Y - TopHatObject.Size.Y);  
                HeadObject.Position = new Vector2(HeadObject.Position.X, HeadObject.Position.Y - HeadObject.Size.Y); // push the top hat and head up
            }
        }
        /// <summary>
        /// removes non acctive snowballs from the llist
        /// </summary>
        private void RemoveInactiveBodyParts()
        {
            ListOfAllAvatarParts.RemoveAll(notActive => !notActive.IsActive);
        }
        /// <summary>
        /// lets the head and snowballs fall down if they dont hit a platform
        /// </summary>

    }
}

