using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public int Id { get; protected set; }
    public Model()
    {

    }

    public Model(int id)
    {
        Id = id;
    }
}
