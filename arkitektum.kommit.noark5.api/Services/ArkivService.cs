using System.Linq;

namespace arkitektum.kommit.noark5.api.Services
{
    public class ArkivService
    {
        private readonly MockNoarkDatalayer _ctx;

        public ArkivService(MockNoarkDatalayer ctx)
        {
            _ctx = ctx;
        }

        public ArkivType GetArkiv(string id)
        {
            return _ctx.Arkiver.FirstOrDefault(i => i.systemID == id);
        }

        public ArkivskaperType GetArkivskaper(string id)
        {
            return _ctx.Arkivskaper.FirstOrDefault(i => i.systemID == id);
        }
    }
}