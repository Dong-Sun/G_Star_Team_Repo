using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
//[RequireComponent(typeof(PlayerInteraction))]

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] public float Character_Height = 1f;
    [HideInInspector] public int Block_Size = 1;
    [HideInInspector] public float Floor_Height = 0.5f;
    

    public static PlayerManager Player_Manager_Instance;
    // Start is called before the first frame update
    void Start()
    {
        SingleTon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SingleTon()
    {
        if (Player_Manager_Instance == null)
        {
            Player_Manager_Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //private void Find_Player_Height()
    //{
    //    MeshFilter mf = this.transform.GetChild(0).GetComponent<MeshFilter>();
    //    Vector3[] vertices = mf.mesh.vertices;
    //    float least_y=Mathf.Infinity;
    //    foreach (var vertice in vertices)
    //    {

    //        Vector3 pos = transform.TransformPoint(vertice);

    //        if (pos.y > Character_Height)
    //        {
    //            Character_Height = pos.y;
    //        }
    //        if (pos.y < least_y)
    //        {
    //            least_y = pos.y;
    //        }
    //    }
    //        Character_Height -= least_y;
    //}
}