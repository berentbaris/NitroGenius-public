//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public class ActionData : ScriptableObject
{

	public EffectData effectData;
	public SectorList sectorList;

	public void OnGameStart()
	{
		foreach (Sector sector in sectorList.list)
		{
			sector.Actions.Clear();
		}

		foreach (Action item in _ActionItems)
		{
			item._Belonging_Sector = sectorList.list[item._Belonging_Sector_ID];
			item._Belonging_Sector.Actions.Add(item);
			item._Image_Sprite = Resources.Load<Sprite>(item._Image_Ref);
			item._Effects.Clear();
			for (int i = 0; i < item._Effect_IDs.Length; i++)
			{
				if (effectData.GetEffect(item._Effect_IDs[i]) != null)
				{
					item._Effects.Add(effectData.GetEffect(item._Effect_IDs[i]));
				}
			}
		}
	}

	public void OnTurnChange(int Turn)
	{
		foreach (Action item in _ActionItems)
		{
			if (item._Repetivity == "Rotating")
			{
				if (item._ExecutedTurn + item._Cooldown >= Turn)
				{
					if (!item._Belonging_Sector.Actions.Contains(item))
					{
						item._Belonging_Sector.Actions.Add(item);
					}
				}
			}
		}
	}

	[SerializeField]
	public List<Action> _ActionItems;

	public Action GetAction(int action_ID)
	{
		int min = 0;
		int max = _ActionItems.Count;
		while (min < max)
		{
			int index = (min + max) >> 1;
			Action item = _ActionItems[index];
			if (item._Action_ID == action_ID) { return item; }
			if (action_ID < item._Action_ID)
			{
				max = index;
			}
			else
			{
				min = index + 1;
			}
		}
		return null;
	}

	public interface IDataGetter
	{
		Action GetAction(int Action_ID);
	}

	private class DataGetter : IDataGetter
	{
		private Func<int, Action> _GetAction;
		public Action GetAction(int Action_ID)
		{
			return _GetAction(Action_ID);
		}
		public DataGetter(Func<int, Action> getAction)
		{
			_GetAction = getAction;
		}
	}
}

[Serializable]
public class Action
{
	[SerializeField]
	public int _Cooldown = 3;

	[SerializeField]
	public int _ExecutedTurn;

	[SerializeField]
	public int _Action_ID;

	[SerializeField]
	public string _Name;

	[SerializeField]
	public int _Belonging_Sector_ID;

	[SerializeReference]
	public Sector _Belonging_Sector;

	[SerializeField]
	public int _Priority;

	[SerializeField]
	public bool _Importance;

	[SerializeField]
	public string _Description;

	[SerializeField]
	public string _Repetivity;

	[SerializeField]
	public float _Cost;

	[SerializeField]
	public string _NH3;

	[SerializeField]
	public string _Nox;

	[SerializeField]
	public string _N2O;

	[SerializeField]
	public string _Image;

	[SerializeField]
	public string _Budget;

	[SerializeField]
	public int[] _Effect_IDs;

	[SerializeField]
	public List<Effect> _Effects;

	[SerializeField]
	public string _Image_Ref;

	[SerializeField]
	public Sprite _Image_Sprite;
}