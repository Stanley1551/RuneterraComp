using RuneterraCompanion.Common;
using RuneterraCompanion.Handlers;
using RuneterraCompanion.ResponseModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RuneterraCompanion.Factory
{
    public static class GameRequestFactory
    {
        public static async Task<IGameResponse> Get(Enums.RequestType type)
        {
            switch (type)
            {
                case Enums.RequestType.StaticDeckList:
                    return await new GameRequestHandler().GetStaticDeckList();
                case Enums.RequestType.PositionalRectangles:
                    return await new GameRequestHandler().GetPositionalRectangles();
                default:
                    throw new ArgumentException();
            }
        }

        public static async Task<IGameResponse> Post(Enums.RequestType type)
        {
            throw new NotImplementedException();
        }
    }
}
