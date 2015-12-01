using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    CultistSpawner spawner; //As all enemies will need to connect to their respective spawners, this means I'll have to generalize spawner sometime in the future. For now, I only have 4 hours left.

	// Update is called once per frame

    public void connectSpawner(CultistSpawner s)
    {
        spawner = s;
    }

    public override void death()
    {
        spawner.reportDeath();
        base.death();
    }
}
