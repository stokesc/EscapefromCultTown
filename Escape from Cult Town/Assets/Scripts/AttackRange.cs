using UnityEngine;
using System.Collections;

public class AttackRange : MonoBehaviour {


    public GameObject attackEffect; //Should be replaced by an animation in the polish phase.

    string tagOfTarget;
    Attack attack;
    bool entityInRange = false;

	// Use this for initialization
	void Start () 
    {
        attack = GetComponentInParent<Attack>();
        tagOfTarget = attack.getTargetTag();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == tagOfTarget)
        {
            if (!other.GetComponent<Entity>().getIsDead()) //I don't like this, because means that there's an unstated assumption that only Entitites can be attacked. Change when not in game jam.
            {
                attack.addTargetInRange(other);
                entityInRange = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == tagOfTarget)
        {
            attack.removeTargetInRange(other);
            entityInRange = false;
        }
    }

    public bool isEntityInRange()
    {
        return entityInRange;
    }

    public void playAttackEffect(float duration)
    {
         attackEffect.SetActive(true);
         Invoke("stopAttackEffect", duration);

    }

    public void stopAttackEffect()
    {
        attackEffect.SetActive(false);
    }
}
