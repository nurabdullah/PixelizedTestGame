using UnityEngine;
public class PlayerSwerve : MonoBehaviour
{
    [SerializeField] private float maxXPosition;
    public float swerveSpeed;


    private void Update()
    {
        Swerve();
    }


    private void Swerve()
    {
        float inputHorizontal = SimpleInput.GetAxis("Horizontal");
        Vector3 targetPosition = transform.position + (Vector3.right * inputHorizontal * swerveSpeed * Time.deltaTime);
        targetPosition.x = Mathf.Clamp(targetPosition.x, -maxXPosition, maxXPosition);
        transform.position = targetPosition;
    }

}
