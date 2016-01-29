using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This really, really should have been a base class that an EnemyAttack and CharacterAttack could have inherited from. 
public class Attack : MonoBehaviour 
{
    public bool debugMode = false;
    public string targetTag;
    public bool autoAttackOn;
    public float attackDamage;
    public float attackCooldown; //Number of seconds between each attack
    public List<AttackRange> attackRanges = new List<AttackRange>();

    protected bool canAttack = false;
    protected float nextAttack; //Timestamp for when the next attack will occur. 
    List<Entity> entitiesInRange = new List<Entity>();

	// Use this for initialization
	protected void Start () 
    {
        nextAttack = Time.time + attackCooldown;
	}
	
	// Update is called once per frame
	protected void Update () 
    {
        if (debugMode) Debug.Log("Seconds until can attack: " + (nextAttack - Time.time));
        //Debug.Log(entitiesInRange.Count);

        if (Time.time > nextAttack && canAttack == false)
        {
            canAttack = true;
        }

        if (autoAttackOn && canAttack && entitiesInRange.Count != 0)
        {
            attackAllTargetsInRange();

        }
	}

    protected void resetCooldown()
    {
        nextAttack = Time.time + attackCooldown;
        canAttack = false;
    }

    protected void attackAllTargetsInRange(bool allowWithNoTargets = false)
    {
        bool attacked = false; 

        if (debugMode) Debug.Log("Attack!");
        if (entitiesInRange.Count != 0)
        {
            List<Entity> toRemove = new List<Entity>(); //Must be a list, 'cause multiple characters might die from the same attack.
            foreach (Entity entity in entitiesInRange)
            {
                entity.damageHealth(attackDamage);
                if (entity.getIsDead())
                    toRemove.Add(entity);
            }

            if(toRemove.Count != 0)
            {
                foreach(Entity entity in toRemove)
                {
                    entitiesInRange.Remove(entity);
                }
            }

            foreach(AttackRange atkRange in attackRanges)
            {
                if(atkRange.isEntityInRange())
                {
                    atkRange.playAttackEffect(.5f);
                }
            }

            resetCooldown();
            attacked = true;
        }

        if(allowWithNoTargets && !attacked)
        {
            foreach (AttackRange atkRange in attackRanges)
            {
                atkRange.playAttackEffect(.5f);
            }

            resetCooldown();
        }
    }

    public void addTargetInRange(Collider2D other)
    {
         entitiesInRange.Add(other.GetComponent<Entity>());
    }

    public void removeTargetInRange(Collider2D other)
    {
        entitiesInRange.Remove(other.GetComponent<Entity>());

    }

    public string getTargetTag()
    {
        return targetTag;
    }
}
