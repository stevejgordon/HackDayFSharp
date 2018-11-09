namespace Models

module ArticleModels =
  open StringTypes.WrappedString
  open System

    type ArticleTitle = ArticleTitle of string
    and Image = Id of Guid
    and Category = { Id : Guid; Name : string }
    and Tag = Tag of String10
    
    type Person = {
        ID : Guid
        FirstName : String50
        MiddleInitial: String1 option
        LastName : String50 
    }
    
    type DraftArticleData = {
        Id: Guid
        Title: ArticleTitle
        Body: string
        Image : Image option
        Author : Person option
        Category : Category list
        Tag : Tag list option
    }
    and PendingArticleData = { Reviewer : Person }
    and ApprovedArticleData = { Feedback : string }
    and DeclinedArticleData = { Feedback : string }
    and PublishedArticleData = { StartDate: DateTime; EndDate: DateTime }

    type Article = 
        | Draft of DraftArticleData
        | Pending of DraftArticleData * PendingArticleData
        | Approved of DraftArticleData * PendingArticleData * ApprovedArticleData
        | Declined of DraftArticleData * PendingArticleData * DeclinedArticleData 
        | Published of DraftArticleData * PendingArticleData * ApprovedArticleData * PublishedArticleData
        | Archived of  DraftArticleData *  PendingArticleData * ApprovedArticleData * PublishedArticleData

module ArticleImplementation =
    open ArticleModels
    open System

    let createArticleTitle (s:string) =
       if (s.Split(' ').Length <=10)
           then Some (ArticleTitle s)
           else None

    let create person title body = 
        match (createArticleTitle title), (string body) with
        | Some f, l -> Draft {Title = f; Body = l; Image = None; Author = person; 
        Category = list.Empty; Tag = None; Id = Guid.NewGuid(); }
        | _ -> failwith "Invalid Article content"
        
    let submitForApproval article reviewer = 
        match article with
        | Draft(d) -> Pending(d, {Reviewer = reviewer;})
        | _ -> failwith "Cant sumbit for approval - invalid state"

    let approveArticle article feedback = 
        match article with
        | Pending(d, p) -> Approved(d, p, feedback)
        | _ -> failwith "Cant approve - invalid state"

    let declineArticle article feedback = 
        match article with
        | Pending(d, p) -> Declined(d, p, feedback)
        | _ -> failwith "Cant decline - invalid state"
    
    let publishArticle article startDate endDate =
        match article with 
        | Approved(d, p, a) -> Published(d, p, a, { StartDate = startDate; EndDate = endDate })
        | _ -> failwith "Cant publish - invalid state"