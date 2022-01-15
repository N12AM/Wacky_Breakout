using UnityEngine;
using UnityEngine.Serialization;

public class BlockSetting : MonoBehaviour
{

    [FormerlySerializedAs("standardBlockprefab")] 
    [FormerlySerializedAs("blockprefab")] 
    [SerializeField] private GameObject standardBlockPrefab = null;
    
    [SerializeField] private GameObject bonusBlockPrefab = null;
    [SerializeField] private GameObject freezerBlockPrefab = null;
    [SerializeField] private GameObject speederBlockPrefab = null;
//    [SerializeField] private Sprite[] blockSprites = null;

    private Random _random;

    private Camera _camera;
    
    // Start is called before the first frame update
    void Start()
    {
        var block = Instantiate(standardBlockPrefab);
        var totalDistance = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;
        var colliderWidth = block.GetComponent<BoxCollider2D>().size.x / 1.9f;
        var colliderHeight = block.GetComponent<BoxCollider2D>().size.y;
        var totalBlock = (int) (totalDistance / colliderWidth );
        
        //1 / 5 height of the Screen
        var location = new Vector2(Screen.width , (float) (Screen.height  * 4)/ 5 );
        _camera = Camera.main;
        Destroy(block); 
        CreateBlock(totalBlock , location, colliderWidth);
        
        location.y /= 1.078f;
        CreateBlock(totalBlock , location , colliderWidth);
        
        location.y /= 1.084f;
        CreateBlock(totalBlock , location , colliderWidth);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateBlock(int totalBlocks, Vector2 location , float colliderWidth)
    {
        if (_camera != null)
            location = _camera.ScreenToWorldPoint(location);
        
        float factor = 1.35f;
        var previousPosition = ScreenUtils.ScreenLeft -0.1;
        for (var blocks = 0; blocks < totalBlocks-1; blocks++)
        {
            var probability = GetProbability();
            GameObject newBlock;
            if (probability == 1)
            {
             //   print("probability : 1");

                newBlock = Instantiate(standardBlockPrefab);
                var position = newBlock.transform.position;
                
                previousPosition =  position.x = (float) ( previousPosition + factor );
                position.y = location.y;
                newBlock.transform.position = position;

            }
            else if (probability == 2)
            {
                //    print("probability : 2");
                newBlock = Instantiate(bonusBlockPrefab);
                var position = newBlock.transform.position;
                
                previousPosition =  position.x = (float) ( previousPosition + factor );
                position.y = location.y;
                newBlock.transform.position = position;

            }
            else if (probability == 3)
            {
             //   print("probability : 3");

                newBlock = Instantiate(freezerBlockPrefab);
                var position = newBlock.transform.position;
                
                previousPosition =  position.x = (float) ( previousPosition + factor );
                position.y = location.y;
                newBlock.transform.position = position;

            }
            else if (probability == 4)
            {
                //    print("probability : 4");

                newBlock = Instantiate(speederBlockPrefab);
                var position = newBlock.transform.position;
                
                previousPosition =  position.x = (float) ( previousPosition + factor );
                position.y = location.y;
                newBlock.transform.position = position;

            }
            
            //   var newBlock = Instantiate(standardBlockPrefab);
            //    var ran = Random.Range(0, 3);
            //    newBlock.GetComponent<SpriteRenderer>().sprite = blockSprites[ran];
            // newBlock.transform.position = new Vector2(ScreenUtils.ScreenLeft + 1, location.y);
            // var position = newBlock.transform.position;
            //
            //
            // previousPosition =  position.x = (float) ( previousPosition + factor );
            // position.y = location.y;
            // newBlock.transform.position = position;
                
            if(blocks != 0)
                factor = colliderWidth + 0.05f; 

        }
    }


    private int GetProbability()
    {
        float random = Random.value;

        if (random <= 0.7f)
            return 1;
        if (random <= 0.9f)
            return 2;
        if (random <= 0.95f)
            return 3;
        
        return 4;

    }
    
}
