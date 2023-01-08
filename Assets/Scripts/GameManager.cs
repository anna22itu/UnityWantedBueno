using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    [SerializeField] private FaceImageScript FaceStartObject;
    [SerializeField] private WantedImageScript WantedStartObject;
    [SerializeField] private OthersImageScript OtherStartObject;

    [SerializeField] private GameObject fondo;
    [SerializeField] private GameObject SuccesBackground;
    [SerializeField] private GameObject GameOverBackGround;

    [SerializeField] private Sprite[] imagesFace;
    [SerializeField] private Sprite[] imagesWanted;


    int numOtros = 20;

    private int score = 0;
    [SerializeField] private TextMesh scoreText;

    [SerializeField] private TimerScript myTimer;

    //bool foundWantedPassedLevel = false;

    int level;

    Vector3 fondoPosition;

    private void Start()
    {

        level= 0;

        fondoPosition = fondo.transform.position;

        gameLevel(level);
     
    }

    Vector3 wantedStartPosition;
    Vector3 otherStartPosition;

    WantedImageScript wantedImage;
    FaceImageScript faceImage;
    OthersImageScript othersImage;

    float positionZOthers;

    public void gameLevel(int i)
    {
        //foundWantedPassedLevel = false;
        int id = level;
        //int id = i;

        wantedStartPosition = WantedStartObject.transform.position;
        otherStartPosition = OtherStartObject.transform.position;

        wantedImage = WantedStartObject;
        faceImage = FaceStartObject;

        if (level > 3)
        {
            Succes();
        }

        if (id != 0)
        {
            wantedImage.ChangeSprite(id, imagesWanted[id]);
            faceImage.ChangeSprite(id, imagesFace[id]);
        }
        else 
        {
            positionZOthers = otherStartPosition.z;
        }

        wantedImage.transform.position = wantedStartPosition;

        float positionX = Random.Range(-8, -1);
        float positionY = Random.Range(-4, 4);

        if (positionX == wantedImage.transform.position.x && positionY == wantedImage.transform.position.y) 
        {
            positionX = Random.Range(-8, -1);
            positionY = Random.Range(-4, 4);
        }

        faceImage.transform.position = new Vector3(positionX, positionY, -2);


        Sprite[] otrosFaces = new Sprite[imagesFace.Length - 1];
        int pos = 0;

        for (int j = 0; j < imagesFace.Length; j++)
        {
            if (id != j)
            {
                otrosFaces[pos] = imagesFace[j];
                pos++;
            }
        }

        positionZOthers = positionZOthers - 2;

        for (int f = 0; f < numOtros; f++)
        {
            int idOtros = Random.Range(0, 3);

            othersImage = Instantiate(OtherStartObject) as OthersImageScript;

            othersImage.ChangeSprite(idOtros, otrosFaces[idOtros]);

            float positionXOthers = Random.Range(-8, -1);
            float positionYOthers = Random.Range(-4, 4);


            othersImage.transform.position = new Vector3(positionXOthers, positionYOthers, positionZOthers);

            //if (id == 3) 
            //{
            //    othersImage.transform.localScale = new Vector3(3/20, 3/20,1);
            //}
        }

        if (id == 1)
        {
            OtherStartObject.transform.position = new Vector3(otherStartPosition.x, otherStartPosition.y, 100);
        }

    }

    private void Update()
    {
        if (myTimer.getTimer() == 0)
        {
            GameOver();
        }
        else if (score == 40)
        {
            Succes();
        }
    }

    int spriteIdEncontrado = 10;
    public void checkIdFaceClick(int respuesta)
    {
        this.spriteIdEncontrado = respuesta;
        foundWantedImageFace();
    }

    float fondoPositionZ;
    public void foundWantedImageFace()
    {
        if (spriteIdEncontrado == wantedImage.spriteId) 
        {
            score = score + 10;
            scoreText.text = "Score:" + score;
            //foundWantedPassedLevel = true;
            Final();
        }
    }

    public void Final() 
    {
        if (score == 40)  // Hemos superado todos los niveles
        {
            Succes();
        }
        else if (score < 40 && myTimer.getTimer() == 0)   //Hemos perdido
        {
            GameOver();
        }
        else
        {
            numOtros = numOtros + 5;
            if (level == 0)
            {
                fondoPositionZ = fondoPosition.z - 2;
            }
            else
            {
                fondoPositionZ = fondoPositionZ - 2;
            }
            fondo.transform.position = new Vector3(fondoPosition.x, fondoPosition.y, fondoPositionZ);
            gameLevel(level++);
        }
    }

    public void Succes()
    {
        SuccesBackground.transform.position = new Vector3(0, 0, -10);
    }

    public void GameOver() 
    {
        GameOverBackGround.transform.position = new Vector3(0, 0, -10);
    }


}