using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trade;
using TMPro;

public class BoatScript : MonoBehaviour
{
    public float speed;
    public TradeController tc;
    
    private float step;
    private PortScript currentPort, targetPort; 
    private Vector3 lastPos, nextPos;
    private int funds = 300;
    private List<Good> goods = new List<Good>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] ports = GameObject.FindGameObjectsWithTag("Port");

        int startPortIndex = Random.Range(0, ports.Length);

        GameObject startPort = ports[startPortIndex];

        transform.position = startPort.transform.position;

        currentPort = startPort.GetComponent<PortScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPort == null)
        {
            if (targetPort == null)
            {
                FindNextMarket();

                lastPos = transform.position;
                nextPos = targetPort.gameObject.transform.position;
            } 

            step += Time.deltaTime * speed;

            if (step >= 1)
            {
                transform.position = nextPos;
                currentPort = targetPort;
            } else
            {
                transform.position = ((lastPos * (1 - step)) + (nextPos * step));
            }          
        }
    }

    void TradeWithPort()
    {
        
    }

    void FindNextMarket ()
    {
        
    }
}