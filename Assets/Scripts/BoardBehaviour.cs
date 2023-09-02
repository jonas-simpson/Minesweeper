using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// using UnityEngine.EventSystems;

[RequireComponent(typeof(GridLayoutGroup))]
public class BoardBehaviour : MonoBehaviour
{
    public GameObject tilePrefab;
    public int boardSize;

    //private float tileSize;
    private TileBehaviour[,] grid;
    private GridLayoutGroup gridLayoutGroup;
    private RectTransform panel;
    private RectTransform safeArea;

    private void Awake() { }

    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        panel = GetComponent<RectTransform>();
        safeArea = transform.parent.GetComponent<RectTransform>();

        UISafeAreaHandler.onDeviceOrientationChange += ReceiveResizeRequest;

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = boardSize;

        PopulateGrid();
        // Debug.Log(safeArea.gameObject.name);

        //Invoke("ResizeGUI", 0.5f);
        // ResizeGUI();


        //Calculate square tile size based on board size and dimensions
        float tileSize = panel.rect.width / boardSize;
    }

    // Update is called once per frame
    void Update() { }

    private void PopulateGrid()
    {
        grid = new TileBehaviour[boardSize, boardSize];
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var newTile = Instantiate(tilePrefab, Vector3.one, Quaternion.identity);
                newTile.transform.SetParent(transform);
                newTile.transform.localScale = Vector3.one;
                newTile.name = ('(' + x.ToString() + ',' + y.ToString() + ')');
                grid[x, y] = newTile.GetComponent<TileBehaviour>();
            }
        }
    }

    private void ReceiveResizeRequest()
    {
        Debug.Log("Resize request received!");
        gameObject.SetActive(false);
        Invoke("ResizeGUI", 0.25f);
    }

    public void ResizeGUI()
    {
        Debug.Log(
            safeArea.gameObject.name
                + " Width: "
                + safeArea.rect.width
                + " , "
                + safeArea.gameObject.name
                + " Height: "
                + safeArea.rect.height
        );

        float shortest = Mathf.Min(safeArea.rect.width, safeArea.rect.height);
        // panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, shortest);
        // panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, shortest);

        gameObject.SetActive(true);
        Debug.Log("Resize GUI!");

        float tileSize = (shortest / boardSize) * 0.95f;

        gridLayoutGroup.cellSize = new Vector2(tileSize, tileSize);
    }
}
