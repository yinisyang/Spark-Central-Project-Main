function deleteTech() {
    swal({
        title: 'Delete Technology',
        text: 'Are you sure you want to delete this technology?',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    }).then((value) => {
        if (value) {
            swal('Technology deleted', { icon: 'success', });
            deleteConfirm();
        } else {
            return false;
        }

    })
}

function deleteConfirm() {

    PageMethods.deleteTe(getParameterByName("item_id", false));
    window.location.href = "Technology.aspx";

}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$(document).ready(function () {

});