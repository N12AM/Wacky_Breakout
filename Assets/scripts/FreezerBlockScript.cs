using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerBlockScript : Block
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //default point 5
        DefaultScore = ConfigurationUtils.PointsPerFreezerBlock;
        PaddleFreezeEventManager.AddInvoker(this);
        base.Start();
    }

    //overrides for standardBlock , updates score and destroys it 
    protected override void DestroyBlock(Collision2D other)
    {
           
        if (other.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.FreezerBlock);
        //    _statusUpdate.ScoreCount(_defaultScore);
        
             //adding invoker to update score
             OneArgumentEvent.Invoke(DefaultScore);
             PaddleFreezeEvent.Invoke();
             Destroy(gameObject);
        }
    }
}
