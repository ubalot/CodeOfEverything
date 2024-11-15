using System.Threading.Tasks;

namespace CodeOfEverything.Services.Tasks
{
    public interface ITaskFactory
    {
        public Task<int> Launch();
    }
}