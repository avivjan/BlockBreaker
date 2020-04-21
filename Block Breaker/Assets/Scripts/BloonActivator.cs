using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloonActivator : MonoBehaviour
{
    [SerializeField] WhiteBaloon whiteBallon;
    [SerializeField] BlackBaloon blackBallon;
    [SerializeField] Ball ball;

    private const float MAX_X_POS = 15.5f;
    private const float MIN_X_POS = 0;
    private const float MIN_Y_POS = 0;
    private const float MAX_Y_POS= 11.5f;

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
            Instantiate<WhiteBaloon>(whiteBallon, new Vector3(Random.Range(MIN_X_POS, MAX_X_POS), Random.Range(MIN_Y_POS, MAX_Y_POS),0),Quaternion.identity);
            Instantiate<BlackBaloon>(blackBallon, new Vector3(Random.Range(MIN_X_POS, MAX_X_POS), Random.Range(MIN_Y_POS, MAX_Y_POS), 0), Quaternion.identity);
            WereActivated = true;
        }
    }
}
