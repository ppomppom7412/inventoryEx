using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {
    public abstract void Enter(Monster _mt);
    public abstract void Execute(Monster _mt);
    public abstract void Exit(Monster _mt);
}
