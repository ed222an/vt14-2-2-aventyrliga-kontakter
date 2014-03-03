﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdventurousContacts.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Kundkontakter (version 1, ADO.NET)</title>
    <link href="Content/reset.css" rel="stylesheet" />
    <link href="Content/site.css" rel="stylesheet" />
</head>
<body>
    <div id="page">
        <div id="main">
            <form id="theForm" runat="server">
            <h1>
                Kontakter
            </h1>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fel inträffade. Korrigera det som är fel och försök igen."
            CssClass="validation-summary-errors" />
            <%-- 
                    Visar alla kontakter.
                    Hämtar alla kontaktuppgifter som finns i tabellen Contact i databasen via affärslogikklassen Service och 
                    metoden GetContacts, som i sin tur använder klassen ContactDAL och metoden GetContacts, som skapar en
                    lista med referenser till Contact-objekt; ett Contact-objekt för varje post i tabellen. 
            --%>
            <asp:ListView ID="ContactListView" runat="server"
                ItemType="AdventurousContacts.Model.Contact"
                SelectMethod="ContactListView_GetData"
                InsertMethod="ContactListView_InsertItem"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactID"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table class="grid">
                        <tr>
                            <th>
                                Föramn
                            </th>
                            <th>
                                Efternamn
                            </th>
                            <th>
                                Email
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <asp:Label ID="FirstNameLabel" runat="server" Text='<%#: Item.FirstName %>' />
                        </td>
                        <td>
                            <asp:Label ID="LastNameLabel" runat="server" Text='<%#: Item.LastName %>' />
                        </td>
                        <td>
                            <asp:Label ID="EmailAddressLabel" runat="server" Text='<%#: Item.EmailAddress %>' />
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kontaktuppgifter. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då kontaktuppgifter saknas i databasen. --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Kontaktuppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kontaktuppgifter. Visas bara om InsertItemPosition 
                     har värdet FirstItemPosition eller LasItemPosition.--%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="EmailAddress" runat="server" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att lägga till en ny kontaktuppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kontaktuppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="EmailAddress" runat="server" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
            </form>
        </div>
    </div>
</body>
</html>