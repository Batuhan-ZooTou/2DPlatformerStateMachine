using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordHitBox : MonoBehaviour
{
    public Player player;
    [SerializeField] private PlayerData playerData;
    private List<IDamageable> detectedDamageable = new List<IDamageable>();
    private void Awake()
    {
    }
    public void CheckMeleeAttack()
    {
        foreach (IDamageable item in detectedDamageable)
        {
            item.damage(playerData.attackDamage[player.PlayerAttackState.attackCounter]);
        }
    }
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddToDetected(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveFromDetected(collision);
    }
}
