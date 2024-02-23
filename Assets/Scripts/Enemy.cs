using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Player player;
    public Animator anim;
    public float attackDistance;
    public int damage;
    public int maxHealth;
    public int currentHp;

    private bool isAttacking;
    private bool isDead;

    private void Update()
    {
        if (isDead)
            return;

        if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            agent.isStopped = true;

            if (!isAttacking)
                Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            anim.SetBool("Running", true);
        }
    }

    public void TakeDamage(int damageToTake)
    {
        currentHp -= damageToTake;

        if (currentHp <= 0)
        {
            isDead = true;
            agent.isStopped = true;
            anim.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;
        }

    }

    private void Attack()
    {
        isAttacking = true;
        anim.SetBool("Running", false);
        anim.SetTrigger("Attack");

        Invoke("TryDamage", 1.3f);
        Invoke("DisableIsAttacking", 2.66f);
    }

    private void TryDamage()
    {
        if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
            player.TakeDamage(damage);
    }

    private void DisableIsAttacking()
    {
        isAttacking = false;
    }
}
