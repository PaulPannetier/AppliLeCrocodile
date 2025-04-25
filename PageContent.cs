

namespace AppliLeCrocodile
{
    internal class PageContent
    {
        protected MainPage mainPage;

        public Layout content {  get; protected set; }  
        public string title {  get; protected set; }

        public PageContent(MainPage mainPage)
        {
            this.mainPage = mainPage;
        }

        public virtual void Start()
        {

        }
    }
}
