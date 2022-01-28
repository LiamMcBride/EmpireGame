using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workerScript : MonoBehaviour
{
    private Vector3 goalCoordinates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGoalCoordinates(Vector3 coords)
    {
        goalCoordinates = roundVector(coords);

        this.gameObject.transform.position = roundVector(this.gameObject.transform.position);

        StartCoroutine(moveWorker());

        
    }

    public Vector3 roundVector(Vector3 tor)
    {
        var vec = new Vector3(
            Mathf.Round(tor.x),
            Mathf.Round(tor.y),
            Mathf.Round(tor.z)
            );
        return vec;
    }

    IEnumerator moveWorker()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        if (goalCoordinates.x > this.gameObject.transform.position.x)
        {
            x += .25f;
        }
        else if (goalCoordinates.x < this.gameObject.transform.position.x)
        {
            x -= .25f;
        }

        if (goalCoordinates.y > this.gameObject.transform.position.y)
        {
            y += .25f;
        }
        else if (goalCoordinates.y < this.gameObject.transform.position.y)
        {
            y -= .25f;
        }

        if (goalCoordinates.z > this.gameObject.transform.position.z)
        {
            z += .25f;
        }
        else if (goalCoordinates.z < this.gameObject.transform.position.z)
        {
            z -= .25f;
        }

        this.gameObject.transform.position = new Vector3(
                this.gameObject.transform.position.x + x,
                this.gameObject.transform.position.y + y,
                this.gameObject.transform.position.z + z
            );

        yield return new WaitForSeconds(.1f);
        
        if(this.gameObject.transform.position != goalCoordinates)
        {
            StartCoroutine(moveWorker());
        }
        
    }
}
