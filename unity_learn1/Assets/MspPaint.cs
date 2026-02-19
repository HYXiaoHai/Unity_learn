using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MspPaint : MonoBehaviour
{
    public Color paintColor = Color.red;
    public float paintSize = 0.1f;

    public LineRenderer currentLine;
    public Material lineMaterial;

    public List<Vector3>positions = new List<Vector3>();
    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))//绘制开始
        {
            isMouseDown = true;
            GameObject go = new GameObject();
            go.transform.SetParent(transform);
            currentLine = go.AddComponent<LineRenderer>();
            currentLine.material = lineMaterial;
            currentLine.startWidth = paintSize;
            currentLine.startColor = paintColor;
            currentLine.endColor = paintColor;
            currentLine.endWidth = paintSize;

            currentLine.numCornerVertices = 5;
            currentLine.numCapVertices = 5;

            Vector3 position = GetMousePosition();
            AddPosition(position);
            //初始化
        }
        if (isMouseDown)
        {
            Vector3 position = GetMousePosition();
            if(Vector3.Distance(position,lastMousePosition)>0.1f)//优化
            AddPosition(position);
        }
        if (Input.GetMouseButtonUp(0))//绘制结束
        {
            currentLine = null;
            positions.Clear();
            isMouseDown = false;
        }
    }

    void AddPosition(Vector3 position)
    {
        lastMousePosition = position;
        position.x += 0.1f;
        if (position != Vector3.zero)
        {
            positions.Add(position);
        }
        currentLine.positionCount = positions.Count;
        currentLine.SetPositions(positions.ToArray());
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // 击中，返回点
        }
        else
        {
            return Vector3.zero; // 未击中，返回 null
        }
    }


    #region
    public void OnRedColorChanged(bool ison)
    {
        if(ison)
        {
            paintColor = Color.red;
        }
    }
    public void OnBlueColorChanged(bool ison)
    {
        if(ison)
        {
            paintColor = Color.blue;
        }
    }
    public void OnGreenColorChanged(bool ison)
    {
        if(ison)
        {
            paintColor = Color.green;
        }
    }
    public void OnMinColorChanged(bool ison)
    {
        if(ison)
        {
            paintSize = 0.1f;
        }
    }
    public void OnMidelColorChanged(bool ison)
    {
        if(ison)
        {
            paintSize = 0.2f;
        }
    }
    public void OnMaxColorChanged(bool ison)
    {
        if(ison)
        {
            paintSize = 0.4f;
        }
    }

    #endregion

}
