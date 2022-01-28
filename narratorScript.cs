using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class narratorScript : MonoBehaviour
{
    private Camera mainCamera;
    private Grid grid;
    public GameObject cursor;
    private Vector3 pastPosition = new Vector3(0,0,0);
    public GameObject house;
    public GameObject hotel;
    public GameObject duplex;
    public GameObject brushType;
    public Material selectedMaterial;
    public Material normalMaterial;
    private int currentTool = 1;
    private int zoom;

    private ArrayList selectedBuildings = new ArrayList();
    private ArrayList selectedWorkers = new ArrayList();
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = this.GetComponent<Camera>();
        grid = FindObjectOfType<Grid>();
        brushType = house;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = 1;
            selectedBuildings = clearSelected(selectedBuildings);
            brushType = house;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            brushType = hotel;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            brushType = duplex;
        }

        if (currentTool == 1)
        {   


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                cursor.SetActive(false);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "ground")
                {
                    placeObject(hit.point);
                }
            }
            else
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "ground")
                {
                    placeCursor(hit.point);
                }
                else
                {
                    //cursor.SetActive(false);
                }
            }
        }
        else if (currentTool == 2)
        {
            cursor.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.gameObject.tag == "building")
                    {
                        hit.transform.gameObject.GetComponent<Renderer>().material = selectedMaterial;
                        selectedBuildings.Add(hit.transform.gameObject);
                        hit.transform.gameObject.tag = "selectedBuilding";
                    }
                    else if (hit.transform.gameObject.tag == "selectedBuilding")
                    {
                        //Object.Destroy(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<Renderer>().material = normalMaterial;
                        selectedBuildings.Remove(hit.transform.gameObject);
                        hit.transform.gameObject.tag = "building";
                    }
                    else if(hit.transform.gameObject.tag == "worker")
                    {
                        selectedBuildings = clearSelected(selectedBuildings);
                        selectedWorkers.Add(hit.transform.gameObject.GetComponent<workerScript>());
                    }
                    else
                    {
                        if(selectedBuildings.Count > 0)
                        {
                            selectedBuildings = clearSelected(selectedBuildings);
                        }
                        else if(selectedWorkers.Count > 0)
                        {
                            foreach(workerScript worker in selectedWorkers)
                            {
                                worker.setGoalCoordinates(hit.point);
                            }
                        }
                        
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                foreach(GameObject building in selectedBuildings)
                {
                    Object.Destroy(building);
                }
                selectedBuildings.Clear();
            }
        }



        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mainCamera.orthographicSize -= 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mainCamera.orthographicSize += 1;
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x,
                mainCamera.transform.position.y + (50 * Time.deltaTime),
                mainCamera.transform.position.z
                );
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x - (30 * Time.deltaTime),
                mainCamera.transform.position.y,
                mainCamera.transform.position.z + (30 * Time.deltaTime)
                );
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x,
                mainCamera.transform.position.y - (50 * Time.deltaTime),
                mainCamera.transform.position.z
                );
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.position = new Vector3(
                mainCamera.transform.position.x + (30 * Time.deltaTime),
                mainCamera.transform.position.y,
                mainCamera.transform.position.z - (30 * Time.deltaTime)
                );
        }
    }

    void placeObject(Vector3 point)
    {
        var finalPosition = grid.GetNearestPointOnGrid(point);

        if(brushType != house)
        {
            finalPosition = new Vector3(
                finalPosition.x - .5f,
                finalPosition.y,
                finalPosition.z
                );
        }

        Instantiate(brushType, finalPosition, Quaternion.identity);
    }

    void placeCursor(Vector3 point)
    {
        var finalPosition = grid.GetNearestPointOnGrid(point);

        if (pastPosition != finalPosition)
        {

            if (brushType != house)
            {
                finalPosition = new Vector3(
                    finalPosition.x - .5f,
                    finalPosition.y,
                    finalPosition.z
                    );
            }
            cursor.SetActive(true);
            cursor.transform.position = finalPosition;
        }
        
    }

    ArrayList clearSelected(ArrayList objects)
    {
        foreach(GameObject obj in objects)
        {
            obj.GetComponent<Renderer>().material = normalMaterial;
            obj.tag = "building";
        }
        objects.Clear();
        return objects;
    }


}
