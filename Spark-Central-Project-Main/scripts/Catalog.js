

function deleteBook(id) {
    PageMethods.deleteBookClick(id);
    location.reload();

}

function deleteDVD(id) {
    PageMethods.deleteDVDClick(id);
    location.reload();
}

function deleteTechnology(id) {
    PageMethods.deleteTechClick(id);
    location.reload();

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


    $('table').DataTable();


});