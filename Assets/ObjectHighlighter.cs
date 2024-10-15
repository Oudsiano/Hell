using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    public float raycastDistance = 5f; // Дальность луча для обнаружения объектов
    public LayerMask interactableLayer; // Убедитесь, что здесь выбран слой Interactable
    public Material highlightMaterial; // Материал для выделения
    private Material originalMaterial; // Исходный материал объекта
    private Transform highlightedObject; // Ссылка на текущий выделенный объект

    void Update()
    {
        CheckForObject();
    }

    void CheckForObject()
    {
        // Стандартный луч из центра камеры вперед
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        // Проверяем, столкнулся ли луч с интерактивным объектом
        if (Physics.Raycast(ray, out hit, raycastDistance, interactableLayer))
        {
            Transform objectHit = hit.transform;

            // Проверяем, не выделен ли уже объект
            if (highlightedObject != objectHit)
            {
                ClearHighlight();

                // Сохраняем ссылку на новый объект
                highlightedObject = objectHit;

                // Меняем материал объекта
                Renderer renderer = highlightedObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    originalMaterial = renderer.material;
                    renderer.material = highlightMaterial;
                }
            }
        }
        else
        {
            // Сбрасываем выделение, если луч не нацелен на интерактивный объект
            ClearHighlight();
        }
    }

    void ClearHighlight()
    {
        if (highlightedObject != null)
        {
            // Возвращаем оригинальный материал объекту
            Renderer renderer = highlightedObject.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }
            highlightedObject = null;
        }
    }
}
