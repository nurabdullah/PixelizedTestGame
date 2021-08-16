using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject particalEffect;
    [SerializeField] private GameObject failParticalEffect;
    public Animator playerAnimator;
    private PlayerBrain playerBrain;
    private PlayerMovement _playerMovement;
    private PlayerSwerve _playerSwerve;
    private int _charactersCount;



    private void Start()
    {

        playerBrain = GetComponentInParent<PlayerBrain>();
        _playerSwerve = GetComponentInParent<PlayerSwerve>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
        playerBrain.OnFinish += FinishMethod;
    }

    private void OnDisable()
    {
        playerBrain.OnFinish -= FinishMethod;
    }



    private void FinishMethod()
    {
        _charactersCount = playerBrain.stackedPlayers.Count;
        _playerMovement.movementSpeed = 0;
        _playerSwerve = GetComponentInParent<PlayerSwerve>();
        _playerSwerve.swerveSpeed = 0;
        playerAnimator.SetBool("isFight ", true);

      

        if (_charactersCount >= 10)
        {

            playerAnimator.SetBool("isWin", true);
            Instantiate(particalEffect, new Vector3(-4f, 2f, 172f), Quaternion.identity);
            Instantiate(particalEffect, new Vector3(4f, 2f, 172f), Quaternion.identity);

        }
        if (_charactersCount < 10)
        {
            playerAnimator.SetBool("isFail", true);
            Instantiate(failParticalEffect, new Vector3(1.82f, 1f, 175), Quaternion.identity);
            Instantiate(failParticalEffect, new Vector3(-1.82f, 1f, 175f), Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerBrain.DeletePlayer(this);
        }
    }

}
