<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditBook.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PanelPlaceHolder" runat="Server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab is-active">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/ManageAdmins.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <br />

    <div class="mdl-grid" style="width: 50%">
        <div class="mdl-cell mdl-cell--6-col">

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookTitle" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookTitle">Title</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookAuthor" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookAuthor">Author</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookPublisher" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookPublisher">Publisher</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookYear" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookYear">Publication Year</label>
            </div>


        </div>
        <div class="mdl-cell mdl-cell--6-col">

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookCategory" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookCategory">Category</label>
            </div>
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookPages" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookPages">Pages</label>
            </div>


            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookIsnb10" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookIsbn10">Isbn 10</label>
            </div>

            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                <asp:TextBox ID="bookIsbn13" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookIsbn13">Isbn 13</label>
            </div>
        </div>
        <div class="mdl-cell mdl-cell--12-col">
            <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label mdl-textfield--full-width">
                <asp:TextBox ID="bookDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                <label class="mdl-textfield__label" for="bookDescription">Description</label>
            </div>
        </div>
    </div>
    <div class="mdl-cell mdl-cell--6-col">
    </div>
    <div class="mdl-cell mdl-cell--6-col">
        <div class="mdl-dialog__actions">
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="Submit_Click" CssClass="mdl-button mdl-js-button mdl-button" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="Cancel_Click" CssClass="mdl-button mdl-js-button mdl-button" />

        </div>
    </div>


</asp:Content>

