using DataLayer.Entities;

namespace ServiceLayer.Interfaces
{
    public interface IContentService
    {
        MainContent Get();

        void ChangeTitle(string title);
    }
}
