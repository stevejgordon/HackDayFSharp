using System;
using System.Dynamic;
using Models;
using StringTypes;

//using StringTypes;

namespace CSharp_client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var draftArticle = 
                ArticleImplementation.create(GetPerson("Steve", "J", "Gordon"), "title", "body");

            var pendingArticle = 
                ArticleImplementation.submitForApproval(draftArticle, GetPerson("Chris", "D", "Sargood"));

            var approvedArticle = 
                ArticleImplementation.approveArticle(pendingArticle, new ArticleModels.ApprovedArticleData("Vote for us"));

            var publishedArticle =
                ArticleImplementation.publishArticle(approvedArticle, DateTime.Now, DateTime.Now.AddMonths(1));

            var publishedArticleGoBang = 
                ArticleImplementation.publishArticle(pendingArticle, // hee hee evil coder!
                    DateTime.Now, DateTime.Now.AddMonths(1));
        }

        private static ArticleModels.Person GetPerson(string v, string a, string b)
        {
            return new ArticleModels.Person(Guid.NewGuid(), WrappedString.String50.NewString50("Steve"), WrappedString.String1.NewString1("A"), WrappedString.String50.NewString50("Steve"));
        }
    }

    var 
}
