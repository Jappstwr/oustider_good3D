using UnityEngine;

public class WalletLogic : MonoBehaviour
{

    public Camera _camera;
    public Camera InspectionCamera;
    public Transform InspectionWallet;
    void Start()
    {

    }


    void Update()
    {

    }

    public void Interacted()
    {
        _camera.GetComponent<Camera>().enabled = false;
        InspectionCamera.GetComponent<Camera> ().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
