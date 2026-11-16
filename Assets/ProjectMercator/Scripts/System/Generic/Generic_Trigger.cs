using UnityEngine;

/// <summary>
/// Базовый класс, который внутри реализует OnTriggerEnter/OnTriggerExit, его методы возвращают GameObject. 
/// Можно засекать объекты с интерфейсами или по слою, переключается коллекцией TriggerType_Enum
/// </summary>
public abstract class Trigger : MonoBehaviour
{
    [Header("Target Validation")]
    [SerializeField] protected TriggerType_Enum _triggerType;
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] protected string _interfaceName;
    /// <summary>
    /// Метод на вход в триггер, реализуется базовым OnTriggerEnter, возвращает GameObject
    /// </summary>
    /// <param name="anyObject"></param>
    protected abstract void OnEnter(GameObject anyObject);
    /// <summary>
    /// метод на выход из триггера, реализуется базовым OnTriggerExit, возвращает GameObject
    /// </summary>
    /// <param name="anyObject"></param>
    protected abstract void OnExit(GameObject anyObject);
    /// <summary>
    /// метод на нахождение в триггере, реализуется базовым OnTriggerStay, возвращает GameObject
    /// </summary>
    /// <param name="anyObject"></param>
    protected virtual void OnStay(GameObject anyObject) { }

    /// <summary>
    /// Взависимости от выбраного имени в коллекции будет проверяться интерфейс или слой
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    protected bool IsValidTarget(GameObject other)
    {
        // Основная проверка по типу
        return _triggerType switch
        {
            TriggerType_Enum.LayerMask => ValidateLayerMask(other),
            TriggerType_Enum.Interface => ValidateInterface(other),
            _ => false,
        };
    }

    private bool ValidateLayerMask(GameObject other)
    {
        return (_targetLayer & (1 << other.layer)) != 0;
    }

    private bool ValidateInterface(GameObject other)
    {
        if (string.IsNullOrEmpty(_interfaceName))
            return false;
        return other.GetComponent(_interfaceName) != null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidTarget(other.gameObject))
            OnEnter(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidTarget(other.gameObject))
            OnExit(other.gameObject);
    }    

    private void OnTriggerStay(Collider other)
    {
        if (IsValidTarget(other.gameObject))
            OnStay(other.gameObject);
    }
}
