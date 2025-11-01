using System;


using System.Collections.Generic;

using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;


namespace PD_1_Project_FrostyGame.PD1.General
{
    public static class Data
    {
        public static ContentManager contentManager;
        public static Texture2D LoadTexture2D(string filePath)
        {
            return contentManager.Load<Texture2D>(filePath);
        }
        public static SpriteFont LoadSpriteFont(string filePath)
        {
            return contentManager.Load<SpriteFont>(filePath);
        }
        public static SoundEffect LoadSoundEffect(string filePath)
        {
            return contentManager.Load<SoundEffect>(filePath);
        }
        public static Song LoadSong(string filePath)
        {
            return contentManager.Load<Song>(filePath);
        }
        public static void SerializeAndSend(List<PlayerScore> scores, string file)
        {
            if (File.Exists(file))
            {
                string serializedText = JsonConvert.SerializeObject(scores);
                using (StreamWriter streamWriter = new StreamWriter(file))
                {
                    streamWriter.Write(serializedText);
                }
            }
        }
        public static List<PlayerScore> ReadAndDesiriliazeScores(string file)
        {
            List<PlayerScore> list = new List<PlayerScore>();
            if (File.Exists(file))
            {
                using (StreamReader streamReader = new StreamReader(file))
                {
                    try
                    {
                        list = JsonConvert.DeserializeObject<List<PlayerScore>>(streamReader.ReadToEnd());
                    }
                    catch (Exception ex)
                    {
                        Console.BackgroundColor = System.ConsoleColor.Red;
                        Debug.WriteLine(ex.Message);
                        Console.BackgroundColor = System.ConsoleColor.Black;
                    }
                    return list;
                }
            }
            else return list;
        }


    }
}
