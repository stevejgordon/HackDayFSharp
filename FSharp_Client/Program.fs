open Models
open System
open Models.ArticleModels
open Models.ArticleImplementation
open StringTypes.WrappedString

[<EntryPoint>]
let main argv =
    let draftArticle = ArticleImplementation.create "1 2 3 4 5 6 7 8 9" "Body"

    //service - get person using Id
    let pendingArticle = submitForApproval draftArticle {ID = Guid.NewGuid(); FirstName = (string50 "Chris").Value; MiddleInitial = None; LastName = (string50 "Sargood").Value;}

    let approvedArticle = approveArticle pendingArticle { Feedback = "This is amazing" }

    let publishedArticle = publishArticle approvedArticle  (DateTime.Now.AddSeconds(-1.00)) (DateTime.Now.AddMonths(1))
    0 // return an integer exit code
