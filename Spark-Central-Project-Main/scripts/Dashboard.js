$(document).ready(function () {

    var smartDialog = document.getElementById("smartDialog");
    var showDialogButton = document.getElementById("btnSmart");

    if (!smartDialog.showModal) {
        dialogPolyfill.registerDialog(smartDialog);
    }
    if (showDialogButton) {
        showDialogButton.addEventListener('click', function () {
            console.log("Clicked");
            smartDialog.showModal();
        }, false);
    }
        smartDialog.querySelector('.close').addEventListener('click', function () {
            smartDialog.close();
        });
    var coDialog = document.getElementById("checkOutDialog");
    var coButton = document.getElementById('checkOutButton');
    if (!coDialog.showModal) {
        dialogPolyfill.registerDialog(coDialog);
    }
    coButton.addEventListener('click', function () {
        coDialog.showModal();
    });
    coDialog.querySelector('.close').addEventListener('click', function () {
        coDialog.close();
    });

    var dialog = document.getElementById('member-dialog');
    var memDialogButton = document.getElementById('addMemButton');
    if (!dialog.showModal) {
        dialogPolyfill.registerDialog(dialog);
    }
    memDialogButton.addEventListener('click', function () {
        dialog.showModal();
    });
    dialog.querySelector('.close').addEventListener('click', function () {
        dialog.close();
    });


});