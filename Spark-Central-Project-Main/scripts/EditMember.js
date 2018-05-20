function deleteMember() {
    swal({
        title: 'Delete Member',
        text: 'Are you sure you want to delete this member?',
        icon: 'warning',
        buttons: true,
        dangerMode: true,
    }).then((value) => {
        if (value) {
            swal('Member deleted', { icon: 'success', });
            deleteConfirm();
        } else {
            return false;
        }

    })
} 

function deleteConfirm() {

    PageMethods.deleteMem(getParameterByName("memberid", false));
    window.location.href = "Members.aspx";

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