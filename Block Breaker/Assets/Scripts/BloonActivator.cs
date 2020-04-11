using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloonActivator : MonoBehaviour
{
    [SerializeField] WhiteBaloon whiteBallon;
    [SerializeField] BlackBaloon blackBallon;
    [SerializeField] Ball ball;

    //States
    private bool WereActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.hasStarted && !WereActivated)
        {
            Instantiate<WhiteBaloon>(whiteBallon, new Vector3(Random.Range(0,16), Random.Range(0,16),0),Quaternion.identity);
            Instantiate<BlackBaloon>(blackBallon, new Vector3(Random.Range(0,16), Random.Range(0,16),0),Quaternion.identity);
            WereActivated = true;
        }
    }
}
