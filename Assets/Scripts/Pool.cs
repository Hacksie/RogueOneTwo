using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class Pool : MonoBehaviour
    {
        [Header("Prefabs")]
        public List<GameObject> pickups = new List<GameObject>();
        public GameObject explosion = null;
        public GameObject smallExplosion = null;
        [Header("Pools")]
        public List<Enemy> enemyPool = new List<Enemy>();
        public List<GameObject> propPool = new List<GameObject>();
        public List<GameObject> trapPool = new List<GameObject>();
        

        public static Pool Instance { get; private set; }
        private int maxEnemyCount = 0;

        Pool() => Instance = this;

        public int EnemyCount { get { return enemyPool.Count; }}
        public int MaxEnemyCount { get { return maxEnemyCount; }}

        public void SpawnRandomPickup(Vector2 position)
        {
            var go = Instantiate(pickups[Random.Range(0, pickups.Count)], position, Quaternion.identity, this.transform);
            propPool.Add(go);
        }

        public void SpawnExplosion(Vector2 position)
        {
            var go = Instantiate(explosion, position, Quaternion.identity, this.transform);
            propPool.Add(go);
        }

        public void SpawnSmallExplosion(Vector2 position)
        {
            var go = Instantiate(smallExplosion, position, Quaternion.identity, this.transform);
            propPool.Add(go);
        }        

        public void SpawnEnemies(Level level)
        {
            if (level.enemySpawnLocationList == null)
            {
                return;
            }

            foreach (Spawn spawn in level.enemySpawnLocationList)
            {
                var enemyPrefab = level.template.enemies.FirstOrDefault(e => e.name == spawn.name);

                if (enemyPrefab is null)
                {
                    continue;
                }

                var go = Instantiate(enemyPrefab.gameObject, level.ConvertLevelPosToWorld(spawn.levelLocation) + spawn.worldOffset, Quaternion.identity, this.transform);
                Enemy enemy = go.GetComponent<Enemy>();
                enemyPool.Add(enemy);
            }
            maxEnemyCount = enemyPool.Count;
        }

        public void SpawnProps(Level level)
        {
            if (level.propSpawnLocationList == null)
            {
                return;
            }

            foreach (Spawn spawn in level.propSpawnLocationList)
            {
                var propPrefab = level.template.props.FirstOrDefault(p => p.name == spawn.name);

                if (propPrefab is null)
                {
                    continue;
                }

                var go = Instantiate(propPrefab.gameObject, level.ConvertLevelPosToWorld(spawn.levelLocation) + spawn.worldOffset, Quaternion.identity, this.transform);
                
                propPool.Add(go);
            }
        }        

        public void SpawnTraps(Level level)
        {
            Debug.Log("Spawn traps pool");
            if (level.trapSpawnLocationList == null)
            {
                return;
            }

            foreach (Spawn spawn in level.trapSpawnLocationList)
            {
                var trapPrefab = level.template.traps.FirstOrDefault(p => p.name == spawn.name);

                if (trapPrefab is null)
                {
                    continue;
                }

                var go = Instantiate(trapPrefab.gameObject, level.ConvertLevelPosToWorld(spawn.levelLocation) + spawn.worldOffset, Quaternion.identity, this.transform);
                
                trapPool.Add(go);
            }
        }                

        public void UpdateEnemiesBehaviour()
        {
            foreach (var enemy in enemyPool)
            {
                enemy.UpdateBehaviour();
            }
        }

        public void Attack()
        {
            foreach (var enemy in enemyPool)
            {
                enemy.Attack();
            }
        }

        public void UpdateEnemiesNextTurn()
        {
            foreach (var enemy in enemyPool)
            {
                enemy.NextTurn();
            }
        }

        public void KillEnemy(Enemy enemy)
        {
            enemyPool.Remove(enemy);
            enemy.gameObject.SetActive(false);
            Destroy(enemy);
        }

        public void ClearEnemies()
        {
            enemyPool.Clear();
            propPool.Clear();
            trapPool.Clear();
            maxEnemyCount = 0;
            for (int k = 0; k < this.transform.childCount; k++)
            {
                this.transform.GetChild(k).gameObject.SetActive(false);
                Destroy(this.transform.GetChild(k).gameObject);
            }
        }
    }
}