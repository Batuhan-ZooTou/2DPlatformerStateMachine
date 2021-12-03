using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    private Animator anim;
    [SerializeField] private GameObject hitParticles;

    public void damage(float amount)
    {
        //Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        Debug.Log(amount +"delt");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
