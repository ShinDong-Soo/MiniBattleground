using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LockCursor(true);
    }


    public void LockCursor(bool shouldLock)
    {
        if (shouldLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    public void OnInventoryOpened()
    {
        LockCursor(false);
    }


    public void OnInventoryClosed()
    {
        LockCursor(true);
    }
}
