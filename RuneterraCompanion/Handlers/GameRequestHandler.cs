using RuneterraCompanion.Common;
using RuneterraCompanion.ResponseModels;
using RuneterraCompanion.ResponseModels.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Windows.Threading;
using RuneterraCompanion.Configuration.Interfaces;
using System.Threading;

namespace RuneterraCompanion.Handlers
{
    public class GameRequestHandler
    {
        public async Task<StaticDeckList> GetStaticDeckList()
        {
            StaticDeckList deckList = new StaticDeckList();

            try
            {
                var response = await MakeRequest(StaticDeckListUrl);
                deckList = DeserializeBody<StaticDeckList>(response);
                deckList.IsSuccess = true;
            }
            catch(Exception)
            {
                deckList.IsSuccess = false;
            }

            return deckList;
        }

        public async Task<PositionalRectangles> GetPositionalRectangles()
        {
            PositionalRectangles rectangles = new PositionalRectangles();

            try
            {
                var response = await MakeRequest(PositionalRectanglesUrl);
                rectangles = DeserializeBody<PositionalRectangles>(response);
                rectangles.IsSuccess = true;
            }
            catch (Exception)
            {
                rectangles.IsSuccess = false;
            }

            return rectangles;
        }

        private async Task<string> MakeRequest(string url)
        {
            string responseBody = string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    responseBody = await client.DownloadStringTaskAsync(url);
                }
                catch (WebException e)
                {
                    //TODO? Game is not started or port is not configured
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return responseBody;
        }

        private T DeserializeBody<T>(string body)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        //validation?
        private string GetPort => ((App)Application.Current).Configuration.Port.ToString();
        private string BaseUrl => Constants.Protocol + Constants.Host + ':' + GetPort;
        private string PositionalRectanglesUrl => BaseUrl + Constants.PathToPositionalRectangles;
        private string StaticDeckListUrl => BaseUrl + Constants.PathToStaticDeckList;
    }
}
