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

namespace RuneterraCompanion.Handlers
{
    public class GameRequestHandler
    {
        public async Task<StaticDeckList> GetStaticDeckList()
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    var result = await client.DownloadStringTaskAsync(StaticDeckListUrl);
                    var retVal = JsonConvert.DeserializeObject<StaticDeckList>(result);
                    retVal.IsSuccess = true;
                    return retVal;
                }
                catch(WebException)
                {
                    return new StaticDeckList()
                    {
                        IsSuccess = false
                    };
                }
            }
        }

        public async Task<PositionalRectangles> GetPositionalRectangles()
        {
            using (WebClient client = new WebClient())
            {
                return JsonConvert.DeserializeObject<PositionalRectangles>(await client.DownloadStringTaskAsync(PositionalRectanglesUrl));
            }
        }

        private string GetPort => ((MainWindow)Application.Current.MainWindow).Configuration.Port.ToString();
        private string BaseUrl => Constants.Protocol + Constants.Host + ':' + GetPort;
        private string PositionalRectanglesUrl => BaseUrl + Constants.PathToPositionalRectangles;
        private string StaticDeckListUrl => BaseUrl + Constants.PathToStaticDeckList;
    }
}
