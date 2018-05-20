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




});

function findMember() {
    document.getElementById("memberNameField").innerHTML = "Looking...";
    var xhttp = new XMLHttpRequest();

    var memberid = document.getElementById("txtmemberid").value;
    var url = "http://api.sparklib.org/api/member?member_id=" + memberid;

    xhttp.open("GET", url);
    xhttp.setRequestHeader("apikey", "254a2c54-5e21-4e07-b2aa-590bc545a520");

    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("memberNameField").innerHTML = obj.first_name + " " + obj.last_name;
            document.getElementById("memberhidden").innerHTML = obj.member_id;
        }
        else {
            document.getElementById("memberNameField").innerHTML = "No Result";
        }
    };
}

function findBook() {
    document.getElementById("itemNameField").innerHTML = "Looking...";
    var xhttp = new XMLHttpRequest();

    var itemid = document.getElementById("txtitemid").value;
    var url = "http://api.sparklib.org/api/book?item_id=" + itemid;

    xhttp.open("GET", url);
    xhttp.setRequestHeader("apikey", "254a2c54-5e21-4e07-b2aa-590bc545a520");

    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("itemNameField").innerHTML = obj.title;
            document.getElementById("itemhidden").innerHTML = obj.item_id;
        }
        else {
            document.getElementById("itemNameField").innerHTML = "No Result";
        }
    };
}

function findDvd() {
    document.getElementById("itemNameField").innerHTML = "Looking...";
    var xhttp = new XMLHttpRequest();

    var itemid = document.getElementById("txtitemid").value;
    var url = "http://api.sparklib.org/api/dvd?item_id=" + itemid;

    xhttp.open("GET", url);
    xhttp.setRequestHeader("apikey", "254a2c54-5e21-4e07-b2aa-590bc545a520");

    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("itemNameField").innerHTML = obj.title;
            document.getElementById("itemhidden").innerHTML = obj.item_id;
        }
        else {
            document.getElementById("itemNameField").innerHTML = "No Result";
        }
    };
}

function findTech() {
    document.getElementById("itemNameField").innerHTML = "Looking...";
    var xhttp = new XMLHttpRequest();

    var itemid = document.getElementById("txtitemid").value;
    var url = "http://api.sparklib.org/api/technology?item_id=" + itemid;

    xhttp.open("GET", url);
    xhttp.setRequestHeader("apikey", "254a2c54-5e21-4e07-b2aa-590bc545a520");

    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("itemNameField").innerHTML = obj.name;
            document.getElementById("itemhidden").innerHTML = obj.item_id;
        }
        else {
            document.getElementById("itemNameField").innerHTML = "No Result";
        }
    };
}



function findGoogleBook() {
    var xhttp = new XMLHttpRequest();

    var isbn = document.getElementById("isbn").value;
    var url = "https://www.googleapis.com/books/v1/volumes?q=isbn:" + isbn;

    xhttp.open("GET", url, true);
    xhttp.send();

    xhttp.onreadystatechange = function () {
        if (this.readyState === 4 && this.status === 200) {
            var obj = JSON.parse(this.responseText);
            document.getElementById("bookTitle").innerHTML = obj.items[0].volumeInfo.title;
            document.getElementById("bookAuthor").innerHTML = obj.items[0].volumeInfo.authors[0];
            document.getElementById("bookPublisher").innerHTML = obj.items[0].volumeInfo.publisher;
            document.getElementById("bookYear").innerHTML = obj.items[0].volumeInfo.publishedDate.slice(0, 4);
            document.getElementById("bookPages").innerHTML = obj.items[0].volumeInfo.pageCount;
            document.getElementById("bookCategory").innerHTML = obj.items[0].volumeInfo.categories[0];
            document.getElementById("bookIsbn10").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[0].identifier;
            document.getElementById("bookIsbn13").innerHTML = obj.items[0].volumeInfo.industryIdentifiers[1].identifier;
            document.getElementById("bookDescription").innerHTML = obj.items[0].volumeInfo.description;
            document.getElementById("bookPic").setAttribute("src", obj.items[0].volumeInfo.imageLinks.thumbnail);
        }
        else {
            document.getElementById("bookTitle").innerHTML = "Sorry, couldn't find that.";
        }
    };

}

function addBook() {

    var bookAuthor = document.getElementById("bookAuthor").innerHTML;
    var bookIsbn10 = document.getElementById("bookIsbn10").innerHTML;
    var bookCategory = document.getElementById("bookCategory").innerHTML;
    var bookPublisher = document.getElementById("bookPublisher").innerHTML;
    var bookYear = parseInt(document.getElementById("bookYear").innerHTML);
    var bookPages = parseInt(document.getElementById("bookPages").innerHTML);
    var bookDesc = document.getElementById("bookDescription").innerHTML.slice(0, 150);
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
    };

    document.getElementById("bookAuthor").innerHTML = "";
    document.getElementById("bookPublisher").innerHTML = "";
    document.getElementById("bookYear").innerHTML = "";
    document.getElementById("bookPages").innerHTML = "";
    document.getElementById("bookCategory").innerHTML = "";
    document.getElementById("bookIsbn10").innerHTML = "";
    document.getElementById("bookIsbn13").innerHTML = "";
    document.getElementById("bookDescription").innerHTML = "";
    document.getElementById("bookPic").setAttribute("src", "images/empty.png");

    var id = PageMethods.Submit_Click(book);
    document.getElementById("bookTitle").innerHTML = "Adding Item to Database";

    setTimeout(function () {
        document.getElementById("bookTitle").innerHTML = "Item Added";
    }, 1000);

}