///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — TileGeneration.cs
///////////////////////////////////////////////////////////////////////////////////////////////////

#pragma warning disable 0169
#pragma warning disable 0649
#pragma warning disable 0108
#pragma warning disable 0414

using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

#region ENUMS
//public enum EnumStatus
//{
//	
//};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class TileGeneration : MonoBehaviour
{
    #region FIELDS
    [Header("TILES")]
    [SerializeField]
    GameObject tile;
    [SerializeField]
    int amount;
    [SerializeField]
    int max = 200;
    [SerializeField]
    int triesPerTile = 3;
    [SerializeField]
    Transform startingTile;
    [SerializeField]
    Vector3 startRotation = new Vector3(90f, 0f, 0f);
    [SerializeField]
    float generationDelay = 0.2f;
    Vector3 nextPosition;
    Direction previousDirection;

    List<Tile> tiles;
    #endregion

    #region INITIALIZATION
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnValidate
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnValidate()
    {
        //refs


        //initial values
        nextPosition = startingTile.transform.position;
        tiles = new List<Tile> { };
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        StartCoroutine(GenerateTiles(nextPosition, amount));
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// SetSubscriptions
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetSubscriptions()
    {
        //Events.instance.AddListener<event>(function);
    }
    #endregion

    #region PUBLIC METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////

    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator GenerateTiles(Vector3 _position, int _tries)
    {
        if (max > 0)
        {
            if (tile != null)
            {
                for (int i = 0; i < _tries; ++i)
                {
                    //create the tile
                    GameObject _tile = CreateTile(_position);

                    //check if it is a duplicate
                    if (DuplicateCheck(_tile.transform.position))
                    {
                        //duplicate, destroy it
                        Destroy(_tile);
                    }
                    else
                    {
                        --max;
                        tiles.Add(_tile.GetComponent<Tile>());
                        StartCoroutine(GenerateTiles(_tile.transform.position, triesPerTile));
                    }

                    _position = UpdateNextPosition(_position);

                    yield return new WaitForSeconds(generationDelay);
                }
            }
        }
        else
        {
            //Debug.Log("max reached!\ntiles.Count = " + tiles.Count);
            StopAllCoroutines();
            UpdateTileNeighbors();
            CreatePerimeterWalls();
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    Vector3 UpdateNextPosition(Vector3 _position)
    {
        Vector3 _shift = ChooseRandomDirection(Random.Range(0, 3));
        return _position + _shift;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    Vector3 ChooseRandomDirection(int _direction)
    {
        switch (_direction)
        {
            case 0:
                return new Vector3(0, 0, 1);

            case 1:
                return new Vector3(1, 0, 0);

            case 2:
                return new Vector3(0, 0, -1);

            case 3:
                return new Vector3(-1, 0, 0);

            default:
                Debug.LogError("Random.Range(0,3) somehow returned a number out of range: " + _direction);
                return Vector3.zero;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    bool DuplicateCheck(Vector3 _pos)
    {
        if (tiles.Count > 0)
        {
            foreach (Tile _tile in tiles)
            {
                if (_tile.transform.position == _pos)
                    return true;
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    GameObject CreateTile(Vector3 _position)
    {
        //create the tile at _position, rotate, initialize, then return it
        GameObject _tile = Instantiate(tile, _position, Quaternion.identity);
        _tile.GetComponent<Tile>().Initialize(TileType.TILE, _position);
        //_tile.GetComponent<Transform>().rotation = Quaternion.Euler(startRotation);
        return _tile;
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTileNeighbors()
    {
        foreach (Tile _tile in tiles)
        {
            _tile.DiscoverNeighbors();
        }
        //foreach (Tile _tile in tiles)
        //{
        //    _tile.PrintNeighbors();
        //}
    }
    void CreatePerimeterWalls()
    {
        foreach (Tile _tile in tiles)
        {
            _tile.PlaceWalls(tile);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    #endregion

    #region ONDESTORY
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// OnDestroy
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void OnDestroy()
    {
        //remove listeners
        //Events.instance.RemoveListener<>();
    }
    #endregion

    #region TESTING
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// UpdateTesting
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateTesting()
    {
        //Keypad 0
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {

        }
        //Keypad 2
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {

        }
        //Keypad 3
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        //Keypad 4
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
        //Keypad 5
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {

        }
        //Keypad 6
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
    }
    #endregion
}