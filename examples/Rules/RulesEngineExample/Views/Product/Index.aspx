<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<RulesEngineExample.Domain.Product>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>
    <table>
        <tr>           
            <th>
                Id
            </th>
            <th>
                Description
            </th>
            <th>
                Price
            </th>
             <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>          
            <td>
                <%: item.Id %>
            </td>
            <td>
                <%: item.Description %>
            </td>
            <td>
                <%: String.Format("{0:F}", item.Price) %>
            </td>
              <td>
                <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.Id }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.Id })%> |--%>
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Id })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</body>
</html>

