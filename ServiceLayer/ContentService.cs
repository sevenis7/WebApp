using DataLayer.Entities;
using DataLayer.Repositories;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class ContentService : IContentService
    {
        private readonly IContent _content;

        public ContentService(IContent content)
        {
            _content = content;
        }

        public void ChangeTitle(string title)
        {
            _content.Get().Title = title;
            _content.Update();
        }

        public MainContent Get()
        {
            return _content.Get();
        }
    }
}
