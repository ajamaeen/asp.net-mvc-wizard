namespace Wizard.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wizard.Model;

    public class BookmarkView
    {
        private readonly Bookmark<WizardStep> bookMark;

        public BookmarkView(Bookmark<WizardStep> bookMark)
        {
            this.bookMark = bookMark;
        }

        public bool Active
        {
            get
            {
                return this.bookMark.State == BookmarkState.Enabled ||
                       this.bookMark.State == BookmarkState.Current;

            }
        }

        public string Text
        {
            get
            {
                return this.bookMark.Text;
            }
        }
        
        public String Id
        {
            get
            {
                return this.bookMark.Id;
            }
        }

        public String CssClass
        {
            get
            {
                if (this.bookMark.State == BookmarkState.Disabled)
                {
                    return "nav-item nav-link disabled";
                }

                if (this.bookMark.State == BookmarkState.Current)
                {
                    return "nav-item nav-link active";
                }

                if (this.bookMark.State == BookmarkState.Enabled)
                {
                    return "nav-item nav-link";
                }

                return "";
            }
        }
    }
}
