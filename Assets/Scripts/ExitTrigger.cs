using UnityEngine;

namespace HackedDesign
{
    public class ExitTrigger : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer lockSprite;
        [SerializeField] private SpriteRenderer unlockSprite;

        void Update()
        {
            lockSprite.gameObject.SetActive(EnemyPool.Instance.EnemyCount != 0);
            unlockSprite.gameObject.SetActive(EnemyPool.Instance.EnemyCount == 0);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player") && EnemyPool.Instance.EnemyCount == 0)
            {
                Game.Instance.NextLevel();
            }
        }
    }
}