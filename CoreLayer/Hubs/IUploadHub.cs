using System.Threading.Tasks;

namespace CoreLayer.Hubs
{
    public interface IUploadHub
    {
        Task SendProgress(string fileName,int progress);
    }
}