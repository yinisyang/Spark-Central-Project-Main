


function deleteMember(id) {
    PageMethods.deleteClick(id);
    location.reload();

}

function editMember(e){
    if (!e)
        e = window.event;
    var sender = e.srcElement || e.target;

    while (sender && sender.nodeName.toLowerCase() != "button")
        sender = sender.parentNode;

    var id = sender.id;
    window.location.href = "EditMember.aspx?memberid=" + id;
}

$(document).ready(function () {

    var dialog = document.querySelector('dialog');
    var showDialogButton = document.querySelector('#show-dialog');
    if (!dialog.showModal) {
        dialogPolyfill.registerDialog(dialog);
    }
    showDialogButton.addEventListener('click', function () {
        dialog.showModal();
    });
    dialog.querySelector('.close').addEventListener('click', function () {
        dialog.close();
    });

    $('table').DataTable({
        processing: true,
        serverSide: true,
        stateSave: true,
        columnDefs: [{ orderable: false, targets: [1,2,3,4,5,6,7] }],
        ajax: {
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/Members.aspx/GetData",
            data: function (d) {
                return JSON.stringify({ parameters: d });
            }
        }
    });

});
