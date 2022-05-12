using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Siccity.GLTFUtility;

public class Controller : MonoBehaviour
{
    public Camera cam;
    private Vector3 previousPosition;

    public GameObject ground;
    string path;
    void Start()
    {
        path = $"{Application.persistentDataPath}/Files/";
        //ground.GetComponent<MeshRenderer>().material.color = Color.green;
        //addObject("https://storage.googleapis.com/lom/cube.glb");
        
    }

    public void addObject(string meshFile, int objectId)
    {
        string filePath = GetFilePath(meshFile);
        this.HandleFiles(filePath, meshFile, objectId);
    }

    IEnumerator GetFileRequest(string url, Action<UnityWebRequest> callback)
    {
        using(UnityWebRequest req = UnityWebRequest.Get(url))
        {
            req.downloadHandler = new DownloadHandlerFile(GetFilePath(url));
            yield return req.SendWebRequest();
            callback(req);
        }
    }

    string GetFilePath(string url)
    {
        string[] pieces = url.Split('/');
        string filename = pieces[pieces.Length - 1];

        return $"{path}{filename}";
    }

    void HandleFiles(string filePath, string fileUrl, int objectId)
    {
        if (File.Exists(filePath))
            {
                Debug.Log("Found file locally");
                Debug.Log(filePath);
                //numberFileDownloaded += 1;
                GameObject newObj = Importer.LoadFromFile(filePath);
                newObj.name = "object " + objectId.ToString();
                newObj.AddComponent<DragController>();
                newObj.AddComponent<BoxCollider>();
            }
            else
            {
                StartCoroutine(GetFileRequest(fileUrl, (UnityWebRequest req) =>
                {
                    if (req.isNetworkError || req.isHttpError)
                    {
                        Debug.Log("error");
                        Debug.Log($"{req.error}");
                    }
                    else
                    {
                        Debug.Log("Download success: " + filePath);
                        GameObject newObj = Importer.LoadFromFile(filePath);
                        newObj.name = "object " + objectId.ToString();
                        newObj.AddComponent<DragController>();
                        newObj.AddComponent<BoxCollider>();
                    }
                }));
            }
    }

    public void changeGroundColor(string color) {
        Color newColor = Color.white;
        if (color == "Red") {
            newColor = Color.red;
        }
        else if (color == "Yellow"){
            newColor = Color.yellow;
        }
        else if (color == "Green"){
            newColor = Color.green;
        }
        else if (color == "Blue"){
            newColor = Color.blue;
        }
        else if (color == "Black"){
            newColor = Color.black;
        }
        ground.GetComponent<MeshRenderer>().material.color = newColor;
    }
    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }    
        if ( Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = new Vector3();
            cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0), - direction.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));


            // cam.transform.RotateAround(new Vector3(), new Vector3(1, 0, 0), direction.y * 180);
            // cam.transform.RotateAround(new Vector3(), new Vector3(0, 1, 0), - direction.x * 180);

            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
