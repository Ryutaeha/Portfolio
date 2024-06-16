using ServiceInterface;
using UnityEngine;


public class Logger : ILoggerService
{
    #region Methods
    public void Log(string message)
    {
        Debug.Log($"Logged: {message}");
    }
    #endregion

}
