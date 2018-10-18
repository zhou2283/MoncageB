using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPositionGroupManager : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public MarkPositionBase CheckForCameraSnapTarget(float x, float y)
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                if (isMarkInRange(x, y, child.GetComponent<MarkPositionBase>()))
                {
                    return child.GetComponent<MarkPositionBase>();
                }
            }
        }
        return null;
    }

    bool isMarkInRange(float x, float y, MarkPositionBase markPositionBase)
    {
        //no sub, treat as a point
        if (!markPositionBase.hasSubMarkPosition)
        {
            var _xDist = Mathf.Abs(x - markPositionBase.GetRotation().x);
            var _yDist = Mathf.Abs(y - markPositionBase.GetRotation().y);
            var _Dist = Mathf.Sqrt(_xDist * _xDist + _yDist * _yDist);
            if (_Dist < markPositionBase.snapDist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        //has sub, treat as a line
        Vector2 p1 = markPositionBase.GetRotation();
        Vector2 p2 = markPositionBase.subMarkPositionRotation;
        if (pointToLine(p1, p2, new Vector2(x, y)) < markPositionBase.snapDist)
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }
    
    
    private float pointToLine(Vector2 point1, Vector2 point2, Vector2 position)//point1和point2为线的两个端点
    {
        float space = 0;
        float a, b, c;
        a = Vector2.Distance(point1,point2);// 线段的长度      
        b = Vector2.Distance(point1, position);// position到点point1的距离      
        c = Vector2.Distance(point2, position);// position到point2点的距离 
        if (c <= 0.00001f || b <= 0.00001f)
        {
            space = 0;
            return space;
        }
        if (a <= 0.00001f)
        {
            space = b;
            return space;
        }
        if (c * c >= a * a + b * b)
        {
            space = b;
            return space;
        }
        if (b * b >= a * a + c * c)
        {
            space = c;
            return space;
        }
        float p = (a + b + c) / 2;// 半周长      
        float s = Mathf.Sqrt(p * (p - a) * (p - b) * (p - c));// 海伦公式求面积      
        space = 2 * s / a;// 返回点到线的距离（利用三角形面积公式求高）      
        return space;
    }
}
