using UnityEngine;

namespace Platformer
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping;

        // Лучше камеру в LateUpdate , когда все остальные элементы сцены уже подвигались,
        private void LateUpdate()
        {                                                                                                   // позицию по z оставляем, т.к. мы в 2D
            var destination = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _damping);
            // присваиваем камере новую позицию с помощью интерполяции. Указываем начальную позицию, направление, и величину, на которую будем умножать
            // каждый LateUpdate мы будем получать желаемую позицию. Затем двигать от текущей позиции к желаемой через интерполяцию, и умножать на скорость в зависимости от времени, которое прошло с последнего кадра.
        }
    }
}

