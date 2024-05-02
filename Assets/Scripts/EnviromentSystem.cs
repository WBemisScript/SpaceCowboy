using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnviromentSystem : MonoBehaviour
{


    public static Tilemap WallTiles;
    public Tilemap WallTilesReference;
    public Tile BloodTile;
    public Tilemap BloodTileMap;
    public static    EnviromentSystem SHOWNVIRO;
    public static GameObject Blood;
    public GameObject BloodReference;


    void Start()
    {
        SHOWNVIRO = this;
        WallTiles = WallTilesReference;
        Blood = BloodReference;
    }




    public void RENDER_BLOOD(Vector3Int Coords)
    {
        Debug.Log(Coords);
        if (WallTiles.HasTile(Coords))
        {
            if (!WallTiles.HasTile(new Vector3Int(Coords.x, Coords.y + 1, Coords.z)))
            {
                Debug.Log("HasTile");
                BloodTileMap.SetTile(Coords, BloodTile);
            }
          
        }
    }


    void Update()
    {
        
    }
}
