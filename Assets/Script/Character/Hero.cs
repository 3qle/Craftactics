using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    // Start is called before the first frame update
    void Start()
    {
        Chache();
    }

    public override void MoveCharacter(Vector3 destination) =>  StartCoroutine(MoveHero(destination));
    

    private IEnumerator MoveHero(Vector3 destination)
    {
        if (AP > 0)
        {
            AP -= 1;
            _field.SetTileType(this, true);
            while (transform.position != destination)
            {
                MakeSteps(destination);
                yield return new WaitForSeconds(0.1f);
            }

            FinishSteps();
        }
      
    }
    
}
