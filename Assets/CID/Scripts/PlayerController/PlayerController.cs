using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 3; // �ړ����x
    [SerializeField] private float jumpPower = 3; // �W�����v��
    private CharacterController _characterController; // CharacterController�̃L���b�V��
    private Transform _transform; // Transform�̃L���b�V��
    private Vector3 _moveVelocity; // �L�����̈ړ����x���

    private void Start()
    {
        _characterController = GetComponent<CharacterController>(); // ���t���[���A�N�Z�X����̂ŁA���ׂ������邽�߂ɃL���b�V�����Ă���
        _transform = transform; // Transform���L���b�V������Ə����������ׂ�������
    }

    private void Update()
    {
        //Debug.Log(_characterController.isGrounded ? "�n��ɂ��܂�" : "�󒆂ł�");

        // ���͎��ɂ��ړ������i�����𖳎����Ă���̂ŁA�L�r�L�r�����j
        _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        _moveVelocity.z = Input.GetAxis("Vertical") * moveSpeed;

        // �ړ������Ɍ���
        _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));

        if (_characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // �W�����v����
                Debug.Log("�W�����v�I");
                _moveVelocity.y = jumpPower; // �W�����v�̍ۂ͏�����Ɉړ�������
            }
        }
        else
        {
            // �d�͂ɂ�����
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
        }

        // �I�u�W�F�N�g�𓮂���
        _characterController.Move(_moveVelocity * Time.deltaTime);

        // �ړ��X�s�[�h��animator�ɔ��f
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
    }
}