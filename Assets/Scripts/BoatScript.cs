using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trade;
using TMPro;

public class BoatScript : MonoBehaviour
{
    public float speed;
    public TradeController tradeController;
    
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
            } 

            step += Time.deltaTime * speed;
			Debug.Log(step);

            if (step >= 1)
            {
                transform.position = nextPos;
                currentPort = targetPort;
				targetPort = null;
				step = 1;
            } else
            {
                transform.position = ((lastPos * (1 - step)) + (nextPos * step));
            }          
        } else {
			TradeWithPort();
		}
    }

    void TradeWithPort()
    {
        currentPort = null;
    }

    void FindNextMarket ()
    {
			step = 0;
			GameObject[] ports = GameObject.FindGameObjectsWithTag("Port");

			int index = Random.Range(0, ports.Length);
			
			targetPort = ports[index].GetComponent<PortScript>();

			lastPos = transform.position;
			nextPos = targetPort.gameObject.transform.position;
    }
}