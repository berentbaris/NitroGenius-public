//----------------------------------------------
//    Auto Generated. DO NOT edit manually!
//----------------------------------------------

#pragma warning disable 649

using System;
using UnityEngine;
using System.Collections.Generic;

public partial class NationalData : ScriptableObject {

	public SectorList sectorList;
    public ActivityData ActivityData;
	public DepositionData DepositionData;
	public IntVariable Turn;
	public ActionData actionData;

	public void ClearRecords()
	{
		_NationalItem.GDP_Per_Capita_Record.Clear();
		_NationalItem.Happiness_Record.Clear();
		_NationalItem.Total_Value_Record.Clear();
	}

	public void RecordPreviousTurnValues()
	{
		_NationalItem.GDP_Per_Capita_Record.Add(_NationalItem._GDP_Per_Capita);
		_NationalItem.Happiness_Record.Add(_NationalItem._Happiness);
		_NationalItem.Total_Value_Record.Add(_NationalItem._Total_Value);
	}

	public void CalculateNationalDeposition()
	{
		_NationalItem._National_N2O_Emissions = 0;
		foreach (Activity activity in ActivityData._ActivityItems)
		{
		_NationalItem._National_N2O_Emissions += activity._N2O_Emissions;
		}
		foreach (Deposition item in DepositionData._DepositionItems)
		{
			if (item._NL == true)
			{
				_NationalItem._Mean_Nox_Deposition += item._NOx_Deposition;
				_NationalItem._Mean_NH3_Deposition += item._NH3_Deposition;
			}
			if (item._Natura2000 == true)
			{
				_NationalItem._Mean_N2000_Nox_Deposition += item._NOx_Deposition;
				_NationalItem._Mean_N2000_NH3_Deposition += item._NH3_Deposition;
			}
		}
		_NationalItem._Mean_Nox_Deposition = _NationalItem._Mean_Nox_Deposition / 1520;
		_NationalItem._Mean_NH3_Deposition = _NationalItem._Mean_NH3_Deposition / 1520;
		_NationalItem._Mean_N2000_Nox_Deposition = _NationalItem._Mean_N2000_Nox_Deposition / 263;
		_NationalItem._Mean_N2000_NH3_Deposition = _NationalItem._Mean_N2000_NH3_Deposition / 263;
	}

	public void CalculateN2000Percentage()
    {
		_NationalItem._N2000_Below_Critical = 0;
		foreach (Deposition deposition in DepositionData._DepositionItems)
		{
			if (deposition._Total_Deposition < deposition._Critical_Deposition && deposition._Natura2000 == true)
			{
				_NationalItem._N2000_Below_Critical++;
			}
		}
		_NationalItem._N2000_Below_Critical = _NationalItem._N2000_Below_Critical / 263 * 100;
	}

	public void CalculateHappiness()
	{
		//Population
		_NationalItem._Population = _NationalItem._Population + (_NationalItem._Population / 100 * _NationalItem._Population_Growth_Rate);
		//GDP/Value
		_NationalItem._Total_Value = 0;
		_NationalItem._GDP = 0;
		foreach (Activity item in ActivityData._ActivityItems)
		{
			_NationalItem._Total_Value += item._Production_Value;
		}
		_NationalItem._GDP = _NationalItem._Total_Value * 1276160;
		for (int i = 1; i < Turn.Value; i++)
		{
			_NationalItem._GDP = _NationalItem._GDP - ((_NationalItem._GDP / 100) * 3.7f);
		}
		//GDP Per Capita
		_NationalItem._GDP_Per_Capita = _NationalItem._GDP / _NationalItem._Population;
		//Calculate Employement
		_NationalItem._Unemployment = 0;
		foreach (Activity activity in ActivityData._ActivityItems)
		{
			_NationalItem._Unemployment += activity._Product_Volume * activity._Jobs_Per_Unit;
		}
		_NationalItem._Unemployment = 95.15f - (95.15f / 10) + (_NationalItem._Unemployment * 0.00000145295f) * 0.97f;
		//Calculate Happiness
		_NationalItem._Happiness = 0;
		_NationalItem._Economic_Factor = 0;
		_NationalItem._Health_Factor = 0;
		_NationalItem._Environment_Factor = 0;
		_NationalItem._Economic_Factor = (((_NationalItem._GDP_Per_Capita * _NationalItem._Unemployment) - 1867620) / (7867620 - 1867620)) * (30 - 0) + 0; 
		_NationalItem._Health_Factor = (((0 - _NationalItem._Mean_Nox_Deposition) / (3000 - 0)) * (15 - 0) + 15) + (((0 - _NationalItem._Mean_NH3_Deposition) / (3000 - 0)) * (10 - 0) + 10) + (((0 - _NationalItem._Mean_N2000_NH3_Deposition) / (3600 - 0)) * (5 - 0) + 5) + ((((_NationalItem._GDP_Per_Capita * _NationalItem._Unemployment) - 1867620) / (7867620 - 1867620)) * (5 - 0) + 0) + (((5000 - _NationalItem._National_N2O_Emissions) / (65889 - 5000)) * (5 - 0) + 5);
		foreach (Sector sector in sectorList.list)
		{
			sector.public_health = _NationalItem._Health_Factor;
		}
		_NationalItem._Environment_Factor = (((0 - _NationalItem._Mean_Nox_Deposition) / (3000 - 0)) * (10 - 0) + 10) + (((0 - _NationalItem._Mean_N2000_NH3_Deposition) / (3600 - 0)) * (10 - 0) + 10) + (((5000 - _NationalItem._National_N2O_Emissions) / (65889 - 5000)) * (10 - 0) + 10);
		_NationalItem._Happiness = _NationalItem._Economic_Factor + _NationalItem._Health_Factor + _NationalItem._Environment_Factor;
		foreach (Sector sector in sectorList.list)
		{
			if (sector.SelectedChoices.Contains(actionData.GetAction(211)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(212)))
				{
					_NationalItem._Happiness_Adjuster += 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(177)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(170)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(150)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(151)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(86)))
				{
					_NationalItem._Happiness_Adjuster -= 0.5f;
				}
			if (sector.SelectedChoices.Contains(actionData.GetAction(88)))
				{
					_NationalItem._Happiness_Adjuster += 0.5f;
				}
		}
		_NationalItem._Happiness = _NationalItem._Happiness + _NationalItem._Happiness_Adjuster;
	}

	public void CalculateSectorScore()
	{
		foreach (Sector sector in sectorList.list)
        {
			if (sector != sectorList.list[3] && sector != sectorList.list[2])
			{
				sector.score = (((_NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((_NationalItem._Happiness - 0) / (100 - 0)) * (20 - 0) + 0) + (((_NationalItem._Economic_Factor - 0) / (30 - 0)) * (20 - 0) + 0) + ((((((sector.Product_Volume_Total - sector.ProductVolumeTotalRecord[0]) / sector.ProductVolumeTotalRecord[0]) * 100) + 100) / (100 + 100)) * (10 - 0) + 0) + (((sector.Image - 8) / (12 - 8)) * (10 - 0) + 0);
			}
			if (sector == sectorList.list[3])
			{
				sector.score = (((_NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((_NationalItem._Happiness - 0) / (100 - 0)) * (20 - 0) + 0) + (((_NationalItem._Economic_Factor - 0) / (30 - 0)) * (20 - 0) + 0) + (((sector.Image - 8) / (12 - 8)) * (20 - 0) + 0);
			}
			if (sector == sectorList.list[2])
			{
				sector.score = (((_NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((_NationalItem._Happiness - 0) / (100 - 0)) * (30 - 0) + 0) + (((_NationalItem._Economic_Factor - 0) / (30 - 0)) * (30 - 0) + 0);
			}
		}
	}	

	[NonSerialized]
	private int mVersion = 1;

	[SerializeField]
	public National _NationalItem;

	public void Reset() {
		mVersion++;
	}
}

[Serializable]
public class National {

	[SerializeField]
	public float _Population;

	[SerializeField]
	public float _Population_Growth_Rate;

	[SerializeField]
	public float _GDP;

	[SerializeField]
	public float _Total_Value;
	public List<float> Total_Value_Record = new List<float>();

	[SerializeField]
	public float _GDP_Per_Capita;
    public List<float> GDP_Per_Capita_Record = new List<float>();

	[SerializeField]
	public float _Unemployment;

	[SerializeField]
	public float _Economic_Factor;

	[SerializeField]
	public float _Health_Factor;

	[SerializeField]
	public float _Environment_Factor;

	[SerializeField]
	public float _Happiness;
    public List<float> Happiness_Record = new List<float>();

	[SerializeField]
	public float _Happiness_Adjuster;

	[SerializeField]
	public float _National_N2O_Emissions;

	[SerializeField]
	public float _National_NH3_Emissions;

	[SerializeField]
	public float _National_Nox_Emissions;

	[SerializeField]
	public float _National_Total_Deposition;

	[SerializeField]
	public float _Mean_Nox_Deposition;

	[SerializeField]
	public float _Mean_NH3_Deposition;

	[SerializeField]
	public float _Mean_N2000_Nox_Deposition;

	[SerializeField]
	public float _Mean_N2000_NH3_Deposition;

	[SerializeField]
	public float _N_Imports;

	[SerializeField]
	public float _N_Exports;

	[SerializeField]
	public float _N2000_Below_Critical;
}