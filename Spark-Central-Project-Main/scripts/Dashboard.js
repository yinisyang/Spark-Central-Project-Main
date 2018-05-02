$(document).ready(function () {

    var smartDialog = document.getElementById("smartDialog");
    var showDialogButton = document.getElementById('btnSmart');
    if (!smartDialog.showModal) {
        dialogPolyfill.registerDialog(smartDialog);
    }
    showDialogButton.addEventListener('click', function () {
        smartDialog.showModal();
    });
    smartDialog.querySelector('.close').addEventListener('click', function () {
        smartDialog.close();
    });
});

function loadDoc() {
    var xhttp = new XMLHttpRequest();

    var isbn = document.getElementById("isbn").value;
    var url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn;

    xhttp.open("GET", url, true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var obj = JSON.parse(this.responseText)
            document.getElementById("bookTitle").innerHTML = obj.items[0].volumeInfo.title;
            document.getElementById("bookAuthor").innerHTML = obj.items[0].volumeInfo.authors[0];
            document.getElementById("bookPublisher").innerHTML = obj.items[0].volumeInfo.publisher;
            document.getElementById("bookYear").innerHTML = obj.items[0].volumeInfo.publishedDate;
            document.getElementById("bookPages").innerHTML = obj.items[0].volumeInfo.pageCount;
            document.getElementById("bookCategory").innerHTML = obj.items[0].volumeInfo.categories[0];
            document.getElementById("bookIsbn10").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[0].identifier;
            document.getElementById("bookIsbn13").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[1].identifier;
            document.getElementById("bookDescription").innerHTML = obj.items[0].volumeInfo.description;

        }
    };

}

function addBook() {

    var bookAuthor = document.getElementById("bookAuthor").innerHTML;
    var bookIsbn10 = document.getElementById("bookIsbn10").innerHTML;
    var bookCategory = document.getElementById("bookCategory").innerHTML;
    var bookPublisher = document.getElementById("bookPublisher").innerHTML;
    var bookYear = parseInt(document.getElementById("bookYear").innerHTML);
    var bookPages = parseInt( document.getElementById("bookPages").innerHTML);
    var bookDesc = document.getElementById("bookDescription").innerHTML;
    var bookTitle = document.getElementById("bookTitle").innerHTML;
    var bookIsbn13 = document.getElementById("bookIsbn13").innerHTML;


    var book = {
        author: bookAuthor,
        isbn_10: bookIsbn10,
        category: bookCategory,
        publisher: bookPublisher,
        publication_year: bookYear,
        pages: bookPages,
        description: bookDesc,
        title: bookTitle,
        isbn_13: bookIsbn13
    }

    PageMethods.Submit_Click(book);
    location.reload();
}