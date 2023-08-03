using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    [Header("状态与速度")]
    public bool squatzt = false;//下蹲状态
    public bool ground = false;//地面状态
    public bool sky = false;//浮空状态
    public bool coverhead = false;//盖头状态
    public bool hanging = false;
    public float nowxspeed;//当前x速
    public float nowyspeed;//当前y速
    [Header("环境参数")]
    public LayerMask DiBan;//地板
    float jumpforce = 8.2f;//跳跃力量
    float jumpforcemore = 35f;//额外跳跃力量
    float jumptimemore = 0.3f;//额外跳跃时间
    float squatjumpforcemore = 2.5f;//蹲跳额外力量
    float hungjumpforce = 15f;//悬挂跳跃力量
    float jumptime;//跳跃时长
    float hangtime;//防多次悬挂时间
    float groundspeed = 8;//地面速度
    float squatspeed = 4;//蹲走速度
    float skyspeed = 6;//天空速度
    float speed;//当前设定速度
    BoxCollider2D boxcoll;//box碰撞体
    Collider2D coll;//碰撞体
    Animator anim;//动画器
    Rigidbody2D rb;//刚体
    float xinput;//x轴输入
    float yinput;//y轴输入
    float ZuoYou;//左右
    float Shangxia;//上下
    bool jump;//跳起
    bool jumph;//长按跳跃
    bool squat;//长按蹲键
    bool squatonce;//单次蹲键
    float jcoffset = 0.4f;//检测偏移
    float tccd = 0.2f;//检测长度
    float ctach = 0.4f;
    float reach = 0.7f;
    int zmfxi = 1;//正面方向
    Vector2 zmfx;//正面方向
    RaycastHit2D leftfoot;//测左脚器
    RaycastHit2D rightfoot;//测右脚器
    RaycastHit2D lefthead;//测左头器
    RaycastHit2D righthead;//测右头器
    RaycastHit2D nowhead;//正面头射线
    RaycastHit2D noweye;//正面眼射线
    RaycastHit2D bigua;//壁挂射线

    void Start()//初始化
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        boxcoll = GetComponent<BoxCollider2D>();
        speed = groundspeed;
        hangtime = Time.time;
        jumptime = Time.time;
    }
    void Update()//帧同步
    {
        if(GameManager.gamegodie())return;
        if (!hanging) FangXiang();
        move();
        change();
        checkinput();
        hang();
    }
    void FixedUpdate()//0.02秒
    {
    }

    void FangXiang()//左右扭头
    {
        ZuoYou = Input.GetAxisRaw("ShuiPing1");
        if (ZuoYou != 0)
            if (transform.localScale.x != ZuoYou)
                transform.localScale = new Vector3(ZuoYou, 1,1);
        //正面方向
        if (transform.localScale.x > 0) { zmfxi = 1; zmfx = Vector2.right; }
        if (transform.localScale.x < 0) { zmfxi = -1; zmfx = Vector2.left; }
    }
    void move()//左右移动
    {
        xinput = Input.GetAxis("ShuiPing1");
        if (xinput != 0) rb.velocity = new Vector2(xinput * speed, rb.velocity.y);
    }
    void checkinput()//输入检测与状态
    {

        //射线
        leftfoot = wtjc(new Vector2(-jcoffset, 0), Vector2.down, tccd, DiBan);//左脚射线
        rightfoot = wtjc(new Vector2(jcoffset, 0), Vector2.down, tccd, DiBan);//右脚射线
        lefthead = wtjc(new Vector2(-jcoffset, boxcoll.size.y), Vector2.up, tccd, DiBan);//左头射线
        righthead = wtjc(new Vector2(jcoffset, boxcoll.size.y), Vector2.up, tccd, DiBan);//右头射线
        if (!squatzt)//非蹲时 悬挂射线
        {
            nowhead = wtjc(new Vector2(jcoffset * zmfxi, boxcoll.size.y), zmfx, ctach, DiBan);//正面头射线
            noweye = wtjc(new Vector2(jcoffset * zmfxi, boxcoll.size.y * 0.75f), zmfx, ctach, DiBan);//正面眼射线
            bigua = wtjc(new Vector2(reach * zmfxi, boxcoll.size.y), Vector2.down, ctach, DiBan);//壁挂射线
        }
        //状态
        if (leftfoot || rightfoot) ground = true; else ground = false;
        sky = !ground;
        if (lefthead || righthead) coverhead = true; else coverhead = false;
        //输入
        jump = Input.GetButtonDown("jump");//单次跳跃
        jumph = Input.GetButton("jump");//长按跳跃
        squatonce = Input.GetButtonDown("squat");//单次蹲
        squat = Input.GetButton("squat");//长按蹲
        //测速
        nowxspeed = rb.velocity.x;//查看x速度
        nowyspeed = rb.velocity.y;//查看y速度
    }
    void change()//改变状态、速度、碰撞体
    {
        if (hanging)//悬挂时
        {
            if (jump)//悬挂跳
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(new Vector2(0, hungjumpforce), ForceMode2D.Impulse);
                jumptime = Time.time;
                hanging = false;
                AudioManager.playjumpaudio();
            }
            if (squatonce)//悬挂下落
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                hanging = false;
                hangtime = Time.time + 0.2f;
            }
        }
        if (!hanging)//非悬挂时
        {
            if (sky)//天空
            {
                speed = skyspeed;
                squatzt = false;
                squatup();
                if (jumptime > Time.time) if (jumph) rb.AddForce(new Vector2(0, jumpforcemore * Time.deltaTime), ForceMode2D.Impulse);
            }
            if (ground)//地面
            {
                if(squat)//地面蹲
                {
                    speed = squatspeed;
                    squatzt = true;
                    squatdown();
                    if (!coverhead&&jump)//蹲跳
                    {
                        rb.AddForce(new Vector2(0, jumpforce + squatjumpforcemore), ForceMode2D.Impulse);
                        jumptime = Time.time + jumptimemore;
                        AudioManager.playjumpaudio();
                    }
                }
                if (!squat)//松开蹲键
                {
                    if (squatzt)//蹲状态时
                    {
                        if (coverhead)//地面蹲卡头
                        {
                            squatzt = true;
                            squatdown();
                        }
                        if (!coverhead)//地面起立
                        {
                            squatzt = false;
                            squatup();
                        }
                    }
                    if (!squatzt)//地面站立
                    {
                        squatzt = false;
                        speed = groundspeed;
                        squatup();
                        if (jump&&!coverhead)//地面跳
                        {
                            rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                            jumptime = Time.time + jumptimemore;
                            AudioManager.playjumpaudio();
                        }
                    }
                }
            }
        }
    }
    void hang()//悬挂
    {
        if (!nowhead && noweye && bigua && !ground && !hanging && nowyspeed < 0 && hangtime < Time.time)//挂上去
        {
            Vector3 pos = transform.position;
            pos.x += (noweye.distance - 0.1f) * zmfxi;
            pos.y -= bigua.distance;
            pos.y += 0.1f;
            transform.position = pos;
            rb.bodyType = RigidbodyType2D.Static;
            hanging = true;
        }
    }
    RaycastHit2D wtjc(Vector2 offset, Vector2 direction, float length, LayerMask mblayer)//射线并绘制
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, direction, length, mblayer);
        Color ys = hit ? Color.green : Color.red;
        Debug.DrawRay(pos + offset, direction * length, ys);
        return hit;
    }

    void squatdown()//蹲下
    {
        if (boxcoll.size.y != 0.9f)
        {
            boxcoll.size = new Vector2(boxcoll.size.x, 0.9f);
            boxcoll.offset = new Vector2(boxcoll.offset.x, 0.45f);
        }
    }
    void squatup()//站起
    {
        if ((boxcoll.size.y != 1.8f) && (!coverhead))
        {
            boxcoll.size = new Vector2(boxcoll.size.x, 1.8f);
            boxcoll.offset = new Vector2(boxcoll.offset.x, 0.9f);
        }
    }
}
