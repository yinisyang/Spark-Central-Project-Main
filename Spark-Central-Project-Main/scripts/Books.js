
function editBook(e) {
    if (!e)
        e = window.event;
    var sender = e.srcElement || e.target;

    while (sender && sender.nodeName.toLowerCase() != "button")
        sender = sender.parentNode;

    var id = sender.id;
    window.location.href = "EditBook.aspx?item_id=" + id;
}


$(document).ready(function () {

    var dvdDialog = document.getElementById("dvdDialog");
    var showDvdDialogButton = document.getElementById('btnAddDvd');
    if (!dvdDialog.showModal) {
        dialogPolyfill.registerDialog(dvdDialog);
    }
    showDvdDialogButton.addEventListener('click', function () {
        dvdDialog.showModal();
    });
    dvdDialog.querySelector('.close').addEventListener('click', function () {
        dvdDialog.close();
    });

    var bookDialog = document.getElementById("bookDialog");
    var showBookDialogButton = document.getElementById('btnAddBook');
    if (!bookDialog.showModal) {
        dialogPolyfill.registerDialog(bookDialog);
    }
    showBookDialogButton.addEventListener('click', function () {
        bookDialog.showModal();
    });
    bookDialog.querySelector('.close').addEventListener('click', function () {
        bookDialog.close();
    });


    var techDialog = document.getElementById("techDialog");
    var showTechDialogButton = document.getElementById('btnAddTech');
    if (!techDialog.showModal) {
        dialogPolyfill.registerDialog(techDialog);
    }
    showTechDialogButton.addEventListener('click', function () {
        techDialog.showModal();
    });
    techDialog.querySelector('.close').addEventListener('click', function () {
        techDialog.close();
    });

    var smartAddDialog = document.getElementById("smartDialog");
    var showSmartAddDialogButton = document.getElementById('btnSmartAddBook');
    if (!smartAddDialog.showModal)
        dialogPolyfill.registerDialog(smartAddDialog);

    showSmartAddDialogButton.addEventListener('click', function () {
        console.log("Clicked");
        smartAddDialog.showModal();
    });
    smartAddDialog.querySelector('.close').addEventListener('click', function () {
        smartAddDialog.close();
    });


    $('table').DataTable({
        processing: true,
        serverSide: true,
        stateSave: true,
        columnDefs: [{ orderable: false, targets: [7] }],
        ajax: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/Books.aspx/GetData",
            data: function (d) {
                return JSON.stringify({ parameters: d });
            }
        }
    });


});