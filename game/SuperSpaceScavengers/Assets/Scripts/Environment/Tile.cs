﻿///////////////////////////////////////////////////////////////////////////////////////////////////
//AUTHOR — Travis Moore
//SCRIPT — Tile.cs
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
public enum Neighbor
{
    NORTH,
    EAST,
    SOUTH,
    WEST
};
public enum TileType
{
    TILE,
    WALL,
    EMPTY
};
#endregion

#region EVENTS
//public class EVENT_EXAMPLE : GameEvent
//{
//    public EVENT_EXAMPLE() { }
//}
#endregion

public class Tile : MonoBehaviour
{
    #region FIELDS
    Transform tr;
    [SerializeField]
    GameObject quad;
    MeshCollider quad_mc;
    [SerializeField]
    GameObject space;
    BoxCollider space_bc;
    GameObject wall_top;
    GameObject wall_bot;

    [SerializeField]
    TileType type;
    public TileType Type
    {
        private set { type = value; }
        get { return type; }
    }

    Vector2 position;
    public Vector2 Position
    {
        private set { position = value; }
        get { return position; }
    }
    Dictionary<Neighbor, Tile> neighbors;
    Tile north;
    Tile east;
    Tile south;
    Tile west;
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
        SetTileType(type);
        tr = GetComponent<Transform>();
        quad = tr.GetChild(0).gameObject;
        quad_mc = quad.GetComponent<MeshCollider>();
        space = tr.GetChild(1).gameObject;
        space_bc = space.GetComponent<BoxCollider>();
        wall_top = space.transform.GetChild(0).gameObject;
        wall_bot = space.transform.GetChild(1).gameObject;
        //initial values

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Awake
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        tr = GetComponent<Transform>();
        quad = tr.GetChild(0).gameObject;
        quad_mc = quad.GetComponent<MeshCollider>();
        space = tr.GetChild(1).gameObject;
        space_bc = space.GetComponent<BoxCollider>();
        wall_top = space.transform.GetChild(0).gameObject;
        wall_bot = space.transform.GetChild(1).gameObject;

        neighbors = new Dictionary<Neighbor, Tile> { };
        //SetSubscriptions();
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Start
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        Initialize(type, tr.position);
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
    public void Initialize(TileType _type, Vector3 _position, string _name = "tile")
    {
        SetTileType(_type);
        SetPosition(_position);
        SetName(_name);
    }
    public void Initialize(TileType _type, Vector2 _position, string _name = "tile")
    {
        SetTileType(_type);
        SetPosition(_position);
        SetName(_name);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_direction"></param>
    /// <param name="_tile"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void SetNeighbor(Neighbor _neighbor, Tile _tile)
    {
        neighbors.Add(_neighbor, _tile);
        switch (_neighbor)
        {
            case Neighbor.NORTH:
                north = _tile;
                break;
            case Neighbor.EAST:
                east = _tile;
                break;
            case Neighbor.SOUTH:
                south = _tile;
                break;
            case Neighbor.WEST:
                west = _tile;
                break;
            default:
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_wallPrefab"></param>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void PlaceWalls(GameObject _wallPrefab)
    {
        foreach (KeyValuePair<Neighbor, Tile> _neighbor in neighbors)
        {
            if (_neighbor.Value == null)
            {
                GameObject _wall = Instantiate(_wallPrefab, tr.position, Quaternion.identity, tr);
                Transform _wall_tr = _wall.GetComponent<Transform>();
                _wall_tr.position = tr.position;
                _wall_tr.parent = tr;
                Tile _wall_tile = _wall.GetComponent<Tile>();

                //rotate walls based on direction
                switch (_neighbor.Key)
                {
                    //_wall.gameObject.name = "Wall(" + _neighbor.Key.ToString() + ")";
                    case Neighbor.NORTH:
                        _wall_tile.Initialize(TileType.WALL, new Vector2(0.5f, 0), "wall");
                        break;

                    case Neighbor.EAST:
                        _wall_tile.Initialize(TileType.WALL, new Vector2(0, 0.5f), "wall");
                        break;

                    case Neighbor.SOUTH:
                        _wall_tile.Initialize(TileType.WALL, new Vector2(-0.5f, 0), "wall");
                        break;

                    case Neighbor.WEST:
                        _wall_tile.Initialize(TileType.WALL, new Vector2(0, -0.5f), "wall");

                        break;

                    default:
                        break;
                }

            }
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void DiscoverNeighbors()
    {
        RaycastHit _northHit;
        RaycastHit _eastHit;
        RaycastHit _southHit;
        RaycastHit _westHit;
        Physics.Raycast(tr.position, Vector3.forward, out _northHit, 1f);
        //Debug.DrawRay(tr.position, Vector3.forward, Color.blue, 1f);

        Physics.Raycast(tr.position, Vector3.left, out _westHit, 1f);
        //Debug.DrawRay(tr.position, Vector3.left, Color.red, 1f);

        Physics.Raycast(tr.position, -Vector3.forward, out _southHit, 1f);
        //Debug.DrawRay(tr.position, -Vector3.forward, Color.green, 1f);

        Physics.Raycast(tr.position, -Vector3.left, out _eastHit, 1f);
        //Debug.DrawRay(tr.position, -Vector3.left, Color.yellow, 1f);


        if (_northHit.collider != null)
        {
            SetNeighbor(Neighbor.NORTH, _northHit.collider.gameObject.transform.root.GetComponent<Tile>());
        }
        else
        {
            SetNeighbor(Neighbor.NORTH, null);
        }

        if (_eastHit.collider != null)
        {
            SetNeighbor(Neighbor.EAST, _eastHit.collider.gameObject.transform.root.GetComponent<Tile>());
        }
        else
        {
            SetNeighbor(Neighbor.EAST, null);
        }

        if (_southHit.collider != null)
        {
            SetNeighbor(Neighbor.SOUTH, _southHit.collider.gameObject.transform.root.GetComponent<Tile>());
        }
        else
        {
            SetNeighbor(Neighbor.SOUTH, null);
        }

        if (_westHit.collider != null)
        {
            SetNeighbor(Neighbor.WEST, _westHit.collider.gameObject.transform.root.GetComponent<Tile>());
        }
        else
        {
            SetNeighbor(Neighbor.WEST, null);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    public void PrintNeighbors()
    {
        foreach (KeyValuePair<Neighbor, Tile> _neighbor in neighbors)
        {
            if (_neighbor.Value != null)
            {
                print(_neighbor.Key + " is " + _neighbor.Value.gameObject.name);
            }
            else
            {
                print(_neighbor.Key + " is NULL");
            }
        }
    }
    #endregion

    #region PRIVATE METHODS
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetPosition(Vector3 _position)
    {
        tr.position = _position;
        Position = new Vector2(_position.x, _position.z);
    }
    void SetPosition(Vector2 _position)
    {
        tr.position = new Vector3(_position.x, 0, _position.y);
        Position = _position;
    }
    void SetPosition(float _x, float _y)
    {
        tr.position = new Vector3(_x, 0, _y);
        Position = new Vector2(_x, _y);
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetTileType(TileType _tileType)
    {
        switch (_tileType)
        {
            case TileType.TILE:
                quad.SetActive(true);
                space.SetActive(false);
                break;
            case TileType.WALL:
                quad.SetActive(true);
                space.SetActive(true);
                break;
            case TileType.EMPTY:
                quad.SetActive(false);
                space.SetActive(false);
                break;
            default:
                Debug.LogWarning("Incorrect TileType selected, please correct to either 'TILE, WALL, or EMPTY'.");
                break;
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// function
    /// </summary>
    ///////////////////////////////////////////////////////////////////////////////////////////////
    void SetName(string _name)
    {
        this.name = _name + " (" + Position.x + ", " + Position.y + ")";
    }

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
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        //Keypad 1
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            
        }
        //Keypad 2
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            
        }
        //Keypad 3
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            
        }
        //Keypad 4
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            
        }
        //Keypad 5
        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            
        }
        //Keypad 6
        if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            
        }
    }
    #endregion
}