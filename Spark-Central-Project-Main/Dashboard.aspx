<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/Dashboard.js"></script>
    <style>
        .demo-card-square.mdl-card {
            width: 330px;
            height: 330px;
        }

        .demo-card-square > .mdl-card__title {
            color: dimgrey;
        }
    </style>


</asp:Content>

<asp:Content ID="Panel" ContentPlaceHolderID="PanelPlaceHolder" runat="server">
    <asp:Panel ID="pane" class="mdl-layout__tab-bar mdl-js-ripple-effect" runat="server">
        <a href="/Dashboard.aspx" class="mdl-layout__tab is-active">Dashboard</a>
        <a href="/Members.aspx" class="mdl-layout__tab">Members</a>
        <a href="/Catalog.aspx" class="mdl-layout__tab">Catalog</a>
        <a href="/Circulations.aspx" class="mdl-layout__tab">Circulations</a>
        <a href="/Reports.aspx" class="mdl-layout__tab">Reports</a>
        <a href="/Manage.aspx" class="mdl-layout__tab">Manage</a>
    </asp:Panel>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <div class="mdl-grid" style="width: 70%">
        <div class="mdl-cell mdl-cell--4-col">

            <button id="btnSmart" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Smart Add">
                <i class="material-icons">add</i>
            </button>

            <div class="demo-card-square mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Members</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Add
                    </a>
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">View All
                    </a>
                </div>
            </div>



        </div>
        <div class="mdl-cell mdl-cell--4-col">


            <div class="demo-card-square mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Checkouts</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Check-Out
                    </a>
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">Check-In
                    </a>
                </div>
            </div>



        </div>
        <div class="mdl-cell mdl-cell--4-col">



            <div class="demo-card-square mdl-card mdl-shadow--2dp">
                <div class="mdl-card__title mdl-card--expand">
                    <h2 class="mdl-card__title-text">Overdues</h2>
                </div>
                <div class="mdl-card__actions mdl-card--border">
                    <a class="mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect">View All
                    </a>
                </div>
            </div>



        </div>

    </div>

    <dialog class="mdl-dialog" id="smartDialog" style="width: 30%">
        <h4 class="mdl-dialog__title">Smart Add</h4>
        <div class="mdl-dialog__content">
            <div class="mdl-grid" style="width: 90%">
                <div class="mdl-cell mdl-cell--6-col">
                    <form action="#">
                        <div class="mdl-textfield mdl-js-textfield">
                            <input class="mdl-textfield__input" type="text" id="isbn">
                            <label class="mdl-textfield__label" for="isbn">Isbn</label>
                        </div>
                    </form>
                </div>
                <div class="mdl-cell mdl-cell--6-col">
                </div>




                <div class="mdl-cell mdl-cell--6-col">
                    <div id="bookTitle"></div>
                    <div id="bookAuthor"></div>
                    <div id="bookPublisher"></div>
                    <div id="bookYear"></div>

                </div>
                <div class="mdl-cell mdl-cell--6-col">
                    <div id="bookPages"></div>
                    <div id="bookCategory"></div>
                    <div id="bookIsbn10"></div>
                    <div id="bookIsbn13"></div>

                </div>
                <div class="mdl-cell mdl-cell--12-col">
                    <div id="bookDescription"></div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <button id="btnAdd" type="button" onclick="addBook()" class="mdl-button mdl-js-button mdl-button" title="Add">
                Add
            </button>
            <button id="btnSearch" type="button" onclick="loadDoc()" class="mdl-button mdl-js-button mdl-button" title="Search">
                Search
            </button>
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>




</asp:Content>




