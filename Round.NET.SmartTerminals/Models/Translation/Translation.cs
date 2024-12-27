using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace Round.NET.SmartTerminals.Models.Translation
{
	internal class Translation
	{
		public static void InitTranslationCore()
		{
			Translation.Token = Translation.GetAPIToken();
			bool flag = Translation.FlushedTokenThread != null;
			if (flag)
			{
				try
				{
					Translation.FlushedTokenThread.Start();
				}
				catch
				{
				}
			}
			else
			{
				Translation.FlushedTokenThread = new Thread(delegate()
				{
					for (;;)
					{
						Thread.Sleep(60000);
						Translation.Token = Translation.GetAPIToken();
					}
				});
			}
		}
		public static string GetAPIToken()
		{
			string result;
			using (HttpClient client = new HttpClient())
			{
				try
				{
					client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
					client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
					client.DefaultRequestHeaders.ConnectionClose = new bool?(false);
					client.DefaultRequestHeaders.UserAgent.ParseAdd("RoundSmartTerminals/ver1 (https://round-studio.github.io)");
					HttpResponseMessage response = client.GetAsync("https://edge.microsoft.com/translate/auth").Result;
					response.EnsureSuccessStatusCode();
					string responseBody = response.Content.ReadAsStringAsync().Result;
					result = responseBody;
				}
				catch (HttpRequestException e)
				{
					result = null;
				}
			}
			return result;
		}

		public static string GetTextForJson(string Json)
		{
			JArray jsonArray = JArray.Parse(Json);
			foreach (JToken jtoken in jsonArray)
			{
				JObject item = (JObject)jtoken;
				JObject detectedLanguage = item["detectedLanguage"] as JObject;
				JArray translations = item["translations"] as JArray;
				using (IEnumerator<JToken> enumerator2 = translations.GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						JObject translation = (JObject)enumerator2.Current;
						string result;
						if (translation == null)
						{
							result = null;
						}
						else
						{
							JToken jtoken2 = translation["text"];
							result = ((jtoken2 != null) ? jtoken2.ToString() : null);
						}
						return result;
					}
				}
			}
			return null;
		}

		public static string TranslationCore(string Message)
		{
			string result;
			try
			{
				HttpClient client = new HttpClient();
				client.Timeout = TimeSpan.FromMilliseconds(-1.0);
				client.BaseAddress = new Uri("https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to=" + "zh_cn" + "&textType=plain");
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "");
				request.Content = new StringContent("[{\"Text\":\"" + Message + "\"}]", Encoding.UTF8, "application/json");
				request.Headers.UserAgent.ParseAdd("RoundSmartTerminals/ver1 (https://round-studio.github.io)");
				request.Headers.Add("Authorization", "Bearer " + Translation.Token);
				request.Headers.Add("Accept", "*/*");
				request.Headers.Add("Cache-Control", "no-cache");
				request.Headers.Add("Host", "api.cognitive.microsofttranslator.com");
				request.Headers.Add("Connection", "keep-alive");
				HttpResponseMessage response = client.SendAsync(request).Result;
				response.EnsureSuccessStatusCode();
				string responseBody = response.Content.ReadAsStringAsync().Result;
				result = Translation.GetTextForJson(responseBody);
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public static string Token;
		private static Thread FlushedTokenThread;
	}
}
