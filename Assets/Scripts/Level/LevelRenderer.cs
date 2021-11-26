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
        [SerializeField] private GameObject levelParent;
        

        public void Render(Level level)
        {
            DestroyLevel();
            PopulateLevelTilemap(level);
            PopulateEnemySpawns(level);
        }

        public void DestroyLevel()
        {
            // Destroy Tiles
            for (int k = 0; k < levelParent.transform.childCount; k++)
            {
                levelParent.transform.GetChild(k).gameObject.SetActive(false);
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

                    Vector3 roomPosition = new Vector3(j * level.template.spanHorizontal + (level.template.spanHorizontal / 2), i * -level.template.spanVertical + ((level.template.levelHeight - 1) * level.template.spanVertical) + (level.template.spanVertical / 2), 0);

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


                    if (room.isEntry)
                    {
                        Instantiate(level.template.entryElement, roomPosition, Quaternion.identity, levelParent.transform);
                    }

                    if (room.isExit)
                    {
                        Instantiate(level.template.exitElement, roomPosition, Quaternion.identity, levelParent.transform);
                    }
                }
            }
        }

        GameObject FindRoomEntity(string name, LevelGenTemplate levelGenTemplate) => levelGenTemplate.levelElements.FirstOrDefault(g => g != null && g.name == name);

        public void PopulateEnemySpawns(Level level) => EnemyPool.Instance.SpawnEnemies(level);
    }
}