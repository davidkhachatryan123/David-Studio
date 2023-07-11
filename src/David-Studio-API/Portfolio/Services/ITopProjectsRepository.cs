namespace Portfolio.Services
{
    public interface ITopProjectsRepository
    {
        Task<int[]> MarkAsTop(int[] ids);
    }
}

