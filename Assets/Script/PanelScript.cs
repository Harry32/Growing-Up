using UnityEngine;

public class PanelScript : MonoBehaviour
{
    private float time;
    private float speed;
    private bool showPanel;
    private bool hidePanel;
    [SerializeField]
    private AnimationCurve movementCurve;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        showPanel = false;
        hidePanel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(showPanel && transform.position.y > 550)
        {
            speed = movementCurve.Evaluate(time);
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - speed);
        }
    }

    [ContextMenu("Show Panel")]
    public void ShowPanel()
    {
        showPanel = true;
        hidePanel = false;
    }

    [ContextMenu("Hide Panel")]
    public void HidePanel()
    {
        showPanel = false;
        hidePanel = true;
    }
}