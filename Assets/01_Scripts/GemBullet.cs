using UnityEngine;

public class GemBullet : MonoBehaviour
{

    public GoldShipAI enemyAI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GoldShipAI enemy = collision.gameObject.GetComponent<GoldShipAI>();
            if (enemy != null)
            {

                enemy.ChangeEnemyState(GoldShipAI.ENEMY_STATE.Stunned);

            }
            Destroy(gameObject);
        }
    }
}
