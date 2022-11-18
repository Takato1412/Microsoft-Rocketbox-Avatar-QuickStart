using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushableButton : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPressedEvent : UnityEvent { }
    public ButtonPressedEvent OnButtonPressed;

    // 押されてない状態の高さ
    private float unpushedY = 0.0f;
    // 押したと判定する高さ
    private float pushedThreshold = -0.02f;

    // 押されているか
    [SerializeField]
    private bool isPushed;

    // Rigidbody
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // 少しでも押されていたら押し返す
        if (transform.localPosition.y < unpushedY)
        {
            rb.AddForce(transform.up);
            transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);// x, z
        }
        // 初期位置に戻ってきたら、しっかり元の位置に戻す
        else
        {
            //buttonTop.transform.localPosition = new Vector3(buttonTop.transform.localPosition.x, unpushedY, buttonTop.transform.localPosition.z);
            transform.localPosition = new Vector3(0f, unpushedY, 0f);
        }

        // 閾値まで押し込まれたら、押し込まれた際の処理をする
        if (!isPushed && transform.localPosition.y < pushedThreshold)
        {
            isPushed = true;
            // 押し込み時の処理
            OnButtonPressed.Invoke();

        }
        // 押し込まれたあと、閾値未満に戻ってきたら
        if (isPushed && transform.localPosition.y > pushedThreshold)
        {
            isPushed = false;
            //audioSource.Play();
        }
    }

}
