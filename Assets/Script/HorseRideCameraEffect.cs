using UnityEngine;
using DG.Tweening;

public class HorseRideCameraEffect : MonoBehaviour
{
    public bool isRiding = false;
    public float bounceHeight = 0.08f;     // ���Ʒ� �ⷷ��
    public float bounceSpeed = 0.2f;

    public float tiltAngle = 3f;           // �յ� ���� (X��)
    public float tiltSpeed = 0.15f;

    public Vector3 shakeStrength = new Vector3(0.03f, 0.02f, 0f); // �̼� ��鸲
    public float shakeDuration = 0.4f;
    public int vibrato = 20;

    private Tween bounceTween;
    private Tween tiltTween;
    private Tween shakeTween;

    private Vector3 originalLocalPos;
    private Vector3 originalLocalRot;

    void Start()
    {
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localEulerAngles;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) StartRideEffect();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopRideEffect();
    }

    public void StartRideEffect()
    {
        if (isRiding) return;
        isRiding = true;

        // ���� �ⷷ��
        bounceTween = transform.DOLocalMoveY(originalLocalPos.y + bounceHeight, bounceSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // �յ� �� ����
        tiltTween = transform.DOLocalRotate(new Vector3(tiltAngle, originalLocalRot.y, originalLocalRot.z), tiltSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // �̼� ��鸲
        shakeTween = transform.DOShakePosition(shakeDuration, shakeStrength, vibrato)
            .SetLoops(-1, LoopType.Restart);
    }

    public void StopRideEffect()
    {
        if (!isRiding) return;
        isRiding = false;

        bounceTween?.Kill();
        tiltTween?.Kill();
        shakeTween?.Kill();

        // ���� ��ġ, ȸ�� ����
        transform.DOLocalMove(originalLocalPos, 0.2f);
        transform.DOLocalRotate(originalLocalRot, 0.2f);
    }
}
