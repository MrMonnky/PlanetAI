using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dolo.PlanetAI.Nebula;
using Dolo.PlanetAI.NET;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProtoBuf;

namespace Dolo.PlanetAI;

public class MspClientNebula
{
	public static async Task<string> GetRecaptchaTokenAsync()
	{
		MemoryStream str = new MemoryStream();
		RestResponse<string> data = await MspApi.GetStringAsync("https://www.google.com/recaptcha/api2/anchor?ar=1&k=6LcxuOsUAAAAAI2IYDfxOvAZrwRg2T1E7sJq96eg&co=aHR0cHM6Ly9tb3ZpZXN0YXJwbGFuZXQyLmNvbTo0NDM.&hl=en&v=qljbK_DTcvY1PzbR7IG69z1r&size=invisible&cb=kc5or8nzj782");
		if (!data.Success)
		{
			return null;
		}
		Serializer.Serialize<ProtobufData>((Stream)str, new ProtobufData
		{
			A = "qljbK_DTcvY1PzbR7IG69z1r",
			B = data.Result.Split(new string[1] { "value=" }, StringSplitOptions.None)[1].TrimStart('"').Split('"')[0],
			C = "!7Oqg6u8KAAQeF6g9bQEHDwLHaMPdyY7ouljPgQLRkFk2F1_itEwRVMtNNtAClz4c9AJDkWZ0NDXld44MdB1GiLK3E3ykbGIqxFYsRce3-5wFxCJ8MDLUgEO21E4ZdXE05UJAytb9NZWAzdh9D0hVUfS1xCqJ5LGAEuAqvx6GwI76CT8bWF2EAtnEIeuK7YdFYVMCMfkwroua67Hs0vQXjCp3aC9aOL1dsjdH5QG5FjvX7bUyFWxm0du9GvS-O4ZD5ABvfNxd4GW9GfPWlSy2TKIx0eaPPvb4cGxFwHSbDpCvkENiej4PZw8d4oXCitcFzK2QbmbV5WlikvzC2GRRhyIYC2FNm1leYH1ZzwHBRXim1YA0JswO2-lBZ-Hk3Fo-q1LL6ZAIM3FoMwk5ZIUUhhady-Mp4HfP3ZW4vaZELKq6tbH0cNt-LXItIv7obnR5g865bfiI3ghOuJMPlEbdZBt5RG5j1gSoTMaOIgKXDjBrHfLdaK-L9uz6P6RegHy9aaTFWOJF092IwxB_7fdLqQtbOWYCmPrio2TDMGsnz8Q0AuhtZjBrqrHm3sNdXKJHj-ThNAmCdSeZN2dYtIuk9YbnqKdqxq6TQvH5F1yVQ23EKIK-Si27ovMKZd_TRqXYOcg-XtMhxr4VL2QUI55RZcjzs7bhl8NPFneQHffJhn7PPUn36UKEhMIJGHh4YRpVGeT8cRDZSMEbCsoXSQUvH1kOyTjJgnrC9eKYMU018jtMELoU4diIwVkJHKYidbvT134CAPBZM0trcy8KCkOjhJYbl9PQft2ELyoCHJ-YHnKnm_YPfSycArIdWh4q4FVz4IC_EZM-cdGvk4TH92f4iBOUINOioeEP9MS7TjyRT9p6KH0Jotojc2V0N7fmlcthcBqe3F9ll85q-tC4V1R0Ek3-K1quSfodSmbe8bAXdLRMYrGqa1RXh0G0H_aAbXeh7La7wZL1xOUnnAA03e9-8Q2jo_oE0ixGjMFUHqqfBN8qqNeWpOHzUZQJRtP1vb8r9g0F4S6j4MXL5rBnGsPcXQ",
			D = "217035401",
			E = "q",
			F = "login_create",
			G = "6LcxuOsUAAAAAI2IYDfxOvAZrwRg2T1E7sJq96eg",
			H = "0Z0HWX9hanGDpYuQm6nPsbrB0_Xb4OrJL-njLPMhV1E4SmxSW2JwlniBiJq8oquywObI0djrDPL4AxE3GSIpO11DSFNhh2lyeYutk5ijsde5wsmOsZdVbAYUBZBFu5JHZZftrBJP4dAWHI3oJijiGOpUdqiKdFrcXqDikP347jBSQCMIdrl_USexjxVLOa-I1znnWgclLsj4CiwSGyIwVjhBSFp8YmtygKaIkZiqzLK7wtD22OHo-x0DCBMhRykyOP4hKxEO0QMo5yGjTaPhBsknfQe0-xWX3dwpI3k8KhfN3757-cfSVFXf_e9xo73b1fSN0JW4wpj_MPacxsQCMFZsamUeYSZJUyz3LW8R45DLIV_eB5k-yPsdOyEz5SvtEBn_pTNhf5mXnk-SV3qD2eg6IN8MflyGXKpcpnyGyAo4XmSGdSZpLlFa_Je0eqjG4NLZltmewcsiLuETLU9ZU_1ABSgxi5nL0oy9uA40Dhi-VC40cbvqDBY0JtQa3AMIT3kvNRM4bpEalTKVSwVLafNk2uCy4QcRFzHPEdb6AAYbln_2hzlrhaOhnFWYXYCKPDZ_qdv2EBYsxgjN8PtExhBCXHp0iyxzNFthR4R6qMbg3uWW2Z7By7Hu4RM1UzlL_UAFKDHQApymWBGsHiRi3M5ovtCKwSJH0gAeKEJQUlRiWFp1HmEmSVNg50T3mUPFQ1Dq4Td56-4f6bdB_6GLZh-x9IJbxpdJe527obBlqLrcwsvSl7rAptEyXIMIRxlTITLs-rjLTZtcl4EXrLfYirjW8QLppumu0dvBZvEjRWNJWA1QFTvF_Bonpme2CKI0ZsD-hIp5BskmRE3oGjRSTFp0doSGjIM0ezxjabc0grDW3P7tnuHwFfgBC8zz-ZwFSC2cNniBQ3GrrZvBx7HL2eiFyI2wutTmaM48v1CDSM9ZcmFXgX6AoqSW3O8Rxx3ezeNmC0FbAigKK-msTT_1xxFDaXNxc32fka-6V5pfgohmX-7r8v0aDOJsLvC3UJ7E5qhzWNq8v0xSgJ7EutC-zOLU8u2e4abJ02I26Rs1V1VcBUgNMDnUBVOBn7m3wm-ywObI0didwMps9zhyhKrBFqSyzHbxmtVrsX9hMyCzAYbNI8XHiT9pf3lj6YOxNEoT6aPWS23AakwNl56kGgPWVMb4OixmtGqQ_v0GIJp8nlS6oMdI3nWLVKNUn7UesQPcz0lcBWNt833rhSsVy4pUTaxCfEpnfk_WCHqMikg-2BJEaekKAL5AUqQSaSI0xzDWvFKoV10C1SuRH7jzRLOlWsn7MSQBh8ID6X-OL8obmj9Np6Ir3XP6E-6QieQWgDIcPdAfAOcAanwieQpktsC2_Ms8zlSCwNNRT5SfCSudN6nz4WPFa91H6aulZAXLzkRpyFVcMnABkGpMebQ2JAnUwkPp3EHsamTmqO78dqTG3FMo0llu_Vrk1ol3AWsdS8TXaWv0103--ReF5BG3xVe50-VTnkQtcEakJagWNC28mpfKtD8IZkTKRI4FQrDHBSZUlql3RIK9o30TtbfBSwE7TS-5r4nLZiN9k_ZDtZxZp-lj5dAuXApL4nxiI_6wVkSqxOJkNoi1-Q54iow2jRqNZ1DnKSrY72mjBOr5N0l2vR9FKuHzhPfd65EXuSuyMCZPwURSX-3Ilox-HLWsptvWUDogUkDl8MrNEqDqOVM0pylHeHMJZnR_VYeAmvCq1TMha2nn7U_Z49oEFR9lyBofleB5vIGD3fRqFGIPliQCpKbQolBCdMJgzoyTOIswwoTrUOtpMwk28NLtd8GfJQvJi0zHybNdt3HPYWPp80HEBVfKPDJgkoACh_6QjrDSQDpMPfRKcGrgFkzueQY07tky0MbderFnlaOBn5UPyQ_Fsz1m0Vc1i5WHqauOC3HLdQMxT2V3Zh9iJ0pQBiwmIH48YliiH6nb_hgCEMYIzfD7CWdUvt0DNOc9Byzu9O_JowW3Hd9JN1kPPd8Nj5GwGf99l75jalACiCJX8dQeNIojyoRCFG5cxm_t9HMIsvkGlIqUoyje5Q7ZA6SukSsc8r0myaO5XzXH-YOOK1XkGT95p5nvtae9vI4cqY-eY-bEHojdk8Hf-fASr_K32uCHPMZcw0BeaINFk4xKeKq4urDGxNbRhsmOsbtJi0zbCSdBQzn3Of8iKD18HYxaEEK8foiGfBKo5vUC1HcQevz7CNLMQ1SWrUpsMmB-nKqhTpFWeYMlr2X3ZXvhH9mfZiQ6UB47xd-po9W_-b_id-oXrf_2oDocGpy6rFb4ctA26JK8QgAyVFZ4cxxjJEtQ_qF_FXuts7z24dP1Bvln5bN16A0wMadKGBZraffNlAaMOjh-JN4Evs_t_DYMmkhO5LZFDqRCmS7MZumPNReM912OvO7U28EnaTtJP9FTSdumNCnL_lOV2_VnzlP1h_oIWeyCRHaHzjh6dDWz4fweEDbMEtf7ALqZemyurDpojqCutVaYxrjKuX6hq03_yZMVqCoD5YwZpGX3_ksxY32XpZRNkFV4BjxqONaoRlj2yTLgGvTHISoIOlR2aHKEfqCelK9Um1yDibthTAFQFRftpCnnJZ-BTzW8OaBF89obidCKpDIgptxWJLopCkRCbQ8AnyfqGDZMZmkGSQ4xO50HMaqdr0kvLac5IAFPDZuBW11_ahRFpGJr3Vuxq_nn6hiRk2GTrcfR5H3AhaiyOOpFLrhikTI8ulDa-Pc473Wi9WMUn3GzHVdlK008AY_Z403Hag-5c4UsWatFe81cNoBd9B4UEpBKV86f7ihi-M7k3mSy1MrNWtjejEck5r1THUb9JuS3yUM4002PPVdBXwH7LV8Ne0EkYatZtFXnfcQBlKH8ejiuND2Ludfx6A6n6q_S2F7wtsy_aMJEWzDi0PcNrxmjsP_NKyzLtKLQ7wkDDb_E"
		});
		ByteArrayContent bt = new ByteArrayContent(str.ToArray());
		bt.Headers.TryAddWithoutValidation("Content-Type", "application/x-protobuffer");
		RestResponse<string> resp = await MspApi.PostAsync("https://www.google.com/recaptcha/api2/reload?k=6LcxuOsUAAAAAI2IYDfxOvAZrwRg2T1E7sJq96eg", bt);
		if (!resp.Success)
		{
			return null;
		}
		return resp.Result.Split(',')[1].Replace("\"", "");
	}

	public static async Task<RestResponse<NebulaCreatedAccount>> CreateAsync(string username, string password, Server server, NebulaGameType game = NebulaGameType.MSP_5ooi, WebProxy proxy = null)
	{
		string captcha = await GetRecaptchaTokenAsync();
		RestResponse<string> res = await MspApi.PostAsync("https://" + ((server == Server.UnitedStates) ? "us" : "eu") + ".mspapis.com/edgelogins/graphql", JsonConvert.SerializeObject(new GrapqlObject
		{
			query = "mutation create ($loginName: String!, $password: String!, $gameId: String!, $isGuest: Boolean!, $countryCode: Region!, $checksum: String!, $recaptchaV3Token: String ){createLoginProfile(input: { name: $loginName, password: $password, gameId: $gameId, region: $countryCode, isGuest: $isGuest }, verify: {checksum: $checksum, recaptchaV3Token: $recaptchaV3Token } ) {success,loginProfile {loginId,loginName,profileId,profileName,isGuest},error}}",
			variables = "{\"checksum\": \"" + Hash.HashContent(username, password, server.ToServerCode(isUpperCase: true), game) + "\", \"loginName\": \"" + username + "\", \"password\": \"" + password + "\", \"gameId\": \"" + game.ToString().Replace("MSP_", "") + "\", \"isGuest\": false, \"countryCode\": \"" + server.ToServerCode(isUpperCase: true) + "\", \"recaptchaV3Token\": \"" + captcha + "\"}",
			operationName = ""
		}), proxy);
		if (!res.Success || res.Result.StartsWith("{\"errors\":"))
		{
			return new RestResponse<NebulaCreatedAccount>
			{
				Result = new NebulaCreatedAccount
				{
					Error = (string.IsNullOrEmpty(res.Result) ? res.Exception.Message : res.Result),
					Success = false
				},
				Success = false
			};
		}
		NebulaCreatedAccount json = JsonConvert.DeserializeObject<NebulaCreatedAccount>(JObject.Parse(res.Result)["data"]!["createLoginProfile"]!.ToString());
		NebulaCreatedAccount profile = JsonConvert.DeserializeObject<NebulaCreatedAccount>(JObject.Parse(res.Result)["data"]!["createLoginProfile"]!["loginProfile"]!.ToString());
		if (profile != null)
		{
			profile.Username = username;
			profile.Password = password;
			profile.Server = server;
		}
		return new RestResponse<NebulaCreatedAccount>
		{
			Exception = res.Exception,
			Json = res.Json,
			Request = res.Request,
			Response = res.Response,
			Result = ((res.Success && json.Success) ? profile : new NebulaCreatedAccount
			{
				Error = json.Error,
				Success = false
			}),
			Success = (res.Success && json.Success)
		};
	}
}
