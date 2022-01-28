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

        while (goalCoordinates.x != this.gameObject.transform.position.x)
        {
            if(goalCoordinates.x > this.gameObject.transform.position.x)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x + 1f,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z
                    );
            }
            else if (goalCoordinates.x < this.gameObject.transform.position.x)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x - 1f,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z
                    );
            }
        }
        while (goalCoordinates.z != this.gameObject.transform.position.z)
        {
            if (goalCoordinates.z > this.gameObject.transform.position.z)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + 1
                    );
            }
            else if (goalCoordinates.z < this.gameObject.transform.position.z)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z - 1
                    );
            }
        }
        while(goalCoordinates.y != this.gameObject.transform.position.y)
        {
            if (goalCoordinates.y > this.gameObject.transform.position.y)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y + 1f,
                    this.gameObject.transform.position.z
                    );
            }
            else if (goalCoordinates.y < this.gameObject.transform.position.y)
            {
                this.gameObject.transform.position = new Vector3(
                    this.gameObject.transform.position.x,
                    this.gameObject.transform.position.y - 1f,
                    this.gameObject.transform.position.z
                    );
            }
        }
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
}
