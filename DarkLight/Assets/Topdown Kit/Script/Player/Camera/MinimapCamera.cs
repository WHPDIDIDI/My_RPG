/// <summary>
/// Minimap camera.
/// This script use to control minimap camera
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MinimapCamera : MonoBehaviour {
    public Button addButton;
     public Button sumButton;
    public static int zoomLevel; //zoom level
    public static MinimapCamera Instance; //declare this to global script


    //Private variable
    private int zoomCurrent;

    [HideInInspector]
    public Transform Target;

    void Start()
    {
        //Setting Default Value
        zoomLevel = 3;
        zoomCurrent = zoomLevel;
        Instance = this;
        GameObject hero;
        hero = GameObject.FindGameObjectWithTag("Player");
        Target = hero.GetComponent<Transform>();
        addButton.onClick.AddListener(()=>CameraSsize("Add"));
        sumButton.onClick.AddListener(() => CameraSsize("Sub"));
    }

    // Update is called once per frame
    void Update() {

        //Follow player
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        ZoomManage();

    }

    void ZoomManage()
    {
        //Check zoom limit
        if (zoomLevel < 0)
        {
            zoomLevel = 0;
        } else if (zoomLevel > 4)
        {
            zoomLevel = 4;
        }
    }

    public void ZoomUpdate()
    {
        if (zoomLevel < zoomCurrent)
        {
            this.GetComponent<Camera>().orthographicSize += 3;
            zoomCurrent = zoomLevel;
        } else

        if (zoomLevel > zoomCurrent)
        {
            this.GetComponent<Camera>().orthographicSize -= 3;
            zoomCurrent = zoomLevel;
        }
    }
    public void CameraSsize(string s)
    {
        if (s=="Add")
        {
            GetComponent<Camera>().orthographicSize += 1f;
        }
        if (s == "Sub")
        {
            GetComponent<Camera>().orthographicSize -= 1f;
        }
    }
}
