using UnityEngine;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hpBar; 

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        RotateTowardsCamera();
    }

    void RotateTowardsCamera()
    {
        //Vector3 direction = (mainCamera.transform.position - hpBar.transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(-direction);
        //hpBar.transform.rotation = lookRotation;
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
