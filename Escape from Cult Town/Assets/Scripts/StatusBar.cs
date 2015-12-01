using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StatusBar : MonoBehaviour {

    public bool debugMode = false;
    public Image StatusBarFill;
    public float maxStatus;
    protected float currentStatus;

    bool deathReported = false;

    protected Entity entityOwner;
	// Use this for initialization
    protected void Awake()
    {
        entityOwner = gameObject.GetComponent<Entity>();
    }

	protected void Start () 
    {
        StatusBarFill.type = Image.Type.Filled;
        StatusBarFill.fillMethod = Image.FillMethod.Horizontal;
        StatusBarFill.fillOrigin = (int)Image.OriginHorizontal.Left;

        currentStatus = maxStatus;
	}
	
	// Update is called once per frame
    protected void Update()
    {
        StatusBarFill.fillAmount = currentStatus / maxStatus;
        if (!deathReported && currentStatus <= 0 && !entityOwner.getIsDead())
        {
            entityOwner.death();
            deathReported = true;
        }
    }

    public void modifyCurrentStatus(float delta)
    {
        //if (debugMode) Debug.Log("Ouch! " + delta + " damage!");
        if (debugMode) Debug.Log("delta: " + delta);
        if (!(currentStatus + delta > maxStatus))
            currentStatus += delta;
        else
            currentStatus = maxStatus;
    }
}
