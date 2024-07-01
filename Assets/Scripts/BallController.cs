using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public float maxPower;
    public Slider powerSlider;
    public Transform cam;
    public TextMeshProUGUI puttsCountLabel;
    public TextMeshProUGUI showLevel;
    public float minHoleTime;
    public bool ballStop;
    static public bool ballInHole;
    static public bool allReady; // zwrocenie czy wszycy gracze gotowi 



    private LineRenderer line;
    private Rigidbody ball;
    private float powerUpTime;
    private float power;
    private int putts;
    private float holeTime;
    private Vector3 lastPosition;
    private int maxPutts;



    void Awake()
    {

        ballInHole = false;
        ballStop = true;

        ball = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        Cursor.visible = false;
        maxPower = 10;

        showLevel.text = (LevelManager.Instance.currentLevel - 2).ToString();
        maxPutts = 15;

    }

    void Update()
    {
        if (Server.Disconnected)
        {
            Server.Disconnected = false;
            DisconnectMessage.Instance.ShowDisconnectMessage();
        }
        // nizej wywloanie funkcji do pobrania ze grcz jest gotow przejsc dalej
        if (Server.Level)
        {
            Server.Level = false;
            holeTime = 0;
            ballInHole = false;
            LevelManager.Instance.CountPutts(putts);
            LevelManager.Instance.CountTotalPutts();
            LevelManager.Instance.currentLevel++;
            TimerCountdown.countdownTime = 90f;
            SceneManager.LoadScene(LevelManager.Instance.currentLevel);
        } 
        if (!ballInHole && (TimerCountdown.countdownTime == 0 || putts == maxPutts))
        {
            putts = maxPutts + 3;
            ballInHole = true;
            Server.Ready(putts);
        }
        if (ball.velocity.magnitude < 0.1f)
        {
            if (Vector3.Distance(transform.position, lastPosition) > 0f && !ballStop)
            {
                // wywolanie funkcji na serwer, ze pilka sie zatrzymal
                ballStop = true;
                Server.BallMove(0, 0, 0, transform.position.x, transform.position.y, transform.position.z);
            }
            if (!ballInHole && Input.GetKeyUp(KeyCode.Space) && putts != maxPutts)
            {
                // wywolanie funkcji przesylajacej na serwer, ze pilka zostala uderzona
                ballStop = false;
                Putt();
                UpdateLinePositions();
            }
            if (!ballInHole && Input.GetKey(KeyCode.Space) && putts != maxPutts)
            {
                PowerUp();
                UpdateLinePositions();
            }
        }
        else
        {
            line.enabled = false;
        }

    }

    private void UpdateLinePositions()
    {
        if (holeTime == 0) { line.enabled = true; }
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position + Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward * power);
    }

    private void Putt()
    {
        lastPosition = transform.position;
        Vector3 force = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * Vector3.forward * maxPower * power;
        ball.AddForce(force, ForceMode.Impulse);
        // funkcja wysylajaca na serwer cam.eulerAngles.y, power i lastPosition.x, last.position.y, lastposition.y
        Server.BallMove(force.x, force.y, force.z, lastPosition.x, lastPosition.y, lastPosition.z);
        power = 0;
        powerSlider.value = 0;
        powerUpTime = 0;
        putts++;
        LevelManager.Instance.CountPutts(putts);
        LevelManager.Instance.CountTotalPutts();
        puttsCountLabel.text = putts.ToString();
    }

    private void PowerUp()
    {
        powerUpTime += Time.deltaTime;
        power = Mathf.PingPong(powerUpTime, 1);
        powerSlider.value = power;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hole")
        {
            CountHoleTime();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hole")
        {
            LeftHole();
        }
    }
    private void CountHoleTime()
    {
        holeTime += Time.deltaTime;
        if (holeTime >= minHoleTime && !ballInHole)
        {
            ballInHole = true; // nizej wywloanie funkcji do pobrania ze grcz jest gotow przejsc dalej
            Server.Ready(putts);
        }
    }

    private void LeftHole()
    {
        holeTime = 0;

    }
    private void OnCollisionEnter(Collision collision)
    {
        ballStop = false;
        if (collision.collider.tag == "Out of bounds")
        {
            transform.position = lastPosition;
            ball.velocity = Vector3.zero;
            ball.angularVelocity = Vector3.zero;
        }
        else if (collision.collider.tag == "Obstacle")
        {
            Server.BallMove(collision.impulse.x, collision.impulse.y, collision.impulse.z, transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
