using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image Icon = null;
    [SerializeField] private Sprite Cross = null;
    [SerializeField] private Sprite Circle = null;
    private Cell _data;
    private event Action<int> _onTouched;

    public void SetTouchedCallback(Action<int> onTouchedCallback)
    {
        _onTouched += onTouchedCallback;
    }

    public void SetData(Cell cell)
    {
        _data = cell;
    }

    public void UpdateView()
    {
        Icon.enabled = _data.Owner >= 0;
        Icon.sprite = _data.Owner == 1 ? Cross : Circle;
    }

    public override string ToString()
    {
        return _data != null ? $"{_data.Id}:[{_data.X }, {_data.Y}]" : "[Empty]";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_data == null || _data.Owner >= 0) return;
        _onTouched?.Invoke(_data.Id);
    }

    private void OnDrawGizmos()
    {
        if(_data != null)
        {
            Handles.Label(transform.position, ToString());
        }
    }
}
