using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.UI;
using UnityBase.Animations;

public enum ConnectionType
{
    Normal, End, Beggining
}

public class Connection : SerializedMonoBehaviour
{
    private ConnectionData data {
        get {
            return layerData[currentLayer];
        }

        set {
            layerData[currentLayer] = value;
        }
    }
    [SerializeField, OnValueChanged("UpdateRotation")] List<ConnectionData> layerData;
    [SerializeField] Dictionary<ConnectionData, Sprite> dataToSprite;
    public Image sprite;
    [ShowInInspector] Vector2Int pos {
        get {
            if (transform.parent == null) return Vector2Int.zero;
            GridLayoutGroup g = transform.parent.GetComponent<GridLayoutGroup>();

            if (type == ConnectionType.End) return new Vector2Int(8, -transform.GetSiblingIndex());
            if (type == ConnectionType.Beggining) return new Vector2Int(-1, -transform.GetSiblingIndex());
            return new Vector2Int(transform.GetSiblingIndex() % g.constraintCount, -Mathf.FloorToInt(transform.GetSiblingIndex() / g.constraintCount));
        }
    }
    [ShowInInspector, ReadOnly] private static Dictionary<Vector2Int, Connection> posToConnection;
    private bool hover;
    private Quaternion target;
    [PreviouslySerializedAs("smooth")]public float turnSpeed;
    [SerializeField] public ConnectionType type;
    [HideIf("type", ConnectionType.Normal)]
    [SerializeField] private int layer = -1;

    private bool isPowered = false;
    private static int currentLayer;
    [SerializeField] private Color powerColor;
    [SerializeField] private Color backgroundSelectedColor;
    private Color backgroundNormalColor;
    private Color startColor;
    Dictionary<int, Vector2Int> direction;
    private Image image;
    public bool isFinished;
    [SerializeField] private Image connectionDown, connectionRight;

    public void SetLayer(int layer)
    {
        currentLayer = layer;

        foreach (KeyValuePair<Vector2Int, Connection> c in posToConnection)
        {
            c.Value.UpdateRotation();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        backgroundNormalColor = image.color;
        direction = new Dictionary<int, Vector2Int>();
        direction[0] = Vector2Int.up;
        direction[1] = Vector2Int.right;
        direction[2] = Vector2Int.down;
        direction[3] = Vector2Int.left;

        startColor = sprite.color;

        if (posToConnection == null) posToConnection = new Dictionary<Vector2Int, Connection>();
        posToConnection[pos] = this;
        if (type == ConnectionType.Normal)
        {
            for (int i = 0; i < 4; i++)
            {
                layerData[i] = layerData[i].Rotate(Random.Range(0, 4));
            }
        }
        UpdateRotation();
    }

    public void LogicUpdate()
    {
        if (hover)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Rotate(3);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Rotate(1);
            }
        }
    }

    public void VisualUpdate()
    {
        sprite.transform.localRotation = Quaternion.RotateTowards(sprite.transform.localRotation, target, Time.deltaTime * turnSpeed);
        if (type == ConnectionType.Beggining && layer == currentLayer)
        {
            foreach (KeyValuePair<Vector2Int, Connection> p in posToConnection)
            {
                p.Value.isPowered = false;
                //p.Value.connectionDown.gameObject.SetActive(false);
                //p.Value.connectionRight.gameObject.SetActive(false);
            }

            Power();

            foreach (KeyValuePair<Vector2Int, Connection> p in posToConnection)
            {
                if (p.Value.type == ConnectionType.End && p.Value.isPowered && p.Value.layer == currentLayer)
                {
                    bool full = true;
                    foreach (Connection cd in posToConnection.Values)
                    {
                        if (cd.type == ConnectionType.Normal)
                        {
                            bool f = true;

                            for (int i = 0; i < 4; i++)
                            {
                                if (cd.data.GetConnections()[i])
                                {
                                    Connection c;
                                    if (!posToConnection.TryGetValue(cd.pos + direction[i], out c)) continue;
                                    if (!(c.data.GetConnections()[(i + 2) % 4])) f = false;
                                }
                            }

                            Debug.Log(f + " " + cd.pos);

                            if (!f) full = false;
                        }
                    }

                    if (!full) p.Value.isPowered = false;
                }

                if (p.Value.isPowered) p.Value.sprite.color = powerColor;
                else p.Value.sprite.color = p.Value.startColor;

                if (p.Value.type == ConnectionType.Beggining) p.Value.sprite.color = p.Value.powerColor;

                if (p.Value.type == ConnectionType.End && p.Value.layer == currentLayer)
                {
                    p.Value.isFinished = p.Value.isPowered;
                }

                if (p.Value.isFinished)
                {
                    p.Value.sprite.color = p.Value.powerColor;
                }
            }
        }
    }

    public void Power()
    {
        isPowered = true;
        for(int i = 0; i < 4; i++)
        {
            if (data.GetConnections()[i])
            {
                Connection c;
                if (!posToConnection.TryGetValue(pos + direction[i], out c)) continue;
                if (c.data.GetConnections()[(i + 2) % 4])
                {
                    //connectionRight.gameObject.SetActive(i == 1);
                    //connectionDown.gameObject.SetActive(i == 2);
                    if (!c.isPowered && c.type == ConnectionType.Normal || (c.type == ConnectionType.End && c.layer == currentLayer)) c.Power();
                }
            }
        }
    }

    private void Rotate(int ammount)
    {
        data = data.Rotate(ammount);
        target = Quaternion.Euler(0, 0, GetSprite().Value * 90);
        sprite.sprite = GetSprite().Key;
    }

    void UpdateRotation()
    {
        sprite.sprite = GetSprite().Key;
        sprite.transform.rotation = Quaternion.Euler(0, 0, GetSprite().Value * 90);
        target = sprite.transform.rotation;
    }

    private KeyValuePair<Sprite, int> GetSprite()
    {
        foreach(KeyValuePair<ConnectionData, Sprite> pair in dataToSprite)
        {
            for (int i = 0; i < 4; i++)
            {
                bool[] rot1 = pair.Key.GetAllRotations()[i].GetConnections();
                bool[] rot2 = data.GetConnections();
                bool e = true;
                if (rot1[0] != rot2[0]) e = false;
                if (rot1[1] != rot2[1]) e = false;
                if (rot1[2] != rot2[2]) e = false;
                if (rot1[3] != rot2[3]) e = false;

                if (e) return new KeyValuePair<Sprite, int>(pair.Value, i);
            }
        }
        return new KeyValuePair<Sprite, int>(null, 0);
    }

    public void Hover()
    {
        hover = true;
    }

    public void Unhover()
    {
        hover = false;
    }

    [System.Serializable]
    struct ConnectionData
    {
        [BoxGroup]
        [SerializeField] bool up, down, left, right;

        public ConnectionData(bool[] connections)
        {
            up = connections[0];
            right = connections[1];
            down = connections[2];
            left = connections[3];
        }

        public bool[] GetConnections()
        {
            return new bool[]{up, right, down, left};
        }

        public ConnectionData Rotate(int times)
        {
            List<bool> rotated = new List<bool>(GetConnections());

            for(int i = 0; i < times; i++)
            {
                bool first = rotated[0];
                rotated.RemoveAt(0);
                rotated.Add(first);
            }

            return new ConnectionData(rotated.ToArray());
        }

        public ConnectionData[] GetAllRotations()
        {
            ConnectionData[] datas = new ConnectionData[4];

            for (int i = 0; i < 4; i++)
            {
                datas[i] = Rotate(i);
            }

            return datas;
        }
    }
}
