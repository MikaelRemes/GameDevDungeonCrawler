﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemyactionscript : MonoBehaviour
{
    [Header("Main stats")]
    public float attackRange = 1f;
    public float attackDamage = 10f;
    public float attacksPerSecond = 1f;
    public AttackType attackType = AttackType.MELEE;
    public GameObject attackProjectile;

    [Header("Visuals")]
    public GameObject attackEffect;

    private float attackCoolDown = 0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if(GetComponent<AIDestinationSetter>() != null) GetComponent<AIDestinationSetter>().target = player.transform;
    }
    
    void Update()
    {
        if (attackCoolDown >= 0) attackCoolDown -= Time.deltaTime;
        if((transform.position - player.transform.position).magnitude < attackRange && attackCoolDown <= 0)
        {
            attackCoolDown = 1f / attacksPerSecond;
            Attack(player);
        }
    }

    void Attack(GameObject target)
    {
        if(target.GetComponent<Playerstats>() != null)
        {
            if(attackType.Equals(AttackType.MELEE))target.GetComponent<Playerstats>().TakeDamage(attackDamage);
            if (attackType.Equals(AttackType.PROJECTILE))
            {
                GameObject projectile = Instantiate(attackProjectile, transform.position, Quaternion.identity);
                projectile.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>());
            }
            if (attackType.Equals(AttackType.MISSILE))
            {
                GameObject projectile = Instantiate(attackProjectile, transform.position, Quaternion.identity);
                projectile.GetComponent<ProjectileScript>().initializeProjectile(target.transform.position - transform.position, gameObject.GetComponent<Collider2D>(), player.transform);
            }
            if (attackType.Equals(AttackType.NECROMANCERBOSS))
            {
                //TODO: use random attack based on "cooldowns" and rng
            }
        }
    }

    public enum AttackType
    {
        MELEE, PROJECTILE, MISSILE, NECROMANCERBOSS
    }
}
