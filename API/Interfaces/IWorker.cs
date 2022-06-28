namespace API.Interfaces
{
    public interface IWorker
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}