using UnityEngine;

public class MiniMap : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform player;
    void LateUpdate()
    {
        if(player != null)
        {
            Vector3 newPos = player.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
        

    }
}
