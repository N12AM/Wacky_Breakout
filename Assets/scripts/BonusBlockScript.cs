using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockScript : Block
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //default point 2
        DefaultScore = ConfigurationUtils.PointsPerBonusBlock;
        base.Start();
    }
    
    //overrides for standardBlock , updates score and destroys it 
    protected override void DestroyBlock(Collision2D other)
    {
           
        if (other.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.BonusBlock);
           // _statusUpdate.ScoreCount(_defaultScore);
           
           //adding invoker to update score
           OneArgumentEvent.Invoke(DefaultScore);
            Destroy(gameObject);
        }
    }

}
