using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class PointSystemEditorWindow : EditorWindow
{
    [MenuItem("Tools/Point System Editor")]
    public static void Open()
    {
        GetWindow<PointSystemEditorWindow>();
    }

    public Transform Root;

    #region Window drawing & Root initialization
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("Root"));

        if (Root == null)
        {
            EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            if (Root.GetComponent<RootController>() == null)
            {
                if (GUILayout.Button("Set as root"))
                {
                    _thisRootController = Root.gameObject.AddComponent<RootController>();
                    _pointeventSystem = Root.gameObject.AddComponent<PointEventSystem>();
                }
                EditorGUILayout.EndVertical();
                return;
            }
            _thisRootController = Root.gameObject.GetComponent<RootController>();
            _toolbarInt = _thisRootController.toolbarInt;

            PointEditor();

            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }
    #endregion

    #region PointEditor
    private int _toolbarInt = 0;
    private string[] _toolbarStrings = { "Loop", "Ping Pong", "One-way" };
    private Point _firstPoint;
    private RootController _thisRootController;
    private PointEventSystem _pointeventSystem;

    private void PointEditor()
    {
        if (GUILayout.Button("Create Point"))
        {
            CreateAreaPoint();
        }
        if (Root.childCount > 0)
        {
            _firstPoint = Root.GetChild(0).GetComponent<Point>();

            _toolbarInt = GUILayout.Toolbar(_toolbarInt, _toolbarStrings);
        }

        if (Root.childCount != 0)
        {
            switch (_toolbarInt)
            {
                case 0:
                    LoopWaypoints();
                    break;
                case 1:
                    PingPongWaypoints();
                    break;
                case 2:
                    OneWayWaypoints();
                    break;
            }
        }

        if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Point>())
        {
            if (GUILayout.Button("Create Point Before"))
            {
                CreateAreaPointBefore();
            }
            if (GUILayout.Button("Create Point After"))
            {
                CreateAreaPointAfter();
            }
            if (GUILayout.Button("Remove Point"))
            {
                RemoveAreaPoint();
            }
        }
    }
    private void CreateAreaPoint()
    {
        GameObject areaPointObject = new GameObject("Area Point " + Root.childCount, typeof(Point));
        areaPointObject.transform.SetParent(Root.transform, false);

        Point areaPoint = areaPointObject.GetComponent<Point>();
        if (Root.childCount > 1)
        {
            areaPoint.previousPoint = Root.GetChild(Root.childCount - 2).GetComponent<Point>();
            areaPoint.previousPoint.nextPoint = areaPoint;

            areaPoint.transform.position = areaPoint.previousPoint.transform.position;
            areaPoint.transform.forward = areaPoint.previousPoint.transform.forward;
        }
        else
        {
            _firstPoint = areaPointObject.GetComponent<Point>();
        }
        if (_toolbarInt == 0)
        {
            _firstPoint.previousPoint = Root.GetChild(Root.childCount - 1).GetComponent<Point>();
            Root.GetChild(Root.childCount - 1).GetComponent<Point>().nextPoint = _firstPoint;
        }

        Selection.activeGameObject = areaPoint.gameObject;
    }

    private void CreateAreaPointBefore()
    {
        GameObject areaPointObject = new GameObject("Area Point " + Root.childCount, typeof(Point));
        areaPointObject.transform.SetParent(Root.transform, false);

        Point areaPoint = areaPointObject.GetComponent<Point>();
        Point selectedAreaPoint = Selection.activeGameObject.GetComponent<Point>();

        areaPoint.transform.position = selectedAreaPoint.transform.position;
        areaPoint.transform.forward = selectedAreaPoint.transform.forward;

        areaPoint.previousPoint = selectedAreaPoint.previousPoint;
        if (selectedAreaPoint.previousPoint != null)
            selectedAreaPoint.previousPoint.nextPoint = areaPoint;

        areaPoint.nextPoint = selectedAreaPoint;
        selectedAreaPoint.previousPoint = areaPoint;

        areaPoint.transform.SetSiblingIndex(selectedAreaPoint.transform.GetSiblingIndex());

        Selection.activeGameObject = areaPoint.gameObject;
    }

    private void CreateAreaPointAfter()
    {
        GameObject areaPointObject = new GameObject("Area Point " + Root.childCount, typeof(Point));
        areaPointObject.transform.SetParent(Root.transform, false);

        Point areaPoint = areaPointObject.GetComponent<Point>();
        Point selectedAreaPoint = Selection.activeGameObject.GetComponent<Point>();

        areaPoint.transform.position = selectedAreaPoint.transform.position;
        areaPoint.transform.forward = selectedAreaPoint.transform.forward;

        areaPoint.previousPoint = selectedAreaPoint;
        if (selectedAreaPoint.nextPoint == null)
        {
            Root.GetChild(0).GetComponent<Point>().previousPoint = areaPoint;
        }
        else
        {
            areaPoint.nextPoint = selectedAreaPoint.nextPoint;
            selectedAreaPoint.nextPoint.previousPoint = areaPoint;
        }

        selectedAreaPoint.nextPoint = areaPoint;
        areaPoint.previousPoint = selectedAreaPoint;

        areaPoint.transform.SetSiblingIndex(selectedAreaPoint.transform.GetSiblingIndex() + 1);

        Selection.activeGameObject = areaPoint.gameObject;
    }

    private void RemoveAreaPoint()
    {
        Point selectedAreaPoint = Selection.activeGameObject.GetComponent<Point>();

        if (selectedAreaPoint.nextPoint == null)
        {
            selectedAreaPoint.previousPoint.nextPoint = Root.GetChild(0).GetComponent<Point>();
            Root.GetChild(0).GetComponent<Point>().previousPoint = selectedAreaPoint.previousPoint;
        }
        else
        {
            selectedAreaPoint.nextPoint.previousPoint = selectedAreaPoint.previousPoint;

            if (selectedAreaPoint.previousPoint != null)
                selectedAreaPoint.previousPoint.nextPoint = selectedAreaPoint.nextPoint;
        }

        if (selectedAreaPoint.previousPoint != null)
            Selection.activeGameObject = selectedAreaPoint.previousPoint.gameObject;
        else
            Selection.activeGameObject = selectedAreaPoint.nextPoint.gameObject;

        DestroyImmediate(selectedAreaPoint.gameObject);
    }

    private void LoopWaypoints()
    {
        _thisRootController.loop = true;
        _thisRootController.pingpong = false;
        _thisRootController.oneway = false;
        _thisRootController.toolbarInt = 0;

        Root.GetChild(Root.childCount - 1).GetComponent<Point>().nextPoint = _firstPoint;
        _thisRootController.ValidateEditor();
    }

    private void PingPongWaypoints()
    {
        _thisRootController.loop = false;
        _thisRootController.pingpong = true;
        _thisRootController.oneway = false;
        _thisRootController.toolbarInt = 1;

        _firstPoint.previousPoint = null;
        Root.GetChild(Root.childCount - 1).GetComponent<Point>().nextPoint = null;
        _thisRootController.ValidateEditor();
    }

    private void OneWayWaypoints()
    {
        _thisRootController.loop = false;
        _thisRootController.pingpong = false;
        _thisRootController.oneway = true;
        _thisRootController.toolbarInt = 2;

        _firstPoint.previousPoint = null;
        Root.GetChild(Root.childCount - 1).GetComponent<Point>().nextPoint = null;
        _thisRootController.ValidateEditor();
    }
    #endregion
}
