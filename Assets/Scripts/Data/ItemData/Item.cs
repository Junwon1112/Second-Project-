using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item �ϳ� ���� (��ü �ϳ�)
/// </summary>
public class Item : MonoBehaviour
{
    /// <summary>
    /// �� �������� ���� ������ ������
    /// </summary>
    public ItemData data;

    private void Start()
    {
        if(transform.parent == null)
        {
            ParticlePlayer.Instance.PlayParticle(ParticleType.ParticleSystem_ItemAura, this.transform);
        }
    }
}
