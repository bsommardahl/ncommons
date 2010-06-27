<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<NHibernateWebPersistenceExample.Models.Product>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Id</div>
        <div class="display-field"><%: Model.Id %></div>
        
        <div class="display-label">Description</div>
        <div class="display-field"><%: Model.Description %></div>
        
        <div class="display-label">Price</div>
        <div class="display-field"><%: String.Format("{0:F}", Model.Price) %></div>
        
    </fieldset>
    <p>
        <%: Html.ActionLink("Edit", "Edit", new {  Id=Model.Id }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</body>
</html>

