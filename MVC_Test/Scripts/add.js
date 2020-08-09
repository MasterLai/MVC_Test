function add_list() {

    var name = document.getElementById("name").value;
    var age = document.getElementById("age").value;

    $.ajax(
        {
            url: "addItem",
            method: 'POST',
            data: {
                name: name,
                age: age
            },
            dataType: 'json',
            success: function (response) {
                if (response == "success") {

                    alert("Add Completed");
                }
                else {
                    return "error";
                }
            },
            error: function (xhr, status, error) {
                debugger;
                return alert(error);
                //return alert("Add Failure\n");

            }
        });

}