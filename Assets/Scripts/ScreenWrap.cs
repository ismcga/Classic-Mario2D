using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    
    public float screenWidthLimit = 9.5f;

    void Update()
    {
        Vector3 pos = transform.position;

        
        if (pos.x > screenWidthLimit)
        {
            pos.x = -screenWidthLimit;
            transform.position = pos;
        }
        
        else if (pos.x < -screenWidthLimit)
        {
            pos.x = screenWidthLimit;
            transform.position = pos;
        }
    }
}
