<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DVD.aspx.cs" Inherits="Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="scripts/DVD.js"></script>
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

    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <br />

    <div class="mdl-grid" style="overflow-x: auto">


        <br />




        <!-- Catalog Tables -->



        <div id="tableDiv" style="padding-left: 5%; overflow-x: auto; width: 75%">
            <asp:Label ID="NoteLabel" class="mdl-list__item-primary-content" Style="color: darkcyan" runat="server" Text=""></asp:Label>
            <asp:Table ID="table" class="mdl-data-table" runat="server">
            </asp:Table>

        </div>


        <div class="mdl-cell mdl-cell--1-col">



            <button id="btnSmartAddBook" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Smart Add">
                <i class="material-icons">grade</i>
            </button>
            <br />
            <asp:ImageButton ID="BookImage" Title="Books" Height="80" ImageUrl="images/book.png" runat="server" OnClick="buttonBooks_Click" />
            <!-- Add Book Button -->
            <button id="btnAddBook" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Add Book">
                <i class="material-icons">add</i>
            </button>



            <br />
            <br />
            <asp:ImageButton ID="DVDImage" Title="DVDs" Height="80" ImageUrl="images/dvdS.png" runat="server" OnClick="buttonDVD_Click" />
            <!-- Add Dvd Button -->
            <button id="btnAddDvd" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Add DVD">
                <i class="material-icons">add</i>
            </button>


            <br />
            <br />
            <asp:ImageButton ID="TechImage" Title="Technology" Height="80" ImageUrl="images/technology.png" runat="server" OnClick="buttonTech_Click" />
            <!-- Add Tech Button -->
            <button id="btnAddTech" type="button" class="mdl-button mdl-js-button mdl-button--icon" title="Add Tech">
                <i class="material-icons">add</i>
            </button>
            <br />


        </div>

    </div>



    <!--
            Book Dialog
            -->
    <dialog class="mdl-dialog" id="bookDialog" style="width: 50%">
        <h4 class="mdl-dialog__title">Add Book</h4>
        <div class="mdl-dialog__content">

            <div class="mdl-grid" style="width: 90%">
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
                        <asp:TextBox ID="bookDescription" TextMode="MultiLine" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="bookDescription">Description</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <asp:Button ID="SubmitBook" runat="server" Text="Add Book" OnClick="SubmitBook_Click" CssClass="mdl-button mdl-js-button mdl-button" />
            <button type="button" class="mdl-button close">Cancel</button>
        </div>

    </dialog>
    <!--
            End Book Dialog
            -->



    <!--
            DVD Dialog
            -->



    <dialog class="mdl-dialog" id="dvdDialog" style="width: 30%">
        <h4 class="mdl-dialog__title">Add DVD</h4>
        <div class="mdl-dialog__content">

            <div class="mdl-grid" style="width: 90%">
                <div class="mdl-cell mdl-cell--6-col">


                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="dvdTitle" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="dvdTitle">Title</label>
                    </div>

                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="dvdYear" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="dvdYear">Release Year</label>
                    </div>

                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="dvdRating" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="dvdRating">Rating</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <asp:Button ID="SubmitDvd" runat="server" Text="Add DVD" OnClick="SubmitDvd_Click" CssClass="mdl-button mdl-js-button mdl-button" />
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>

    <!--
            End DVD Dialog
            -->



    <!--
            Tech Dialog
            -->

    <dialog class="mdl-dialog" id="techDialog" style="width: 30%">
        <h4 class="mdl-dialog__title">Add Tech</h4>
        <div class="mdl-dialog__content">

            <div class="mdl-grid" style="width: 90%">
                <div class="mdl-cell mdl-cell--6-col">
                    <div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label">
                        <asp:TextBox ID="techName" runat="server" CssClass="mdl-textfield__input"></asp:TextBox>
                        <label class="mdl-textfield__label" for="techName">Name</label>
                    </div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <asp:Button ID="SubmitTech" runat="server" Text="Add Technology" OnClick="SubmitTech_Click" CssClass="mdl-button mdl-js-button mdl-button" />
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>

    <!--
            End Tech Dialog
            -->
    <!-- Smart Add Dialog -->
    <dialog class="mdl-dialog" id="smartDialog" style="width: 30%; top: 2%">
        <h4 class="mdl-dialog__title">Smart Add</h4>
        <div class="mdl-dialog__content">
            <div class="mdl-grid" id="smartGrid" style="width: 90%">
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

                <div class="mdl-cell mdl-cell--4-col"></div>
                <div class="mdl-cell mdl-cell--4-col">
                    <img id="sbookPic" src="images/empty.png" />
                </div>
                <div class="mdl-cell mdl-cell--4-col"></div>

                <div class="mdl-cell mdl-cell--6-col">
                    <div id="sbookTitle"></div>
                    <div id="sbookAuthor"></div>
                    <div id="sbookPublisher"></div>
                    <div id="sbookYear"></div>

                </div>
                <div class="mdl-cell mdl-cell--6-col">
                    <div id="sbookPages"></div>
                    <div id="sbookCategory"></div>
                    <div id="sbookIsbn10"></div>
                    <div id="sbookIsbn13"></div>

                </div>
                <div class="mdl-cell mdl-cell--12-col">
                    <div id="sbookDescription"></div>
                </div>
            </div>
        </div>
        <div class="mdl-dialog__actions">
            <button id="btnAdd" type="button" onclick="addBook()" class="mdl-button mdl-js-button mdl-button" title="Add">
                Add
            </button>
            <button id="btnSearch" type="button" onclick="findGoogleBook()" class="mdl-button mdl-js-button mdl-button" title="Search">
                Search
            </button>
            <button type="button" class="mdl-button close">Cancel</button>
        </div>
    </dialog>
    <!---End Smart Add Dialog-->
</asp:Content>

