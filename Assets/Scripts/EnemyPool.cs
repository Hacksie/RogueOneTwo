using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace HackedDesign
{
    public class EnemyPool : MonoBehaviour
    {
        public List<Enemy> pool = new List<Enemy>();

        public static EnemyPool Instance { get; private set; }
        [SerializeField] private GameObject enemyParent;
        private int maxEnemyCount = 0;

        EnemyPool() => Instance = this;

        public int EnemyCount { get { return pool.Count; }}
        public int MaxEnemyCount { get { return maxEnemyCount; }}

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

                var go = Instantiate(enemyPrefab.gameObject, level.ConvertLevelPosToWorld(spawn.levelLocation) + spawn.worldOffset, Quaternion.identity, enemyParent.transform);
                Enemy enemy = go.GetComponent<Enemy>();
                pool.Add(enemy);
            }
            maxEnemyCount = pool.Count;
        }

        public void UpdateEnemiesBehaviour()
        {
            foreach (var enemy in pool)
            {
                enemy.UpdateBehaviour();
            }
        }

        public void Attack()
        {
            foreach (var enemy in pool)
            {
                enemy.Attack();
            }
        }

        public void UpdateEnemiesNextTurn()
        {
            foreach (var enemy in pool)
            {
                enemy.NextTurn();
            }
        }

        public void KillEnemy(Enemy enemy)
        {
            pool.Remove(enemy);
            enemy.gameObject.SetActive(false);
            Destroy(enemy);
        }

        public void ClearEnemies()
        {
            pool.Clear();
            maxEnemyCount = 0;
            for (int k = 0; k < enemyParent.transform.childCount; k++)
            {
                enemyParent.transform.GetChild(k).gameObject.SetActive(false);
                Destroy(enemyParent.transform.GetChild(k).gameObject);
            }

        }

    }
}