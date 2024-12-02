using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private List<GameObject> characters = new List<GameObject>();

    public void AddCharacter(GameObject character)
    {
        if (characters.Contains(character)) { Debug.LogWarning("You are tried to already Existing Character"); }

        characters.Add(character);
    }

    public List<GameObject> GetCharacter(int layerMask)
    {
       List<GameObject> list = new List<GameObject>();

        list = characters.Where((obj) => (1 << obj.layer & layerMask) != 0).ToList();
        
        return list;
    }

    /// <summary>
    /// Character Manager�� ��ϵ� ĳ�������� Ȯ���մϴ�.
    /// </summary>
    /// <param name="character">Ȯ���� ĳ����</param>
    /// <returns>��ϵ� ĳ������ ��� true</returns>
    public bool IsCharacter(GameObject character)
    {
        return characters.Contains(character);
    }
}
