
function getForums() {

    var url = '/Forum/GetForums';

    var languageFrom = $('#langFromInput').val;
    var languageTo = $('#langToInput').val;


    $.ajax({
        url: url,
        method: 'GET',
        data: {
            langFrom: languageFrom,
            langTo: languageTo
        },

        beforeSend: function () {
            $("#loader").css("display", "block");
        },

        done: function (result) {
            // JSON data array
            var data = result.surrey;

            // get DOM node to be parent of child list nodes
            var $data = $("#forumsTable");
            console.log("$data " + $("#forumsTable"));

            // iterate through each object in JSON array
            data.forEach(function (item) {

                // create an unordered list node
                var $tr = $('<tr></tr>');

                // iterate through all the properties of current JSON object
                for (var field in item) {

                    // append list item node to list node
                    $$tr.append(`<td>${field}: ${item[field]}</td>`);
                }

                // append list node to parent node
                $data.append($tr);

                $("#loader").css("display", "none");

            });
        },
        fail: function (error) {
            $("#loader").css("display", "none");
            console.error(error);
        }
    });
}

$(document).ready(function () {

    getForums();

    $(".langInput").change(function () {

        getForums();
    })

});