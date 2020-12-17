using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class BackEndGameInfo : MonoBehaviour
{
	Param t_param;
	string infoStr;
	public void OnClickInsertData()
	{
		int stage1Score = 100;
		int stage2Score = 200;
		int stage3Score = 300;

		Param param = new Param();
		t_param = param;
		param.Add("stage1", stage1Score);
		param.Add("stage2", stage2Score);
		param.Add("stage3", stage3Score);


		BackendReturnObject BRO = Backend.GameInfo.Insert("StageScore", param);

		if(BRO.IsSuccess())
		{
			Debug.Log("indate : " + BRO.GetInDate());
		}
		else
		{
			switch (BRO.GetStatusCode())
			{
				case "404":
					Debug.Log("존재하지 않는 tableName인 경우");
					break;
				case "412":
					Debug.Log("비활성화 된 tableName인 경우");
					break;
				case "413":
					Debug.Log("하나의 row(colum들의 집합)이 400KB를 넘는경우");
					break;
				default:
					Debug.Log("서버 공통 에러 발생 : " + BRO.GetMessage());
					break;
			}	
		}
	}
	//table list Load
	public void OnClickGetTableList()
	{
		BackendReturnObject BRO = Backend.GameInfo.GetTableList();

		if(BRO.IsSuccess())
		{
			JsonData publics = BRO.GetReturnValuetoJSON()["publicTables"];

			Debug.Log("public Tables--------------------------------");
			foreach(JsonData row in publics)
			{
				Debug.Log(row.ToString());
			}

			Debug.Log("Private Tables--------------------------------");
			JsonData privates = BRO.GetReturnValuetoJSON()["privateTables"];
			foreach(JsonData row in privates)
			{
				Debug.Log(row.ToString());
			}
		}
		else
		{
			Debug.Log("서버 공통 에러 발생 : " + BRO.GetMessage());
		}
	}
	//Public table data Load
	public void OnClickPublicContents()
	{
		BackendReturnObject BRO = Backend.GameInfo.GetPublicContents("StageScore", 1);
		
		if(BRO.IsSuccess())
		{
			Debug.Log(BRO.FirstKeystring());
			GetGameInfo(BRO.GetReturnValuetoJSON());
		}
		else
		{
			CheckError(BRO);
		}
	}

	//private table data Load
	public void OnClickGetPrivateContents()
	{
		BackendReturnObject BRO = Backend.GameInfo.GetPrivateContents("");

		if (BRO.IsSuccess())
		{
			GetGameInfo(BRO.GetReturnValuetoJSON());
		}
		else
		{
			CheckError(BRO);
		}
	}

	//공개 테이블에서 특정 유저의 정보 불러오기
	public void OnClickGetPublicContentsByGamerIndate()
	{
		BackendReturnObject BRO = Backend.GameInfo.GetPublicContentsByGamerIndate("StageScore", infoStr);

		if (BRO.IsSuccess())
		{
			Debug.Log(BRO.GetReturnValue());
			GetGameInfo(BRO.GetReturnValuetoJSON());
			Debug.Log("indate : " + BRO.GetInDate());
			infoStr = BRO.GetInDate();
		}
		else
		{
			CheckError(BRO);
		}
	}

	private void GetGameInfo(JsonData returnData)
	{
		if(returnData != null)
		{
			Debug.Log("데이터가 존재합니다.");

			//rows Load
			if (returnData.Keys.Contains("rows"))
			{
				Debug.Log("rows로 전달받음");
				JsonData rows = returnData["rows"];
				for (int i = 0; i < rows.Count; i++)
				{
					Debug.Log(rows[i]);
					GetData(rows[i]);

				}
			}

			//row Load
			else if(returnData.Keys.Contains("row"))
			{
				Debug.Log("row로 전달받음");

				JsonData row = returnData["row"];
				GetData(row[0]);
			}
		}
		else
		{
			Debug.Log("데이터가 없습니다.");
		}
	}

	private void GetData(JsonData data)
	{
		var stage1Score = data["stage1"][0];
		var stage2Score = data["stage2"][0];
		var stage3Score = data["stage3"][0];

		Debug.Log(stage1Score);
		Debug.Log(stage2Score);
		Debug.Log(stage3Score);
	}
	string firstKey = string.Empty;
	public void OnClickPublicContentsNext()
	{
		BackendReturnObject BRO;

		if (firstKey == null)
		{
			BRO = Backend.GameInfo.GetPublicContents("StageScore", 1);
		}
		else
		{
			//이전에 불러온 데이터의 다음 순번 데이터를 불러온다.
			BRO = Backend.GameInfo.GetPublicContents("StageScore", firstKey, 1);
		}

		if(BRO.IsSuccess())
		{
			firstKey = BRO.FirstKeystring();
			GetGameInfo(BRO.GetReturnValuetoJSON());
		}
		else
		{
			CheckError(BRO);
		}
	}
	public void OnClickGameInfoUpdate()
	{
		Param param = new Param();
		param.Add("stage1", 9999);
		
		BackendReturnObject BRO = Backend.GameInfo.Update("StageScore", OnClickGetIndate(), param);
		if (BRO.IsSuccess())
		{
			Debug.Log("수정 완료");
		}
		else
		{
			switch (BRO.GetStatusCode())
			{
				case "405":
					Debug.Log("param에 partition, gamer_id, inDate, updatedAt 네가지 필드가 있는 경우");
					break;

				case "403":
					Debug.Log("퍼블릭테이블의 타인정보를 수정하고자 하였을 경우");
					break;

				case "404":
					Debug.Log("존재하지 않는 tableName인 경우");
					break;

				case "412":
					Debug.Log("비활성화 된 tableName인 경우");
					break;

				case "413":
					Debug.Log("하나의 row( column들의 집합 )이 400KB를 넘는 경우");
					break;
			}
		}
	}
	private void CheckError(BackendReturnObject BRO)
	{
		switch (BRO.GetStatusCode())
		{
			case "200":
				Debug.Log("해당 유저의 데이터가 테이블에 없습니다.");
				break;
			case "404":
				if (BRO.GetMessage().Contains("gamer not found"))
				{
					Debug.Log("gamerIndate가 존재하지 gamer의 indate인 경우");
				}
				else if (BRO.GetMessage().Contains("table not found"))
				{
					Debug.Log("존재하지 않는 테이블");
				}
				break;
			case "400":
				if(BRO.GetMessage().Contains("bad limit"))
				{
					Debug.Log("limit 값이 100이상인 경우");
				}
				else if(BRO.GetMessage().Contains("bad table"))
				{
					//public Table 정보를 얻는 코드로 privte Table 에 접근했을 떄 또는
					//private Table 정보를 얻는 코드로 public Table 에 접근했을 때
					Debug.Log("요청한 코드와 테이블의 고액여부가 맞지 않습니다.");
				}
				break;
			case "412":
				Debug.Log("비활성화된 테이블입니다.");
				break;
			default:
				Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
				break;
		}
	}

	public void OnClickGameInfoCalculatorUpdate()
	{
		Param param = new Param();
		param.AddCalculation("stage1", GameInfoOperator.addition, -10);


		BackendReturnObject BRO = Backend.GameInfo.Update("StageScore", OnClickGetIndate(), param);
		if (BRO.IsSuccess())
		{
			Debug.Log("수정 완료");
		}
		else
		{
			switch (BRO.GetStatusCode())
			{
				case "405":
					Debug.Log("param에 partition, gamer_id, inDate, updatedAt 네가지 필드가 있는 경우");
					break;

				case "403":
					Debug.Log("퍼블릭테이블의 타인정보를 수정하고자 하였을 경우");
					break;

				case "404":
					Debug.Log("존재하지 않는 tableName인 경우");
					break;

				case "412":
					Debug.Log("비활성화 된 tableName인 경우");
					break;

				case "413":
					Debug.Log("하나의 row( column들의 집합 )이 400KB를 넘는 경우");
					break;
			}
		}

	}
	private string OnClickGetIndate()
	{
		Param where = new Param();
		BackendReturnObject bro = Backend.GameSchemaInfo.Get("StageScore", where, 10);
		// 빈 where절을 이용하여 데이터 10개 검색
		string inDate = bro.GetReturnValuetoJSON()["rows"][0]["inDate"]["S"].ToString();
		Debug.Log(inDate);
		return inDate;
		//리턴된 값들중에서 0번째 데이터의 inDate로 접근하여 string으로 추출
		Backend.GameSchemaInfo.Update("tableName", inDate, where);
		// 해당 inDate의 데이터 수정
		Backend.GameSchemaInfo.UpdateRTRankTable("tableName", "columnName", 123, inDate);
	}
}
