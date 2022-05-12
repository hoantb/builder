using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Net;

public class DragController : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZcoord;

    private bool isDraging = false;

    void Start()
    {
        
    }

    // // Update is called once per frame
    void Update()
    {
        if( isDraging) {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    public void updateGameObject() {
        Debug.Log("updateGameObject");
        // Vector3 position    = new Vector3(1, 1, 1);
        // Quaternion rotation = new Quaternion(1, 1, 1, 1);
        // GameObject obj = Instantiate(ground, position, rotation) as GameObject;
        Debug.Log(gameObject.name.Split(' ')[1]);
        // string apiUrl = "http://" + Setting.serverIP + "/api/gameobjects/1";
        // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
        // request.Method = "GET";
        // request.ContentType = "application/json";
        
        // using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        // {
        //     string json = "{\"username\":\"" + ipUsername.text + "\", \"password\": \"" + ipPassword.text + "\"}";
        //     streamWriter.Write(json);
        // }

        // HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // StreamReader reader = new StreamReader(response.GetResponseStream());
        // string jsonResponse = reader.ReadToEnd();
        // Debug.Log(jsonResponse);
    }
    private void OnMouseDown() {
       // Debug.Log("OnMouseDown");
        mZcoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isDraging = true;
    }

    private void OnMouseUp() {
        isDraging = false;
    }

    private void onMouseDrag() {
        //Debug.Log("onMouseDrag");
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private Vector3 GetMouseWorldPos() {
        //Debug.Log("GetMouseWorldPos");
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZcoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
