using System.Threading.Tasks;

namespace CodeOfEverything.src.Services.Tasks
{
    public interface ITaskFactory
    {
        public Task<int> Launch();
    }
}