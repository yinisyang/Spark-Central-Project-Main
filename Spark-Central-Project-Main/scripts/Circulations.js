
$(document).ready(function () {

    var coDialog = document.getElementById("coDialog");
    var showCoDialogButton = document.getElementById('btnAddCo');
    if (!coDialog.showModal) {
        dialogPolyfill.registerDialog(coDialog);
    }
    showCoDialogButton.addEventListener('click', function () {
        coDialog.showModal();
    });
    coDialog.querySelector('.close').addEventListener('click', function () {
        coDialog.close();
    });


    var fineDialog = document.getElementById("fineDialog");
    var showFineDialogButton = document.getElementById('btnAddFine');
    if (!fineDialog.showModal) {
        dialogPolyfill.registerDialog(fineDialog);
    }
    showFineDialogButton.addEventListener('click', function () {
        fineDialog.showModal();
    });
    fineDialog.querySelector('.close').addEventListener('click', function () {
        fineDialog.close();
    });


    $('table').DataTable();
});