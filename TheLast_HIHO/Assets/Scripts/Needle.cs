using UnityEngine;

public class Needle : MonoBehaviour
{
    [SerializeField] float damage = 1;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
