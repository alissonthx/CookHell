using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
}

public interface IGameController
{
    void Catch();
}

public class Foods : MonoBehaviour, IGameController
{
    void IGameController.Catch()
    {
        Debug.Log("Catch Food");
    }
}
