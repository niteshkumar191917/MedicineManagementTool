using Microsoft.AspNetCore.Components;

namespace MedicineManagementTool.UI.Pages
{
    public partial class Pagination
    {
        [Parameter]
        public int CurrentPage { get; set; } = 1;
        [Parameter]
        public int TotalPageQuantity { get; set; }
        [Parameter]
        public int Radius { get; set; } = 1;
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }
        List<LinkModel> links;
        protected override void OnParametersSet()
        {
            LoadPages();
        }
        private async Task SelectedPageInternal(LinkModel link)
        {
            if (link.Page == CurrentPage) { return; }
            if (!link.Enabled) { return; }
            CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
        private void LoadPages()
        {
            links = new List<LinkModel>();
            var isPreviousPageLinkEnabled = CurrentPage != 1;
            var previousPage = CurrentPage - 1;
            if (previousPage != 0)
                links.Add(new LinkModel(previousPage, isPreviousPageLinkEnabled, "Pre"));

            links.Add(new LinkModel(1, true, $"{1}") { Active = CurrentPage == 1 });
            if (CurrentPage != 1 && CurrentPage != 2 && CurrentPage != 3)
                links.Add(new LinkModel(0, false, "..."));
            for (int i = 2; i <= TotalPageQuantity - 1; i++)
            {
                if (i >= CurrentPage - Radius && i <= CurrentPage + Radius)
                {
                    links.Add(new LinkModel(i) { Active = CurrentPage == i });
                }
            }
            var isNextPageLinkEnable = CurrentPage != TotalPageQuantity;
            var lastPage = TotalPageQuantity;
            var nextPage = CurrentPage + 1;
            if (CurrentPage != lastPage && CurrentPage != lastPage - 1 && CurrentPage != lastPage - 2)
                links.Add(new LinkModel(0, false, "..."));
            if (2 <= TotalPageQuantity)
                links.Add(new LinkModel(lastPage, true, $"{lastPage}") { Active = CurrentPage == lastPage });
            if (nextPage != TotalPageQuantity + 1)
                links.Add(new LinkModel(nextPage, isNextPageLinkEnable, "Next"));
        }
        public class LinkModel
        {
            public LinkModel(int page)
                : this(page, true) { }
            public LinkModel(int page, bool enabled)
                 : this(page, enabled, page.ToString()) { }
            public LinkModel(int page, bool enabled, string text)
            {
                Page = page;
                Enabled = enabled;
                Text = text;
            }
            public int Page { get; set; }
            public string Text { get; set; }
            public bool Enabled { get; set; } = true;
            public bool Active { get; set; } = false;
        }
    }
}