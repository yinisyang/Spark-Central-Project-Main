function findGoogleBook() {
    var xhttp = new XMLHttpRequest();

    var isbn = document.getElementById("isbn").value;
    var url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn;

    xhttp.open("GET", url, true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("sbookTitle").innerHTML = obj.items[0].volumeInfo.title;
            document.getElementById("sbookAuthor").innerHTML = obj.items[0].volumeInfo.authors[0];
            document.getElementById("sbookPublisher").innerHTML = obj.items[0].volumeInfo.publisher;
            document.getElementById("sbookYear").innerHTML = obj.items[0].volumeInfo.publishedDate.slice(0, 4);
            document.getElementById("sbookPages").innerHTML = obj.items[0].volumeInfo.pageCount;
            document.getElementById("sbookCategory").innerHTML = obj.items[0].volumeInfo.categories[0];
            document.getElementById("sbookIsbn10").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[0].identifier;
            document.getElementById("sbookIsbn13").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[1].identifier;
            document.getElementById("sbookDescription").innerHTML = obj.items[0].volumeInfo.description;
            document.getElementById("sbookPic").setAttribute("src", obj.items[0].volumeInfo.imageLinks.thumbnail);
        }
        else {
            document.getElementById("sbookTitle").innerHTML = "Sorry, couldn't find that.";
        }
    };

}

function addBook() {

    var bookAuthor = document.getElementById("sbookAuthor").innerHTML;
    var bookIsbn10 = document.getElementById("sbookIsbn10").innerHTML;
    var bookCategory = document.getElementById("sbookCategory").innerHTML;
    var bookPublisher = document.getElementById("sbookPublisher").innerHTML;
    var bookYear = parseInt(document.getElementById("sbookYear").innerHTML);
    var bookPages = parseInt(document.getElementById("sbookPages").innerHTML);
    var bookDesc = document.getElementById("sbookDescription").innerHTML.slice(0, 150);
    var bookTitle = document.getElementById("sbookTitle").innerHTML;
    var bookIsbn13 = document.getElementById("sbookIsbn13").innerHTML;


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
    };

    document.getElementById("sbookAuthor").innerHTML = "";
    document.getElementById("sbookPublisher").innerHTML = "";
    document.getElementById("sbookYear").innerHTML = "";
    document.getElementById("sbookPages").innerHTML = "";
    document.getElementById("sbookCategory").innerHTML = "";
    document.getElementById("sbookIsbn10").innerHTML = "";
    document.getElementById("sbookIsbn13").innerHTML = "";
    document.getElementById("sbookDescription").innerHTML = "";
    document.getElementById("sbookPic").setAttribute("src", "images/empty.png");

    var id = PageMethods.Submit_Click(book);
    document.getElementById("sbookTitle").innerHTML = "Adding Item to Database";

    setTimeout(function () {
        if (id != "") {
            document.getElementById("sbookTitle").innerHTML = "Item Added!";
        }
        else {
            document.getElementById("sbookTitle").innerHTML = "Smart Add Failed!";
        }
    }, 1000);

    location.reload();
}