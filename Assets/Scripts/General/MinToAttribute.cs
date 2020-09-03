using UnityEngine;
using System.Collections;
 
// Can use it on:
// VECTOR2 / VECTOR2INT
// FLOAT
// you put the attribute above/before the max value!
// then you specify in the MinTo arguments (if the minName template doesn't work)
// the name of the min float variable
public class MinToAttribute : PropertyAttribute
{
    // $ becomes the name of the max property
    // example: [MinTo] float duration; float durationMin;
    public string minName = "$Min";
    public float? max;
    public float min;
 
    public MinToAttribute(string minName = null)
    {
        if (minName != null)
            this.minName = minName;
    }
    public MinToAttribute(float max, string minName = null) : this(0, max, minName) { }
    public MinToAttribute(float min, float max, string minName = null) : this(minName)
    {
        this.max = max;
        this.min = min;
    }
}