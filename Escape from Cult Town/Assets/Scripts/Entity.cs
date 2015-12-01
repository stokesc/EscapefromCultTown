using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
    public bool debugMode = false;

    public HealthBar healthBar;
    public bool hasSanity;
    public SanityBar sanityBar;

    protected bool sanityRegenActive = true;
    protected bool isDead = false;

    public void damageHealth(float damage)
    {
        if (damage >= 0)
            healthBar.modifyCurrentStatus(-damage);
        else
        {
            Debug.Log("You tried to heal with damage, dummy.");
        }
    }

    public void damageSanity(float damage)
    {
        if (hasSanity)
        {
            if (damage >= 0)
                sanityBar.modifyCurrentStatus(-damage);
            else
            {
                Debug.Log("You tried to heal with damage, dummy.");
            }
        }
    }

    public virtual void death()
    {
        if (debugMode) Debug.Log("Aaaaaargh");
        GameObject.Destroy(gameObject, .5f);
    }

    public bool getIsDead()
    {
        return isDead;
    }

    public bool isSanityRegenActive()
    {
        return sanityRegenActive;
    }

}
