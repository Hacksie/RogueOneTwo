using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace HackedDesign
{
    public abstract class LevelGenerator : ILevelGenerator
    {
        protected const string TopLeft = "tl";
        protected const string TopRight = "tr";
        protected const string BottomLeft = "bl";
        protected const string BottomRight = "br";

        protected float randomChance = 0.75f;
        protected float lineOfSightChance = 0.3f;

        public static ILevelGenerator GetGenerator()
        {
            ILevelGenerator generator;

            generator = new RandomLevelGenerator();

            return generator;
        }

        public static Level Generate(LevelGenTemplate template, int levelCount) => Generate(template, levelCount, 0, 0, 0, 0);

        public static Level Generate(LevelGenTemplate template, int levelCount, int length, int height, int width, int enemies)
        {
            var generator = GetGenerator();
            return generator.GenerateLevel(template, levelCount, length, height, width, enemies);
        }


        public Level GenerateLevel(LevelGenTemplate template, int levelCount) => GenerateLevel(template, levelCount, 0, 0, 0, 0);

        public Level GenerateLevel(LevelGenTemplate template, int levelCount, int enemies) => GenerateLevel(template, levelCount, 0, 0, 0, enemies);

        public abstract Level GenerateLevel(LevelGenTemplate template, int levelCount, int length, int height, int width, int enemies);

        protected void GenerateElements(Level level)
        {
            for (int y = 0; y < level.map.Count(); y++)
            {
                for (int x = 0; x < level.map[y].rooms.Count(); x++)
                {
                    if (level.map[y].rooms[x] != null)
                    {
                        GenerateRoomElements(level.map[y].rooms[x], 1, level.template);
                    }
                }
            }
        }

        protected void GenerateRoomElements(ProxyRoom proxyRoom, float chance, LevelGenTemplate template)
        {
            AddRoomElement(ref proxyRoom.topLeft, TopLeft, proxyRoom.left, proxyRoom.top, chance, template);
            AddRoomElement(ref proxyRoom.topRight, TopRight, proxyRoom.right, proxyRoom.top, chance, template);
            AddRoomElement(ref proxyRoom.bottomLeft, BottomLeft, proxyRoom.left, proxyRoom.bottom, chance, template);
            AddRoomElement(ref proxyRoom.bottomRight, BottomRight, proxyRoom.right, proxyRoom.bottom, chance, template);
        }

        protected void AddRoomElement(ref List<string> cornerElements, string corner, string wall1, string wall2, float chance, LevelGenTemplate template)
        {
            if (UnityEngine.Random.Range(0.0f, 1.0f) <= chance)
            {
                var goList = FindRoomElements(corner, wall1, wall2, template).ToList();

                if (goList != null && goList.Count > 0)
                {
                    goList.Randomize();

                    cornerElements.Add(goList[0].name);
                }
            }
        }

        protected IEnumerable<GameObject> FindRoomElements(string corner, string wall1, string wall2, LevelGenTemplate levelGenTemplate)
        {
            return levelGenTemplate.levelElements.Where(g => g != null && MatchPrefabName(g.name, corner, wall1, wall2));
        }

        protected bool MatchPrefabName(string prefabName, string corner, string wall1, string wall2)
        {
            string[] nameSplit = prefabName.ToLower().Split('_');
            corner = corner.ToLower();
            wall1 = wall1.ToLower();
            wall2 = wall2.ToLower();

            if (nameSplit.Length != 2)
            {
                Debug.LogError("LevelGenerator: " + "Invalid prefab name - " + prefabName);
                return false;
            }

            string first = nameSplit[1].Substring(0, 1);
            string second = nameSplit[1].Substring(1, 1);

            return (nameSplit[0] == corner &&
                ((wall1 == ProxyRoom.Open && ProxyRoom.OpenOptions.IndexOf(first) >= 0) ||
                    (wall1 == ProxyRoom.Door && ProxyRoom.DoorOptions.IndexOf(first) >= 0) ||
                    (wall1 == ProxyRoom.Wall && ProxyRoom.WallOptions.IndexOf(first) >= 0) ||
                    (wall1 == ProxyRoom.Exit && ProxyRoom.ExitOptions.IndexOf(first) >= 0) ||
                    (wall1 == ProxyRoom.Entry && ProxyRoom.EntryOptions.IndexOf(first) >= 0)) &&
                ((wall2 == ProxyRoom.Open && ProxyRoom.OpenOptions.IndexOf(second) >= 0) ||
                    (wall2 == ProxyRoom.Door && ProxyRoom.DoorOptions.IndexOf(second) >= 0) ||
                    (wall2 == ProxyRoom.Wall && ProxyRoom.WallOptions.IndexOf(second) >= 0) ||
                    (wall2 == ProxyRoom.Exit && ProxyRoom.ExitOptions.IndexOf(second) >= 0) ||
                    (wall2 == ProxyRoom.Entry && ProxyRoom.EntryOptions.IndexOf(second) >= 0))
            );
        }
    }
}