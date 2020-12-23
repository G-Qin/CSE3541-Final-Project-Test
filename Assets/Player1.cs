using UnityEngine;

public class Player1 : MonoBehaviour
{
    // Start is called before the first frame update

    //define velocity
    private Vector3 v=new Vector3((float)0,(float)0,(float)0);

    //define gravity
    private Vector3 g= new Vector3((float)0,(float)-9.8,(float)0);

    //define a velocity goes up
    private Vector3 jumpVelocity= new Vector3(0f, 3f, 0f);

    private Vector3 d=new Vector3((float)0,(float)1,(float)0);

    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 camLeft;
    private Vector3 camBack;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject cam = GameObject.Find("Main Camera");
        camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        camLeft = cam.transform.right * -1f;
        camLeft.y = 0;
        camLeft.Normalize();

        camBack = cam.transform.forward * -1f;
        camBack.y = 0;
        camBack.Normalize();
        //use arrow to go forward/back, rotate to right/left
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(camForward * Time.deltaTime * 5, Space.Self);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(camBack * Time.deltaTime * 5, Space.Self);
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(camLeft * Time.deltaTime * 5, Space.Self);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(camRight * Time.deltaTime * 5, Space.Self);
        }
        //if player hold space key
        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.8f)
        {
            v = v + jumpVelocity;
        }
        //if player doesn't hold space key and is falling above the groud
        else if(this.transform.position.y > 0.75f){
            v = v + Time.deltaTime * g;
        }
        //if player touch the groud, reset every thing
        if(this.transform.position.y < 0.5f)
        {
            Vector3 p=new Vector3(this.transform.position.x, 0.5f, this.transform.position.z);
            this.transform.position=p;
            v=new Vector3(0f, 0f, 0f);
        }
        this.transform.position = this.transform.position + Time.deltaTime * v;
        rabbitCollision();
        green_ghostCollision();
        blue_slimeCollision();
        pillarRightCollision();
        pillarLeftCollision();
        frontWallCollision();
        leftWallCollision();
        rightWallCollision();
        backWallCollision();
    }
    public void rabbitCollision()
    {
        Vector3 cylinderPosition= GameObject.Find("rabbit").transform.position;
        float radius = 2f;
        Vector3 pHat=this.transform.position-cylinderPosition;
        float distance=Mathf.Sqrt(Mathf.Pow(pHat.magnitude,2)-Mathf.Pow((Vector3.Dot(pHat,d)),2));
        if(distance-radius<0)
        {
            this.transform.position=this.transform.position-(distance-radius)*(pHat-Vector3.Dot(pHat,d)*d)/(pHat-(Vector3.Dot(pHat,d)*d)).magnitude;
        }
    }

    public void green_ghostCollision()
    {
        Vector3 cylinderPosition= GameObject.Find("green_ghost").transform.position;
        float radius = 1.5f;
        Vector3 pHat=this.transform.position-cylinderPosition;
        float distance=Mathf.Sqrt(Mathf.Pow(pHat.magnitude,2)-Mathf.Pow((Vector3.Dot(pHat,d)),2));
        if(distance-radius<0)
        {
            this.transform.position=this.transform.position-(distance-radius)*(pHat-Vector3.Dot(pHat,d)*d)/(pHat-(Vector3.Dot(pHat,d)*d)).magnitude;
        }
    }
 
    public void blue_slimeCollision()
    {
        Vector3 cylinderPosition= GameObject.Find("blue_slime").transform.position;
        float radius = 1.5f;
        Vector3 pHat=this.transform.position-cylinderPosition;
        float distance=Mathf.Sqrt(Mathf.Pow(pHat.magnitude,2)-Mathf.Pow((Vector3.Dot(pHat,d)),2));
        if(distance-radius<0)
        {
            this.transform.position=this.transform.position-(distance-radius)*(pHat-Vector3.Dot(pHat,d)*d)/(pHat-(Vector3.Dot(pHat,d)*d)).magnitude;
        }
    }
    
    public void pillarLeftCollision()
    {
        Vector3 doorPosition=GameObject.Find("pillar_left").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+1.5f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-2f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+2f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-2f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
    public void pillarRightCollision()
    {
        Vector3 doorPosition=GameObject.Find("pillar_right").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+1.5f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-2f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+2f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-2f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
        public void frontWallCollision()
    {
        Vector3 doorPosition=GameObject.Find("front_wall").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+10f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-20f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+2f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-2f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
            public void leftWallCollision()
    {
        Vector3 doorPosition=GameObject.Find("left_wall").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+2f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-2f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+3f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-18f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
    public void rightWallCollision()
    {
        Vector3 doorPosition=GameObject.Find("right_wall").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+2f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-2f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+5f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-15f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
    public void backWallCollision()
    {
        Vector3 doorPosition=GameObject.Find("back_Wall").transform.position;
        //define normal for each plane (top view)
        Vector3 rightN=new Vector3((float)1,(float)0,(float)0);
        Vector3 leftN=new Vector3((float)-1,(float)0,(float)0);
        Vector3 bottomN=new Vector3((float)0,(float)0,(float)1);
        Vector3 topN=new Vector3((float)0,(float)0,(float)-1);
        //find center for each plane
        Vector3 rightCenter=new Vector3(doorPosition.x+20f,doorPosition.y,doorPosition.z);
        Vector3 leftCenter=new Vector3(doorPosition.x-5f,doorPosition.y,doorPosition.z);
        Vector3 bottomCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z+2f);
        Vector3 topCenter=new Vector3(doorPosition.x,doorPosition.y,doorPosition.z-2f);
        //check if position is inside of the plane
        bool inRight=Vector3.Dot((this.transform.position-rightCenter),rightN)<=0;
        bool inLeft=Vector3.Dot((this.transform.position-leftCenter),leftN)<=0;
        bool inBottom=Vector3.Dot((this.transform.position-bottomCenter),bottomN)<=0;
        bool inTop=Vector3.Dot((this.transform.position-topCenter),topN)<=0;
        //if position in not outside of every plane at the same time, then position need to fix
        if(inBottom&&inLeft&&inRight&&inTop)
        {
            //fine distance from the position to each plane
            float rightDis=Vector3.Dot(this.transform.position-rightCenter,rightN);
            float leftDis=Vector3.Dot(this.transform.position-leftCenter,leftN);
            float bottomDis=Vector3.Dot(this.transform.position-bottomCenter,bottomN);
            float topDis=Vector3.Dot(this.transform.position-topCenter,topN);
            //if position is closed to right
            if(rightDis>=leftDis&&rightDis>=topDis&&rightDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x-rightDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to left
            else if(leftDis>=rightDis&&leftDis>=topDis&&leftDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x+leftDis, this.transform.position.y,this.transform.position.z);
            }
            //if position is closed to top
            else if(topDis>=leftDis&&topDis>=rightDis&&topDis>=bottomDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z+topDis);
            }
            //if position is closed to bottom
            else if(bottomDis>=leftDis&&bottomDis>=topDis&&bottomDis>=rightDis)
            {
                this.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z-bottomDis);
            }
        }
    }
}
