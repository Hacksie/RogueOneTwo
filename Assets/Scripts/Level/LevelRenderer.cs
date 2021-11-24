using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace HackedDesign
{
    public class LevelRenderer : MonoBehaviour
    {
        private const string TOPLEFT = "tl";
        private const string TOPRIGHT = "tr";
        private const string BOTTOMLEFT = "bl";
        private const string BOTTOMRIGHT = "br";

        [SerializeField] private GameObject levelParent;
        [Header("Prefabs")]
        [SerializeField] private GameObject doorewPrefab = null;
        [SerializeField] private GameObject doornsPrefab = null;
        [SerializeField] private GameObject exitewPrefab = null;
        [SerializeField] private GameObject exitnsPrefab = null;
        [SerializeField] private GameObject entryewPrefab = null;
        [SerializeField] private GameObject entrynsPrefab = null;
        [SerializeField] private GameObject pointOfInterestPrefab = null;


        public void Render(Level level)
        {
            Debug.Log(name + "rendering level");
            DestroyLevel();
            PopulateLevelTilemap(level);
        }

        public void DestroyLevel()
        {
            // Destroy Tiles
            for (int k = 0; k < levelParent.transform.childCount; k++)
            {
                Destroy(levelParent.transform.GetChild(k).gameObject);
            }
        }

        void PopulateLevelTilemap(Level level)
        {
            for (int i = 0; i < level.map.Count(); i++)
            {
                for (int j = 0; j < level.map[i].rooms.Count(); j++)
                {

                    var room = level.map[i].rooms[j];

                    if (room == null)
                    {
                        continue;
                    }

                    Vector3 roomPosition = new Vector3(j * level.template.spanHorizontal, i * -level.template.spanVertical + ((level.template.levelHeight - 1) * level.template.spanVertical), 0);

                    // BL
                    for (int e = 0; e < room.bottomLeft.Count; e++)
                    {
                        var go = FindRoomEntity(room.bottomLeft[e], level.template);
                        if (go == null)
                        {
                            Debug.LogError(name + " null game object returned from FindRoomEntity");
                            continue;
                        }
                        Instantiate(go, roomPosition, go.transform.rotation, levelParent.transform);
                    }

                    // BR
                    for (int e = 0; e < room.bottomRight.Count; e++)
                    {
                        var go = FindRoomEntity(room.bottomRight[e], level.template);
                        if (go == null)
                        {
                            Debug.LogError(name + " null game object returned from FindRoomEntity");
                            continue;
                        }
                        Instantiate(go, roomPosition, go.transform.rotation, levelParent.transform);
                    }

                    // TL
                    for (int e = 0; e < room.topLeft.Count; e++)
                    {

                        var go = FindRoomEntity(room.topLeft[e], level.template);
                        if (go == null)
                        {
                            Debug.LogError(name + " null game object returned from FindRoomEntity");
                            continue;
                        }
                        Instantiate(go, roomPosition, go.transform.rotation, levelParent.transform);
                    }

                    //TR
                    for (int e = 0; e < room.topRight.Count; e++)
                    {
                        var go = FindRoomEntity(room.topRight[e], level.template);
                        if (go == null)
                        {
                            Debug.LogError(name + " null game object returned from FindRoomEntity");
                            continue;
                        }
                        Instantiate(go, roomPosition, go.transform.rotation, levelParent.transform);
                    }
                }
            }
        }

        GameObject FindRoomEntity(string name, LevelGenTemplate levelGenTemplate)
        {
            return levelGenTemplate.levelElements.FirstOrDefault(g => g != null && g.name == name);
        }
    }
}