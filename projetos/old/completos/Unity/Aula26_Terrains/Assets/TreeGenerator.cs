using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public int iterations;

    private string lSystemIn, lSystemOut;
    private string grammar;
    private LineRenderer lineRenderer;

    void ExpandLSystem()
    {
        lSystemOut = lSystemIn;
        for(int n = 0; n < iterations; n++)
        {
            (lSystemIn, lSystemOut) = (lSystemOut, lSystemIn);

            lSystemOut = "";
            foreach(char ch in lSystemIn)
            {
                if(ch == 'F')
                    lSystemOut += grammar;
                else
                    lSystemOut += ch;
            }
        }
    }

    void GenerateTree()
    {
        Vector3 pos = Vector3.zero;
        Vector3 dir = Vector3.up;
        Stack<Vector3> stack = new Stack<Vector3>();

        lineRenderer.positionCount = 1;
        foreach(char ch in lSystemOut)
        {
            switch(ch)
            {
                case 'F':
                    pos += dir;
                    break;
                case '+':
                    dir = Quaternion.AngleAxis(-20, Vector3.forward) * dir;
                    break;
                case '-':
                    dir = Quaternion.Euler(0, 0, 20) * dir;
                    break;
                case '[':
                    stack.Push(pos);
                    stack.Push(dir);
                    break;
                case ']':
                    dir = stack.Pop();
                    pos = stack.Pop();
                    break;
            }
            lineRenderer.SetPosition(lineRenderer.positionCount-1, pos);
            lineRenderer.positionCount++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        grammar = "F[-F]F[+F][F]";
        lSystemIn = "F";
        ExpandLSystem();
        GenerateTree();
    }
}
